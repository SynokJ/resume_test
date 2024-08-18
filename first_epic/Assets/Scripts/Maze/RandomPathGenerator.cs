using System.Collections.Generic;
using System.Linq;

public class RandomPathGenerator
{
    public HashSet<MazeNode> PathNodes { get => _pathNodes; }

    private Maze _maze = null;
    private HashSet<MazeNode> _pathNodes = new HashSet<MazeNode>();

    private System.Collections.Generic.HashSet<int> _spawnPosId =
        new System.Collections.Generic.HashSet<int>();

    public RandomPathGenerator(Maze maze)
    {
        _maze = maze;
    }

    ~RandomPathGenerator()
    {
        _maze = null;
    }

    public void Generate()
        => StartPath(GetStartNode());

    private void StartPath(MazeNode currentNode)
    {

        _pathNodes.Add(currentNode);
        MazeNode tempNode = currentNode.GetRndNeighboar();
        if (tempNode == null) return;

        currentNode.ConnectNbs(tempNode);
        tempNode.SetPathType();
        StartPath(tempNode);
    }

    public MazeNode GetStartNode()
    {
        int posX = UnityEngine.Mathf.RoundToInt(GameConstants.MAZE_SIZE * 0.5f);
        int posY = UnityEngine.Mathf.RoundToInt(GameConstants.MAZE_SIZE * 0.5f);
        return _maze.GetMazeNode(posX, posY);
    }

    public UnityEngine.Vector2 GetRndSpawnPosition()
    {
        int elementId = UnityEngine.Random.Range(0, _pathNodes.Count);

        while (_spawnPosId.Contains(elementId) || IsStartNode(_pathNodes.ElementAt(elementId).PosOrigin))
            elementId = UnityEngine.Random.Range(0, _pathNodes.Count);

        _spawnPosId.Add(elementId);
        return _pathNodes.ElementAt(elementId).PosOrigin;
    }

    private bool IsStartNode(UnityEngine.Vector2 pos)
        => GetStartNode().PosOrigin == pos;

}
