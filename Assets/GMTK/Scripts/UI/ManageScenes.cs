using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ManageScenes : MonoBehaviour
{
    public void Play(string gameScene)
    {
        SceneManager.LoadScene(gameScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
