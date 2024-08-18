using System.Collections.Generic;

public class MazeNode
{
    public int PosX { get => _posX; }
    public int PosY { get => _posY; }
    public UnityEngine.Vector2 PosOrigin
    {
        get => new UnityEngine.Vector2(_posX, _posY) * GameConstants.MAZE_NODE_OFFSET * 2.0f;
    }

    public Dictionary<GameEnumerators.WallType, Node> Walls { get => _nodes; }
    public Dictionary<GameEnumerators.WallType, MazeNode> Neighboards { get => _neighboards; }
    public GameEnumerators.MazeNodeType PathType { get => _pathType; }

    private int _posX = 0;
    private int _posY = 0;

    private Dictionary<GameEnumerators.WallType, Node> _nodes =
        new Dictionary<GameEnumerators.WallType, Node>();

    private Dictionary<GameEnumerators.WallType, MazeNode> _neighboards =
        new Dictionary<GameEnumerators.WallType, MazeNode>();

    private GameEnumerators.MazeNodeType _pathType = GameEnumerators.MazeNodeType.Free;

    private int[] _nbsIds = new int[4];
    private int currentNbsId = 0;

    public MazeNode(int x, int y)
    {
        _posX = x;
        _posY = y;

        _nodes[GameEnumerators.WallType.top] = new Node(GameEnumerators.NodeType.HorWall);
        _nodes[GameEnumerators.WallType.right] = new Node(GameEnumerators.NodeType.VerWall);
        _nodes[GameEnumerators.WallType.down] = new Node(GameEnumerators.NodeType.HorWall);
        _nodes[GameEnumerators.WallType.left] = new Node(GameEnumerators.NodeType.VerWall);

        _nbsIds = GenerateRandomIndexes();
    }

    ~MazeNode()
    {
        _nodes[GameEnumerators.WallType.top] = null;
        _nodes[GameEnumerators.WallType.right] = null;
        _nodes[GameEnumerators.WallType.down] = null;
        _nodes[GameEnumerators.WallType.left] = null;
    }

    public void GenerateCells()
    {
        GameEnumerators.WallType tempType = GameEnumerators.WallType.top;
        _nodes[tempType] = new Node(GameEnumerators.NodeType.Walckable);
        _neighboards[tempType].SetNodeValue(GameConstants.WALL_REVERTS[tempType], GameEnumerators.NodeType.Walckable);

        tempType = GameEnumerators.WallType.right;
        _nodes[tempType] = new Node(GameEnumerators.NodeType.Walckable);
        _neighboards[tempType]?.SetNodeValue(GameConstants.WALL_REVERTS[tempType], GameEnumerators.NodeType.Walckable);

        tempType = GameEnumerators.WallType.down;
        _nodes[tempType] = new Node(GameEnumerators.NodeType.Walckable);
        _neighboards[tempType]?.SetNodeValue(GameConstants.WALL_REVERTS[tempType], GameEnumerators.NodeType.Walckable);

        tempType = GameEnumerators.WallType.left;
        _nodes[tempType] = new Node(GameEnumerators.NodeType.Walckable);
        _neighboards[tempType]?.SetNodeValue(GameConstants.WALL_REVERTS[tempType], GameEnumerators.NodeType.Walckable);
    }

    public void AddNeighboard(GameEnumerators.WallType type, MazeNode mazeNode)
        => _neighboards[type] = mazeNode;

    public void SetNodeValue(GameEnumerators.WallType wallType, GameEnumerators.NodeType nodeType)
        => _nodes[wallType] = new Node(nodeType);

    public override string ToString()
    {
        string res = _nodes[GameEnumerators.WallType.top].ToString();
        res += _nodes[GameEnumerators.WallType.right].ToString();
        res += _nodes[GameEnumerators.WallType.down].ToString();
        res += _nodes[GameEnumerators.WallType.left].ToString();
        return $"[{res}]";
    }

    public MazeNode GetRndNeighboar()
    {
        if (currentNbsId > 3) return null;

        GameEnumerators.WallType type = GetWallType();
        if (!_neighboards.ContainsKey(type)) return null;
        return _neighboards[type];
    }

    private GameEnumerators.WallType GetWallType()
    {
        GameEnumerators.WallType type = (GameEnumerators.WallType)_nbsIds[currentNbsId];
        while (!_neighboards.ContainsKey(type))
        {
            currentNbsId++;
            if (currentNbsId == 3) break;
            type = (GameEnumerators.WallType)_nbsIds[currentNbsId];

            if (IsWalkableMazeNode(type)) continue;
        }
        return type;
    }

    private bool IsWalkableMazeNode(GameEnumerators.WallType type)
        => _neighboards.ContainsKey(type) &&
        _neighboards[type].PathType != GameEnumerators.MazeNodeType.Free;

    public void ConnectNbs(MazeNode node)
    {
        GameEnumerators.WallType type = (GameEnumerators.WallType)_nbsIds[currentNbsId];
        _nodes[type] = new Node(GameEnumerators.NodeType.Walckable);
        node.SetNodeValue(GameConstants.WALL_REVERTS[type], GameEnumerators.NodeType.Walckable);
        currentNbsId++;
    }

    private int[] GenerateRandomIndexes()
    {
        int[] res = new int[] { 0, 1, 2, 3 };
        System.Random rand = new System.Random();

        int size = 4;
        for (int i = 0; i < size; ++i)
        {
            int rndIndex = rand.Next(0, size);
            int tempNum = res[i];
            res[i] = res[rndIndex];
            res[rndIndex] = tempNum;
        }

        return res;
    }

    public bool IsBlockededMazeNode()
    {
        for (int i = 0; i < 4; ++i)
            if (_nodes[(GameEnumerators.WallType)i].Type == GameEnumerators.NodeType.Walckable)
                return false;

        return true;
    }

    public void DestroyBlockedMazeNode()
    {
        for (int i = 0; i < 4; ++i)
            _nodes[(GameEnumerators.WallType)i] = new Node(GameEnumerators.NodeType.Walckable);
    }

    public void SetPathType()
    {
        if (_pathType == GameEnumerators.MazeNodeType.Free)
            _pathType = GameEnumerators.MazeNodeType.Passed;
        else if (_pathType == GameEnumerators.MazeNodeType.Passed)
            _pathType = GameEnumerators.MazeNodeType.Fired;
    }
}