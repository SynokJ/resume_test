using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WallGenerator
{
    private Maze _maze = null;
    private UnityEngine.GameObject _mazeParent;

    public WallGenerator(Maze maze)
    {
        _maze = maze;
        _mazeParent = new UnityEngine.GameObject("Maze Parent");
    }

    ~WallGenerator()
    {
        _mazeParent = null;
    }

    public void Generate()
    {
        for (int r = 0; r < GameConstants.MAZE_SIZE; ++r)
            for (int c = 0; c < GameConstants.MAZE_SIZE; ++c)
                GenerateWalls(_maze.GetMazeNode(r, c));
    }

    private void GenerateWalls(MazeNode mazeNode)
    {
        GenerateWallBasedOnWalckable(mazeNode, GameEnumerators.WallType.top);
        GenerateWallBasedOnWalckable(mazeNode, GameEnumerators.WallType.right);
        GenerateWallBasedOnWalckable(mazeNode, GameEnumerators.WallType.down);
        GenerateWallBasedOnWalckable(mazeNode, GameEnumerators.WallType.left);
    }

    private void GenerateWallBasedOnWalckable(MazeNode mazeNode, GameEnumerators.WallType wallType)
    {
        if (mazeNode.Walls[wallType].Type != GameEnumerators.NodeType.Walckable)
        {
            var wall = GenerateWall(wallType, mazeNode.PosOrigin);
            wall.name += $" [{mazeNode.PosX} : {mazeNode.PosY}]";
        }
    }

    private UnityEngine.GameObject GenerateWall(GameEnumerators.WallType type, UnityEngine.Vector2 posOrigin)
    {
        UnityEngine.GameObject wall = new UnityEngine.GameObject(type.ToString());
        wall.transform.position = posOrigin + GameConstants.WALL_OFFSETS[type];
        wall.transform.localScale = GameConstants.WALL_SCALES[type];
        wall.transform.parent = _mazeParent.transform;
        wall.isStatic = true;
        wall.gameObject.tag = "Wall";

        bool isVertical = type == GameEnumerators.WallType.right || type == GameEnumerators.WallType.left;

        GenerateSpriteRenderer(wall, isVertical);
        GenerateBoxCollider(wall, isVertical);
        GenerateShadowCaster(wall, isVertical);

        return wall;
    }

    private void GenerateSpriteRenderer(UnityEngine.GameObject wall, bool isVertical)
    {
        SpriteRenderer spriteRenderer = wall.AddComponent<UnityEngine.SpriteRenderer>();

        string path = GetSpritePathByType(isVertical);
        spriteRenderer.sprite = UnityEngine.Resources.Load<UnityEngine.Sprite>(path);
        spriteRenderer.material = UnityEngine.Resources.Load<UnityEngine.Material>("Materials/M_DefaultWall");
    }

    private string GetSpritePathByType(bool isVertical)
    {
        if (isVertical)
            return "Sprites/Wall/StoreShelf_v01";

        return "Sprites/Wall/StoreShelf_h0" + Random.Range(1, 3);
    }

    private void GenerateBoxCollider(UnityEngine.GameObject wall, bool isVertical)
    {
        UnityEngine.BoxCollider2D collider = wall.AddComponent<UnityEngine.BoxCollider2D>();
        if (isVertical)
        {
            collider.size = new Vector2(2.0f, 2.25f);
            collider.offset = new Vector2(0.0f, -0.5f);
            return;
        }

        collider.size = new Vector2(4.0f, 0.25f);
        collider.offset = new Vector2(0.0f, -0.8f);
    }

    private void GenerateShadowCaster(UnityEngine.GameObject wall, bool isVertical)
    {
        ShadowCaster2D shadow = wall.AddComponent<ShadowCaster2D>();
        shadow.selfShadows = true;

        var field = typeof(ShadowCaster2D).GetField("m_ApplyToSortingLayers", BindingFlags.Instance | BindingFlags.NonPublic);
        int[] val = { SortingLayer.NameToID("Floor Shadow") };
        field.SetValue(shadow, val);

        var field_path = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.Instance | BindingFlags.NonPublic);
        Vector3[] path = default;

        if (isVertical)
        {
            path = new Vector3[]{
                new Vector2(-0.5f, -2.0f),
                new Vector2(-0.5f, 2.0f),
                new Vector2(0.5f, 2.0f),
                new Vector2(0.5f, -2.0f)
            };
        }
        else
        {
            path = new Vector3[]
            {
                new Vector2(-2.0f, -0.5f),
                new Vector2(-2.0f, -0.1f),
                new Vector2(2.0f, -0.1f),
                new Vector2(2.0f, -0.5f)
            };
        }

        field_path.SetValue(shadow, path);

        FieldInfo meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.Instance | BindingFlags.NonPublic);
        meshField.SetValue(shadow, null);

        MethodInfo onEnableMethod = typeof(ShadowCaster2D).GetMethod("OnEnable", BindingFlags.Instance | BindingFlags.NonPublic);
        onEnableMethod.Invoke(shadow, new object[0]);
    }
}
