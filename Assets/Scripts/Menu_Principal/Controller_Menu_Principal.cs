using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Mvc.Core;
using Mvc.Entities;
using Mvc.Controllers;

public class Controller_Menu_Principal : MonoBehaviour
{
    [Header("Enregistrement")]
    [SerializeField] private GameObject Registrations_Menu_Principal;

    [Header("Menu")]
    [SerializeField] private GameObject Menu;

    [SerializeField]
    private Model model;

    [SerializeField] private GameObject fbAuthPrefab;
    [SerializeField] private FacebookAuth fbAuth;//= GameObject.Find("FacebookAuth").GetComponent<FacebookAuth>();

    [SerializeField] private GameObject joueurOnControllerPrefab;
    [SerializeField] private JoueurOnController joueurOnController;

    [SerializeField] private GameObject matchEnligneControllerPrefab;
    [SerializeField] private MatchEnLigneController matchEnligneController;
    public string nom_joueur;

    private void Awake()
    {
        /* if (PlayerPrefs.GetString("pseudo") !="")
         {
             //PlayerPrefs.SetString("pseudo", "");
             Menu.SetActive(true);
             Registrations_Menu_Principal.SetActive(false);
         }*/
    }

    private void OnEnable()
    {
        //matchEnligneController = Fonctions.instancierObjet(matchEnligneControllerPrefab).GetComponent<MatchEnLigneController>();
        fbAuth = Fonctions.instancierObjet(fbAuthPrefab).GetComponent<FacebookAuth>();
        //joueurOnController = Fonctions.instancierObjet(joueurOnControllerPrefab).GetComponent<JoueurOnController>();
    }

    private void Update()
    {
        /*if (fbAuth.Model.DatabaseRealtime.Statut != 0)
        {
            fbAuth.Model.DatabaseRealtime.Statut = 0;
            Debug.Log("nombre de donn�es recup�r�es : "+fbAuth.Model.DatabaseRealtime.Resultat.Count);
            fbAuth.getProfilPlayer(fbAuth.Model.DatabaseRealtime.Resultat);
        }*/
    }

    public void bouton_enter()
    {
        if (nom_joueur.Length > 3)
        {
            /*PlayerPrefs.SetString("surnom", "John Doe");
            PlayerPrefs.SetString("dateInscription", DateTime.Now.ToString("yyyy'-'MM'-'dd"));
            //print(PlayerPrefs.GetString("dateInscription"));
            PlayerPrefs.SetString("heureInscription", DateTime.Now.ToString("HH:mm"));
            PlayerPrefs.SetString("email", "johndoe@gmail.com");
            PlayerPrefs.SetInt("idNiveau", 2);
            PlayerPrefs.SetInt("idConnexionCompte", 1);
            PlayerPrefs.SetString("id", "yyyyyy");
            //PlayerPrefs.SetString("id", "xxxxxx");
            joueurOnController.recupereJoueurConnecte();*/

            //PlayerPrefs.SetInt("idMatchEnLigne",100001);
            //PlayerPrefs.SetString("idJoueur2", "IuOIAbF713bMzDKf4fAQAHh0C6C3");
            //PlayerPrefs.SetString("idAdversaire", "ZXJAxuUtYpRxvEEqxxqCBXOwOMb2");
            //PlayerPrefs.SetString("idVainqueur", PlayerPrefs.GetString("id"));
            //matchEnligneController.recupererScoreDuMatch();

            PlayerPrefs.SetString("surnom", nom_joueur);
            fbAuth.loginBtnForFB();


            //d�sactive le menu enregistrement
            //Registrations_Menu_Principal.SetActive(false);

            //cr�er le nouveau  joueur
            //model.insertPlayer();

            //active le menu
            //Menu.SetActive(true);

            /*PlayerPrefs.SetString("userName", nom_joueur);
            //name = user.DisplayName;
            PlayerPrefs.SetString("email", nom_joueur+"@gmail.com");
            //email = user.Email;
            //System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            //uid = user.UserId;
            PlayerPrefs.SetString("id", "0000002");
            model.insertPlayer();*/


            /* if(fbAuth.User!=null)
                 fbAuth.Model.selectById("user",fbAuth.User.UserId);
            */
            //Debug.Log(liste.Count);
            //Debug.Log(liste[0]);
            /*foreach (var value in liste)
            {
                Debug.Log(value);
            }*/

            //model.selectAll("user");

        }
    }

    public void bouton_play()
    {
        //va � la sc�ne Play
        SceneManager.LoadScene("Play");
    }

    public void bouton_shop()
    {
        //va � la sc�ne Shop
        //SceneManager.LoadScene("Shop");
    }

    public void bouton_setting()
    {
        //va � la sc�ne Setting
        SceneManager.LoadScene("Setting");
    }

    public void bouton_rules()
    {
        //va � la sc�ne Rules
        //SceneManager.LoadScene("Rules");
    }

    public void bouton_credits()
    {
        //databaseRealtime.recuperer_donnees();
        //va � la sc�ne Credits
        //SceneManager.LoadScene("Credits");
    }

    public void saisir_nom_joueur(string nom)
    {
        nom_joueur = nom;
    }

    public void bouton_close()
    {
        //quitter l'application
        Application.Quit();
    }
}
