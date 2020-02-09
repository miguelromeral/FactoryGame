using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    
    public void InitGame()
    {
        SceneManager.LoadScene("Game");
        Player.InitPlayer();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Player.Health -= 1;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Main");
    }
}
