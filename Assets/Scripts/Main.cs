using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    
    public void InitGame()
    {
        SceneManager.LoadScene("Game");
        Player.Health = 3;
        Player.Points = 0;
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
