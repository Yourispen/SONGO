using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float temps_depart;
    float temps_actuel;

    public int compteur;

    public Controller_Match_Online controller_match;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Controller_Match_Online") != null)
        {
            controller_match = GameObject.Find("Controller_Match_Online").GetComponent<Controller_Match_Online>();
        }
        //temps_depart = Time.timeSinceLevelLoad;

        compteur = 1;
        StartCoroutine("temps_ecoule");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("p"))
        {
            print("p");
            Time.timeScale = 0;
        }
    }

    void fin_de_connexion()
    {
        controller_match.fin_de_connexion();
    }

    IEnumerator temps_ecoule()
    {
        while (compteur<controller_match.controlleur_scene.gameManager.intervalle)
        {
            Debug.Log(compteur++);
            yield return new WaitForSeconds(1f);
        }
        if (controller_match.match_fini)
            yield break;
        Debug.Log(compteur);
        fin_de_connexion();
    }
}
