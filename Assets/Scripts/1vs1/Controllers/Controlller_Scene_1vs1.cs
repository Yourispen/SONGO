using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlller_Scene_1vs1 : Script_Controller_Scene_Match
{
    #region Variables

    [Header("Enregistrements")]
    [SerializeField] private GameObject Registrations;
    [SerializeField] private TMPro.TMP_Text TextError;

    public Controller_Match_1vs1 controlleur_match;

    [SerializeField]
    private string scene_precedante="Offline";


    #endregion

    #region Fonctions Principale Unity

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //le Menu_Pause est désactivé
        en_pause = false;
    }
    #endregion

    #region Boutons

    public void bouton_pause()
    {
        //active le Menu_Pause
        MenuPause.SetActive(true);
        en_pause = true;
        //désactive le bouton_Pause
        ButtonPause.SetActive(false);
    }

    public void bouton_continue()
    {
        //active le bouton_Pause
        ButtonPause.SetActive(true);
        //désactive le Menu_Pause
        MenuPause.SetActive(false);
        en_pause = false;
    }

    public void bouton_back()
    {
        //va à la scène Offline
        SceneManager.LoadScene(scene_precedante);
    }

    public void bouton_resign()
    {
        MenuPause.SetActive(false);
        if (joueur_1.mon_tour)
        {
            controlleur_match.fin_du_match(joueur_2, true);
        }
        else if (joueur_2.mon_tour)
        {
            controlleur_match.fin_du_match(joueur_1, true);
        }
    }

    public void bouton_quit()
    {
        //va à la scène Offline
        SceneManager.LoadScene(scene_precedante);
    }

    public void bouton_replay()
    {
        Counter_Pieces_P1.SetActive(false);
        //désactive les objets pour afficher la fin du match
        Text_Victoire_Mini.enabled = false;
        Fin_Match.SetActive(false);
        Background_Victoire.SetActive(false);
        Background_Victoire_Mini.SetActive(false);
        ButtonPause.SetActive(true);
        controlleur_match.restart();
    }

    public void bouton_start()
    {
        int taille = 3;
        if (joueur_1.recupere_le_nom().Length >= taille && joueur_2.recupere_le_nom().Length >= taille)
        {
            //active le bouton Pause
            ButtonPause.SetActive(true);
            //active les profils des deux joueurs
            Profil_P1.SetActive(true);
            Profil_P2.SetActive(true);
            afficher_noms();
            afficher_score();
            controlleur_match.GetComponent<Controller_Match_1vs1>().enabled = true;
            //désactive le menu d'enregistrement
            Registrations.SetActive(false);
        }
        else if (joueur_1.recupere_le_nom().Length >= taille && joueur_2.recupere_le_nom().Length < taille)
        {
            TextError.GetComponent<TMPro.TMP_Text>().text = "Le nom  du joueur 2 doit contenir au mois 4 lettres";
        }
        else if (joueur_1.recupere_le_nom().Length < taille && joueur_2.recupere_le_nom().Length >= taille)
        {
            TextError.GetComponent<TMPro.TMP_Text>().text = "Le nom  du joueur 1 doit contenir au mois 4 lettres";
        }
        else
        {
            TextError.GetComponent<TMPro.TMP_Text>().text = "Les noms  des joueurs doivent contenir au mois 4 lettres";
        }

    }

    public void bouton_table_jeu()
    {
        Text_Victoire_Mini.GetComponent<TMPro.TMP_Text>().text = Text_Victoire.GetComponent<TMPro.TMP_Text>().text;
        Text_Victoire_Mini.enabled = true;
        controlleur_match.peut_compter = true;
        Background_Victoire_Mini.SetActive(true);
        Background_Victoire.SetActive(false);
    }

    #endregion

    #region Fonctions voids

    public void saisir_nom_joueur_1(string nom)
    {
        joueur_1.donner_le_nom(nom);
    }

    public void saisir_nom_joueur_2(string nom)
    {
        joueur_2.donner_le_nom(nom);
    }

    public void afficher_nombre_de_pions(string nombre)
    {
        if (joueur_1.mon_tour)
            Text_Compteur_P1.GetComponent<TMPro.TMP_Text>().text = nombre;
        else if (joueur_2.mon_tour)
            Text_Compteur_P2.GetComponent<TMPro.TMP_Text>().text = nombre;
    }
    //afficher les infos pour le tour du joueur
    public void afficher_tour()
    {
        if (joueur_1.mon_tour)
        {
            //Handheld.Vibrate();
            Counter_Pieces_P1.SetActive(true);
            Counter_Pieces_P2.SetActive(false);

            Tour_P1.SetActive(true);
            Tour_P2.SetActive(false);
        }
        else if (joueur_2.mon_tour)
        {
            //Handheld.Vibrate();
            Counter_Pieces_P2.SetActive(true);
            Counter_Pieces_P1.SetActive(false);

            Tour_P2.SetActive(true);
            Tour_P1.SetActive(false);
        }
    }

    //affiche la fin du match
    public void afficher_fin_match()
    {
        afficher_score();
        Counter_Pieces_P2.SetActive(false);
        Tour_P2.SetActive(false);
        Tour_P1.SetActive(false);
        Counter_Pieces_P1.SetActive(true);

        ButtonPause.SetActive(false);
        //active les objets pour afficher la fin du match
        Fin_Match.SetActive(true);
        Background_Victoire.SetActive(true);

        if (joueur_1.recupere_resultat())
        {
            Text_Victoire.GetComponent<TMPro.TMP_Text>().text = "Victoire de " + joueur_1.recupere_le_nom() + " !!!";
        }
        else if (joueur_2.recupere_resultat())
        {
            Text_Victoire.GetComponent<TMPro.TMP_Text>().text = "Victoire de " + joueur_2.recupere_le_nom() + " !!!";
        }
        else
        {
            Text_Victoire.GetComponent<TMPro.TMP_Text>().text = "Match Nul !!!";
        }
    }

    #endregion

}
