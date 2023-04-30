using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIActionManager : MonoBehaviour
{

    public string sceneName;

    public void ResetScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ResetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
