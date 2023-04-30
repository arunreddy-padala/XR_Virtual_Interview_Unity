using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartInterview : MonoBehaviour
{
    public void LoadScene(string sceneName) {
        Debug.Log("creating scene....");
        SceneManager.LoadScene(sceneName);
    }

}
