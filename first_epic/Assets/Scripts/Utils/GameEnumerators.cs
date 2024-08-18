public static class GameEnumerators
{

    public enum BossPathType
    {
        none = 0,
        simpleEdge = 1,
        simpleDiagonal = 2,
        simpleSide = 3,
        crazyEdge = 4,
        crazyDiagonal = 5,
        crazySide = 6
    }

    public enum BossStage
    {
        first = 0,
        second = 1,
        third = 2
    }

    public enum BossPlayerMovementType
    {
        up = 0,
        right = 1,
        down = 2,
        left = 3,
        none = 4
    }

    public enum MazeNodeType
    {
        Free = 0,
        Passed = 1,
        Fired = 2
    }

    public enum WallType
    {
        top = 0,
        right = 1,
        down = 2,
        left = 3
    }

    public enum NodeType
    {
        Walckable = 0,
        HorWall = 1,
        VerWall = 2
    }
}
