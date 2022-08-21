using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Scene_Online : MonoBehaviour
{
    public Photon_Manager photon_manager;

    [Header("Les Objets de la sc�ne")]
    public GameObject Chargement;
    public GameObject Menu;
    public GameObject TextError;


    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //PlayerPrefs.DeleteKey("pseudo");
    }

    public void bouton_match()
    {
        
        //SceneManager.LoadScene("Match");
        photon_manager.bouton_match();

    }

    public void bouton_friends()
    {
        //va � la sc�ne Friends
        //SceneManager.LoadScene("Friends");
    }

    public void bouton_back()
    {
        //va � la sc�ne Play
        SceneManager.LoadScene("Play");
    }
}
