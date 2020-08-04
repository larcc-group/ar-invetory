using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void OpenMain()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenObjetos()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenInventario()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenEditInventario()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenAR()
    {
        SceneManager.LoadScene(3);
    }

    public void OpenConfig()
    {
        SceneManager.LoadScene(4);
    }
}
