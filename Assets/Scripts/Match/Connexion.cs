using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Connexion : MonoBehaviour
{
    public bool connect;
    public bool test_connexion;
    public GameManager gameManager;

    public Controller_Scene_Match controller_scene;
    public Controller_Match_Online controller_match;

    // Start is called before the first frame update
    void Start()
    {
        test_connexion = true;
        connect = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller_match.match_fini)
            return;
        if (test_connexion)
        {
            test_connexion = false;
            StartCoroutine("test_connexion_internet");
        }
    }

    #region Coroutines

    IEnumerator test_connexion_internet()
    {
        //Debug.LogError("Test de connexion");
        UnityWebRequest request = new("http://www.bing.com");
        request.timeout = 10;
        yield return request.SendWebRequest();
        if (request.error == null)
        {
            if (!connect)
            {
                connect = true;
                Debug.Log("connexion restaurée.");
                gameManager.active_reconnexion();
            }
        }
        else
        {
            if (GameObject.Find("Timer") == null)
            {
                controller_scene.en_pause = true;
                controller_match.deconnexion = true;
                connect = false;
                string msg = "Connexion impossible.\nEn attente de reconnexion...";
                controller_scene.afficher_message(msg, true);
                controller_scene.instancier_timer();
            }
        }
        test_connexion = true;
    }

    #endregion
}
