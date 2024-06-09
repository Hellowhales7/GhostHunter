using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.SceneManagement;
public class ARSceneManager : MonoBehaviour
{
    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void GoToMain(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    public void GoToDefeat()
    {
        SceneManager.LoadScene("Defeated", LoadSceneMode.Single);
    }
    public void GoToPreistDefeat()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
