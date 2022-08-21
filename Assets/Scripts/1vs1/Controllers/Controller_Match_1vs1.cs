using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Match_1vs1 : Script_Controller_Match
{
    #region Variables

    [Header("scripts")]
    public Controller_Case[] cases = new Controller_Case[14];
    public Controller_Case[] grandes_cases = new Controller_Case[2];
    public Controlller_Scene_1vs1 controlleur_scene;

    #endregion

    #region Fonctions Principale Unity

    private void Start()
    {
        match_fini = false;
        numero_case_depart = 0;
        en_deplacement = false;
        StartCoroutine("generer_pions");
    }

    #endregion

    #region Fonctions voids

    public void tour_suivant(int numero_joueur)
    {
        //si le joueur 1 a mangé plus de 35 pions
        if (grandes_cases[0].nombre_de_pions() > 35)
        {
            fin_du_match(joueur_1, true);
            return;
        }
        //si le joueur 1 a mangé plus de 35 pions
        else if (grandes_cases[1].nombre_de_pions() > 35)
        {
            fin_du_match(joueur_2, true);
            return;
        }
        //si le joueur 1 a mangé 35 pions et le joueur 2 a mangé 35 pions
        else if (grandes_cases[0].nombre_de_pions() == 35 && grandes_cases[1].nombre_de_pions() == 35)
        {
            fin_du_match(joueur_1, false);
            return;
        }
        else
        {
            //si c'est le joueur 1
            if (numero_joueur == 1)
            {
                //si l'adversaire n'a plus de pions
                if (nombre_de_pions_null(2, 14))
                {
                    fin_du_match(joueur_1, true);
                    return;
                }
                joueur_2.mon_tour = true;
                joueur_1.mon_tour = false;
            }
            //si c'est le joueur 2
            else if (numero_joueur == 2)
            {
                //si l'adversaire n'a plus de pions
                if (nombre_de_pions_null(1, 7))
                {
                    fin_du_match(joueur_2, true);
                    return;
                }
                joueur_1.mon_tour = true;
                joueur_2.mon_tour = false;
            }
            en_deplacement = false;
            numero_case_depart = 0;
            controlleur_scene.afficher_tour();
        }
    }

    public void fin_du_match(Joueur joueur, bool vainqueur)
    {
        peut_jouer = false;
        peut_compter = false;
        match_fini = true;

        joueur_1.mon_tour = true;
        joueur_2.mon_tour = false;

        en_deplacement = false;
        numero_case_depart = 0;

        if (vainqueur)
            joueur.victoire();
        controlleur_scene.afficher_fin_match();
    }

    public void restart()
    {
        numero_case_depart = 0;
        en_deplacement = false;
        foreach (Controller_Case _case in cases)
        {
            _case.restart();
        }
        foreach (Controller_Case grande_case in grandes_cases)
        {
            grande_case.restart();
        }
        joueur_1.restart();
        joueur_2.restart();
        StartCoroutine("partager_pions");

    }


    #endregion

    #region Fonctions Coroutines

    IEnumerator partager_pions()
    {
        int i = 0;
        while (i < 70)
        {
            foreach (Controller_Case _case in cases)
            {
                _case.ajouter_pion(pions_match[i]);
                i += 1;
                yield return new WaitForSeconds(temps_depot_case);

            }
        }
        joueur_1.mon_tour = true;
        peut_jouer = true;
        peut_compter = true;
        match_fini = false;
        controlleur_scene.afficher_tour();
    }

    #endregion

    #region Fonctions bools

    //retourne true si le joueur adverse a encore des pions et false sinon
    public bool nombre_de_pions_null(int numero_joueur, int derniere_case)
    {
        int somme_des_cases = 0;

        //si c'est le tour du joueur 1
        if (numero_joueur == 2)
        {
            //faire la somme du nombre pions de chaque case du joueur 2
            for (int i = 7; i < derniere_case; i++)
            {
                somme_des_cases += cases[i].nombre_de_pions();
            }
        }
        else
        {
            //faire la somme du nombre pions de chaque case du joueur 1
            for (int i = 0; i < derniere_case; i++)
            {
                somme_des_cases += cases[i].nombre_de_pions();
            }
        }
        // si la somme est égal à 0
        if (somme_des_cases == 0)
            return true;
        //si la somme est supérieur à 0
        return false;
    }

    //retourne true si le joueur peut transmettre des pions à l'adversaire et false sinon
    public bool peut_transmettre_des_pions(int numero_joueur)
    {
        //si c'est le tour du joueur 1
        if (numero_joueur == 1)
        {
            int nombre_de_pions_minimum = 7;
            //teste si le joueur 1 peut transmettre des pions
            for (int i = 0; i < 6; i++)
            {
                if (cases[i].nombre_de_pions() >= nombre_de_pions_minimum)
                    return true;
                nombre_de_pions_minimum -= 1;
            }
            //teste si la case 7 peut transmettre des pions à l'adversaire
            if (cases[6].nombre_de_pions() >= 2)
                return true;

        }
        else
        {
            int nombre_de_pions_minimum = 7;
            //teste si le joueur 1 peut transmettre des pions
            for (int i = 7; i < 13; i++)
            {
                if (cases[i].nombre_de_pions() >= nombre_de_pions_minimum)
                    return true;
                nombre_de_pions_minimum -= 1;
            }
            //teste si la case 14 peut transmettre des pions à l'adversaire
            if (cases[13].nombre_de_pions() >= 2)
                return true;
        }
        return false;
    }

    //permet de manger ou pas
    public bool peut_manger(int numero_joueur)
    {
        if (numero_joueur == 1)
        {
            for (int i = 0; i < 6; i++)
            {
                if (cases[i].nombre_de_pions() > 4 || cases[i].nombre_de_pions() < 2)
                    return true;
            }
        }
        else
        {
            for (int i = 7; i < 13; i++)
            {
                if (cases[i].nombre_de_pions() > 4 || cases[i].nombre_de_pions() < 2)
                    return true;
            }
        }
        return false;
    }

    #endregion
}
