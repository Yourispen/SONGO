using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Facebook.Unity;
using Firebase.Auth;
using System;
using Mvc.Core;
using Mvc.Controllers;

namespace Mvc.Controllers
{
    public class SceneController : MonoBehaviour
    {
        public GameObject chargement;
        public GameObject msgScene;
        [SerializeField] private GameObject connexionCompteControllerPrefab;
        [SerializeField] private GameObject connexionInternetPrefab;
        [SerializeField] private ConnexionInternet connexionInternet;
        [SerializeField] private ConnexionCompteController connexionCompteController;
        [SerializeField] private string textSaisiSurnom;
        [SerializeField] private string nomScene;


        void Awake()
        {
            //PlayerPrefs.SetInt("etatConnexionCompte", 1);
            //Debug.Log(PlayerPrefs.GetInt("etatConnexionCompte"));return;
            connexionInternet = Fonctions.instancierObjet(connexionInternetPrefab).GetComponent<ConnexionInternet>();

            if (SceneManager.GetActiveScene().name == "SceneMenuPrincipal")
            {
                if (PlayerPrefs.HasKey("etatConnexionCompte"))
                {
                    Fonctions.desactiverObjet(GameObject.Find("PageDeSaisiDuSurnom"));
                    Fonctions.desactiverObjet(GameObject.Find("PageDeConnexionCompte"));
                    return;
                }
                Fonctions.debutChargement();
                if (PlayerPrefs.HasKey("surnom"))
                {
                    Debug.Log(PlayerPrefs.GetString("surnom"));
                    Fonctions.desactiverObjet(GameObject.Find("PageDeSaisiDuSurnom"));
                    Fonctions.desactiverObjet(GameObject.Find("PageDeConnexionCompte"));
                    Fonctions.finChargement();
                    if (PlayerPrefs.GetInt("idConnexionCompte") == 1)
                    {
                        Fonctions.afficherMsgScene(FacebookAuth.msgConnexion, "succes");
                    }

                }
                else
                {
                    connexionCompteController = connexionCompteControllerPrefab.GetComponent<ConnexionCompteController>();
                    Fonctions.finChargement();
                }
                //PlayerPrefs.DeleteAll();
            }
        }
        void Start()
        {
            Fonctions.nonVeille();
            //Fonctions.afficherMsgScene("J'ai perdu", "erreur");

        }
        public void saisirSurnom(string textSaisiSurnom)
        {
            this.textSaisiSurnom = textSaisiSurnom;
        }
        public void boutonEntrerMenuPrincipal()
        {
            Fonctions.finChargement();
            if (textSaisiSurnom.Length > 1)
            {
                Fonctions.desactiverObjet(chargement);
                PlayerPrefs.SetString("surnom", textSaisiSurnom);
                PlayerPrefs.SetString("role", "ROLE_CLIENT");
                if (PlayerPrefs.GetInt("idConnexionCompte") == 0)
                {
                    Fonctions.finChargement();

                }
                else if (PlayerPrefs.GetInt("idConnexionCompte") == 1)
                {
                    GameObject.Find("joueurOnController").GetComponent<JoueurOnController>().ajouter();
                }
                Fonctions.desactiverObjet(GameObject.Find("PageDeSaisiDuSurnom"));
                PlayerPrefs.SetInt("etatConnexionCompte", 1);

            }
        }
        void OnApplicationQuit()
        {
            PlayerPrefs.DeleteKey("etatConnexionCompte");
        }

        public void boutonPlay()
        {
            nomScene = "ScenePlay";
            //Fonctions.changerDeScene(nomScene);
        }
        public void boutonShop()
        {

        }
        public void boutonSetting()
        {
            nomScene = "SceneSetting";
            //Fonctions.changerDeScene(nomScene);
        }
        public void boutonRules()
        {

        }
        public void boutonClose()
        {
            Fonctions.fermerApplication();
        }
        public void boutonCredits()
        {

        }
        public void boutonHorsLigne()
        {
            nomScene = "SceneOffline";
            Fonctions.changerDeScene(nomScene);
        }
        public void boutonEnligne()
        {
            if (ConnexionInternet.connect)
            {
                nomScene = "SceneOnline";
                Fonctions.changerDeScene(nomScene);
            }
            else
            {
                Fonctions.afficherMsgScene(ConnexionInternet.msgNonConnecte, "erreur");
            }
        }
        public void boutonRetourScene()
        {
            if (SceneManager.GetActiveScene().name == "ScenePlay")
            {
                nomScene = "SceneMenuPrincipal";
            }
            Fonctions.changerDeScene(nomScene);
        }

    }
}
