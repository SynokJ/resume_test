using UnityEngine;


public class SceneController : MonoBehaviour
{
    public void LoadMainGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}
