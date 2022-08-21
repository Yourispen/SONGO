using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Scene_Offline : MonoBehaviour
{   private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    public void bouton_1vs1()
    {
        //va � la sc�ne 1vs1
        SceneManager.LoadScene("1vs1");
    }

    public void bouton_1vsIA()
    {
        //va � la sc�ne 1vs1
        //SceneManager.LoadScene("1vsIA");
    }

    public void bouton_back()
    {
        //va � la sc�ne Play
        SceneManager.LoadScene("Play");
    }
}
