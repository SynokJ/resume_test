using UnityEngine;

public class PlayerGenerator
{

    private GameObject _innerCircle;
    private GameObject _outterCircle;
    private Gameplay _gameplay;
    private CameraController _cameraController;

    private RandomPathGenerator _randomPathGenerator;

    private PlayerGenerator(RandomPathGenerator rndPathGenerator, Gameplay gameplay, CameraController cameraController)
    {
        _randomPathGenerator = rndPathGenerator;
        _cameraController = cameraController;
        _gameplay = gameplay;
    }

    public void InitJoystick(GameObject innerCircle, GameObject outerCircle)
    {
        _innerCircle = innerCircle;
        _outterCircle = outerCircle;
    }

    public void Generate()
    {
        UnityEngine.Vector2 pos = _randomPathGenerator.GetRndSpawnPosition();
        GeneratePlayer(pos);
    }

    private void GeneratePlayer(UnityEngine.Vector2 pos)
    {
        GameObject player = new GameObject();
        player.transform.position = pos;
        player.name = "Player";

        GenerateSpriteRenderer(player);
        GenerateRigibody(player);
        GenerateCollider(player);
        GenerateAnimator(player);
        GenerateLight(player);

        GeneratePlayer(player);
        _cameraController.SetTargetTr(player.transform);
    }

    private void GenerateSpriteRenderer(GameObject player)
    {
        SpriteRenderer renderer = player.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>(PlayerDataManager.testData.SpritePath);
        renderer.material = Resources.Load<Material>("Materials/M_DefaultWall");
        renderer.material.color = Color.white;
    }

    private void GenerateRigibody(GameObject player)
    {
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.freezeRotation = true;
    }

    private void GenerateCollider(GameObject player)
    {
        BoxCollider2D colldier = player.AddComponent<BoxCollider2D>();
        colldier.offset = new Vector2(0.0f, -1.0f);
        colldier.size = new Vector2(1.75f, 1.0f);
    }

    private void GeneratePlayer(GameObject player)
    {
        MazePlayer mazePlayer = player.AddComponent<MazePlayer>();
        mazePlayer.InitMazePlayer(_innerCircle, _outterCircle, _gameplay);
    }

    private void GenerateLight(GameObject player)
    {
        UnityEngine.Rendering.Universal.Light2D light = player.AddComponent<UnityEngine.Rendering.Universal.Light2D>();
        light.intensity = 0.4f;
        light.falloffIntensity = 0.7f;
        light.shadowsEnabled = true;
        light.shadowIntensity = 1.0f;
        light.pointLightOuterRadius = 9.0f;
    }

    private void GenerateAnimator(GameObject player)
    {
        Animator animator = player.AddComponent<Animator>();
        string path = PlayerDataManager.testData.AnimatorPath;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(path);
    }
}
