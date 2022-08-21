using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlleur_Scene_Setting : MonoBehaviour
{
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    public void bouton_tutorial()
    {
        //va � la sc�ne Tutorial
        //SceneManager.LoadScene("Tutorial");
    }

    public void bouton_training()
    {
        //va � la sc�ne Training
        //SceneManager.LoadScene("Training");
    }

    public void bouton_tables()
    {
        //va � la sc�ne Tables
        SceneManager.LoadScene("Tables");
    }

    public void bouton_pieces()
    {
        //va � la sc�ne Pieces
        SceneManager.LoadScene("Pieces");
    }

    public void bouton_results()
    {
        //va � la sc�ne Results
        //SceneManager.LoadScene("Results");
    }

    public void bouton_back()
    {
        //va � la sc�ne Menu_Principal
        SceneManager.LoadScene("Menu_Principal");
    }
}
