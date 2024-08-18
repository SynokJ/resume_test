using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public void LoseGame()
    {
        SceneManager.LoadScene(0);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(0);
    }

    public void CheckForWin(bool status)
    {
        if (status) WinGame();
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(2);
    }
}