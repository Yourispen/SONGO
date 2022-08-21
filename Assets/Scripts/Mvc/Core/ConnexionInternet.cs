using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ConnexionInternet : MonoBehaviour
{
    [SerializeField] public static bool connect;
    [SerializeField] private bool testConnexion;

    public static string msgReconnecte="Connexion internet restaurée";
    public static string msgNonConnecte="Vérifier votre connexion internet";
    void OnEnable()
    {
        connect = true;
        testConnexion = true;
    }
    void Update()
    {
        if (testConnexion)
        {
            testConnexion = false;
            StartCoroutine(testConnexionInternet());
        }
    }
    IEnumerator testConnexionInternet()
    {
        //Debug.LogError("Test de connexion");
        UnityWebRequest request = new("http://www.bing.com");
        request.timeout = 5;
        yield return request.SendWebRequest();
        if (request.error == null)
        {
            //Debug.Log("Connecté");
            connect = true;
        }
        else
        {
            //Debug.Log("Non Connecté");
            connect = false;
        }
        testConnexion = true;
    }
}
