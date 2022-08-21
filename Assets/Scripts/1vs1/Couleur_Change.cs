using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couleur_Change : Script_Couleur_Change
{
    [Header("scripts")]
    public Couleur_Change couleur_change;
    public Controller_Match_1vs1 controlleur;

    #region Fonctions Principales Unity
    void Start()
    {
        temps_depart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!controlleur.en_deplacement)
        {
            initialise_couleur();
            return;
        }

        temps_actuel = Time.time;
        if (temps_actuel >= temps_depart + 0.2f)
        {
            temps_depart = temps_actuel;
            clignoter_case();
        }
    }

    #endregion

    #region Fonctions voids

    public void initialise_couleur()
    {
        gameObject.GetComponent<Renderer>().material = couleur_initiale;
        couleur_change.GetComponent<Couleur_Change>().enabled = false;
    }

    #endregion
}
