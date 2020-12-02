using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void btn_Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
