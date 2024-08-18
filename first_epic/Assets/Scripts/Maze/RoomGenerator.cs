public class RoomGenerator
{

    private Maze _maze;

    public RoomGenerator(Maze maze)
    {
        _maze = maze;
    }

    ~RoomGenerator()
    {
    }

    public void Generate()
    {
        for (int r = 0; r < GameConstants.MAZE_SIZE; ++r)
            for (int c = 0; c < GameConstants.MAZE_SIZE; ++c)
            {
                MazeNode mazeNode = _maze.GetMazeNode(r, c);
                if (mazeNode.IsBlockededMazeNode())
                    mazeNode.DestroyBlockedMazeNode();
            }
    }
}