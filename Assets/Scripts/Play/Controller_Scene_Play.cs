using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mvc.Models;

public class Controller_Scene_Play : MonoBehaviour
{
    //[SerializeField] public GameObject pionPrefab;
    //[SerializeField] private GameObject pion;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        /*for (int i = 1; i <= 3; i++)
        {
            pion = Instantiate(pionPrefab, pionPrefab.transform.position, Quaternion.identity);
        }*/
    }

    public void bouton_offline()
    {
        //va � la sc�ne Offline
        SceneManager.LoadScene("Offline");
    }

    public void bouton_online()
    {
        //va � la sc�ne Online
        SceneManager.LoadScene("Online");
    }

    public void bouton_back()
    {
        //va � la sc�ne Menu_Principal
        SceneManager.LoadScene("Menu_Principal");
    }
}
