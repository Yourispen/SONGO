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
        //va à la scène Tutorial
        //SceneManager.LoadScene("Tutorial");
    }

    public void bouton_training()
    {
        //va à la scène Training
        //SceneManager.LoadScene("Training");
    }

    public void bouton_tables()
    {
        //va à la scène Tables
        SceneManager.LoadScene("Tables");
    }

    public void bouton_pieces()
    {
        //va à la scène Pieces
        SceneManager.LoadScene("Pieces");
    }

    public void bouton_results()
    {
        //va à la scène Results
        //SceneManager.LoadScene("Results");
    }

    public void bouton_back()
    {
        //va à la scène Menu_Principal
        SceneManager.LoadScene("Menu_Principal");
    }
}
