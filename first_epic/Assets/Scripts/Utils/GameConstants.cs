using System.Collections.Generic;

public static class GameConstants
{
    public const int COINS_SCORE_TARGET = 3;

    public const float ENEMY_TIMER_VALUE = 5.0f;
    public const float ENEMY_MOVEMENT_SPEED = 5.0f;
    public const float ENEMY_CHASING_SPEED_MIN = 5.0f;
    public const float ENEMY_CHASING_SPEED_MAX = 7.0f;
    public const float ENEMY_CHASING_TIMER_VALUE = 3.0f;
    public const float ENEMY_TRIGGER_RADIUS_ORIGIN = 5.0f;
    public const float ENEMY_TRIGGER_RADIUS_CHASE = 10.0f;

    public const float PLAYER_MOVEMENT_SPEED = 8.0f;

    public const string JOYSTICK_TAG_NAME = "Joystick";

    public const int MAZE_SIZE = 11;
    public const float WALL_SIZE = 6.0f;

    public const float MAZE_NODE_OFFSET = WALL_SIZE * 0.5f;
    public const float WALL_OFFSET = WALL_SIZE * 0.5f;

    public static readonly Dictionary<GameEnumerators.WallType, UnityEngine.Vector2> WALL_OFFSETS =
        new Dictionary<GameEnumerators.WallType, UnityEngine.Vector2>{
        {GameEnumerators.WallType.top, UnityEngine.Vector2.up * WALL_OFFSET},
        {GameEnumerators.WallType.right, UnityEngine.Vector2.right * WALL_OFFSET},
        {GameEnumerators.WallType.down, UnityEngine.Vector2.down * WALL_OFFSET},
        {GameEnumerators.WallType.left, UnityEngine.Vector2.left * WALL_OFFSET}
    };

    public static readonly Dictionary<GameEnumerators.WallType, UnityEngine.Vector2> WALL_SCALES =
        new Dictionary<GameEnumerators.WallType, UnityEngine.Vector2>{
        {GameEnumerators.WallType.top, new UnityEngine.Vector2(1.5f, 2.0f)},
        {GameEnumerators.WallType.right, new UnityEngine.Vector2(1.5f, 2.5f)},
        {GameEnumerators.WallType.down, new UnityEngine.Vector2(1.5f, 2.0f)},
        {GameEnumerators.WallType.left, new UnityEngine.Vector2(1.5f, 2.5f)}
    };

    public static readonly Dictionary<GameEnumerators.WallType, GameEnumerators.WallType> WALL_REVERTS =
    new Dictionary<GameEnumerators.WallType, GameEnumerators.WallType>{
        {GameEnumerators.WallType.top, GameEnumerators.WallType.down},
        {GameEnumerators.WallType.right, GameEnumerators.WallType.left},
        {GameEnumerators.WallType.down, GameEnumerators.WallType.top},
        {GameEnumerators.WallType.left, GameEnumerators.WallType.right}
    };

    public static readonly Dictionary<UnityEngine.Vector2, GameEnumerators.BossPlayerMovementType> BOSS_MOVEMENT_TYPE =
        new Dictionary<UnityEngine.Vector2, GameEnumerators.BossPlayerMovementType> {
            { UnityEngine.Vector2.up, GameEnumerators.BossPlayerMovementType.up},
            { UnityEngine.Vector2.right, GameEnumerators.BossPlayerMovementType.right},
            { UnityEngine.Vector2.down, GameEnumerators.BossPlayerMovementType.down},
            { UnityEngine.Vector2.left , GameEnumerators.BossPlayerMovementType.left}
        };

    public static float PLAYER_BOSS_MOVEMENT_DELAY = 0.1f;
    public static float PLAYER_BOSS_MOVEMENT_DISTANCE = 2.0f;
    public static float PLAYER_BOSS_MOVEMENT_SPEED = 10.0f;
    public static readonly Dictionary<GameEnumerators.BossPathType, List<UnityEngine.Vector2>> BOSS_PATH_TYPE =
        new Dictionary<GameEnumerators.BossPathType, List<UnityEngine.Vector2>>(){
            { GameEnumerators.BossPathType.simpleEdge, new List<UnityEngine.Vector2>{
                new UnityEngine.Vector2(-10, 4),
                new UnityEngine.Vector2(-10, -4),
                new UnityEngine.Vector2(10, -4),
                new UnityEngine.Vector2(10, 4)
            } },
            { GameEnumerators.BossPathType.simpleDiagonal, new List<UnityEngine.Vector2>{
                new UnityEngine.Vector2(-10, -4),
                new UnityEngine.Vector2(10, 4),
                new UnityEngine.Vector2(10, -4),
                new UnityEngine.Vector2(-10, 4),
                new UnityEngine.Vector2(-10, -4),
                new UnityEngine.Vector2(10, 4)
            } },
            { GameEnumerators.BossPathType.simpleSide, new List<UnityEngine.Vector2>{
                new UnityEngine.Vector2(0, 0),
                new UnityEngine.Vector2(10, 0),
                new UnityEngine.Vector2(-10, 0),
                new UnityEngine.Vector2(0, 0),
                new UnityEngine.Vector2(0, 4),
                new UnityEngine.Vector2(0, -4),
                new UnityEngine.Vector2(0, 0)
            } },
            { GameEnumerators.BossPathType.crazyEdge, new List<UnityEngine.Vector2>{
                new UnityEngine.Vector2(-10, 4),
                new UnityEngine.Vector2(-8, 2),
                new UnityEngine.Vector2(-10, 0),
                new UnityEngine.Vector2(-8, -2),
                new UnityEngine.Vector2(-10, -4),
                new UnityEngine.Vector2(-5, -2),
                new UnityEngine.Vector2(0, -4),
                new UnityEngine.Vector2(5, -2),
                new UnityEngine.Vector2(10, -4),
                new UnityEngine.Vector2(8, -2),
                new UnityEngine.Vector2(10, 0),
                new UnityEngine.Vector2(8, 2),
                new UnityEngine.Vector2(10, 4),
                new UnityEngine.Vector2(5, 2),
                new UnityEngine.Vector2(0, 4),
                new UnityEngine.Vector2(-5, 2),
                new UnityEngine.Vector2(-10, 4)
            } },
            { GameEnumerators.BossPathType.crazyDiagonal, new List<UnityEngine.Vector2>{
                new UnityEngine.Vector2(-10, -4),
                new UnityEngine.Vector2(-5, 0),
                new UnityEngine.Vector2(0, 0),
                new UnityEngine.Vector2(5, 0),
                new UnityEngine.Vector2(10, 4),
                new UnityEngine.Vector2(8, 2),
                new UnityEngine.Vector2(10, 0),
                new UnityEngine.Vector2(8, -2),
                new UnityEngine.Vector2(10, -4),
                new UnityEngine.Vector2(5, 0),
                new UnityEngine.Vector2(0, 0),
                new UnityEngine.Vector2(-5, 0),
                new UnityEngine.Vector2(-10, 4),
                new UnityEngine.Vector2(-8, 2),
                new UnityEngine.Vector2(-10, 0),
                new UnityEngine.Vector2(-8, -2),
                new UnityEngine.Vector2(-10, -4),
                new UnityEngine.Vector2(10, 4)
            } },
            { GameEnumerators.BossPathType.crazySide, new List<UnityEngine.Vector2>{
                new UnityEngine.Vector2(0, 0),
                new UnityEngine.Vector2(10, 0),
                new UnityEngine.Vector2(-10, 0),
                new UnityEngine.Vector2(0, 0),
                new UnityEngine.Vector2(0, 4),
                new UnityEngine.Vector2(0, -4),
                new UnityEngine.Vector2(0, 0)
            } },
            { GameEnumerators.BossPathType.none, new List<UnityEngine.Vector2>{
                new UnityEngine.Vector2(0, 0)
            } }
        };

    public static readonly Dictionary<System.Type, string> BOSS_SPRITE_PATH =
        new Dictionary<System.Type, string>() 
        {
            { typeof(ShootingBoss), "Sprites/Boss/Boss_Shooting" },
            { typeof(MovingBoss), "Sprites/Boss/Boss_Moving" }
        };

    public static readonly Dictionary<System.Type, string> BOSS_ANIMATOR_PATH =
        new Dictionary<System.Type, string>()
        {
            { typeof(ShootingBoss), "Animations/Boss_Shooting_0" },
            { typeof(MovingBoss), "Animations/Boss_Moving_0" }
        };
}
