using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Scene_Pieces : MonoBehaviour
{
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void bouton_back()
    {
        //va � la sc�ne Setting
        SceneManager.LoadScene("Setting");
    }
}
