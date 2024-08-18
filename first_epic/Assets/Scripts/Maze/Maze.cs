public class Maze
{
    private MazeNode[,] _data =
        new MazeNode[GameConstants.MAZE_SIZE, GameConstants.MAZE_SIZE];

    public Maze()
    {
        for (int r = 0; r < GameConstants.MAZE_SIZE; ++r)
            for (int c = 0; c < GameConstants.MAZE_SIZE; ++c)
                _data[r, c] = new MazeNode(r, c);

        for (int r = 0; r < GameConstants.MAZE_SIZE; ++r)
            for (int c = 0; c < GameConstants.MAZE_SIZE; ++c)
            {
                if (TryGetMazeNode(out MazeNode topNode, r, c + 1))
                    _data[r, c].AddNeighboard(GameEnumerators.WallType.top, topNode);
                if (TryGetMazeNode(out MazeNode rightNode, r + 1, c))
                    _data[r, c].AddNeighboard(GameEnumerators.WallType.right, rightNode);
                if (TryGetMazeNode(out MazeNode downNode, r, c - 1))
                    _data[r, c].AddNeighboard(GameEnumerators.WallType.down, downNode);
                if (TryGetMazeNode(out MazeNode leftNode, r - 1, c))
                    _data[r, c].AddNeighboard(GameEnumerators.WallType.left, leftNode);
            }
    }

    ~Maze()
    {
        for (int r = 0; r < GameConstants.MAZE_SIZE; ++r)
            for (int c = 0; c < GameConstants.MAZE_SIZE; ++c)
                _data[r, c] = null;
        _data = null;
    }

    public void GenerateCells()
    {
        for (int r = 0; r < GameConstants.MAZE_SIZE; ++r)
            for (int c = 0; c < GameConstants.MAZE_SIZE; ++c)
                _data[r, c].GenerateCells();
    }

    public MazeNode GetMazeNode(int r, int c)
        => _data[r, c];

    private bool TryGetMazeNode(out MazeNode node, int r, int c)
    {
        node = null;
        bool res1 = r < GameConstants.MAZE_SIZE && c < GameConstants.MAZE_SIZE;
        bool res2 = r >= 0 && c >= 0;
        if (res1 && res2)
        {
            node = _data[r, c];
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        string res = default;
        for (int r = 0; r < GameConstants.MAZE_SIZE; ++r)
        {
            for (int c = 0; c < GameConstants.MAZE_SIZE; ++c)
                res += _data[r, c];
            res += '\n';
        }

        return res;
    }
}
