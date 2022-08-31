using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        [SerializeField] private GameObject matchHorsLigneControllerPrefab;
        [SerializeField] private MatchHorsLigneController matchHorsLigneController;
        [SerializeField] private GameObject matchEnLigneControllerPrefab;
        [SerializeField] private MatchEnLigneController matchEnLigneController;
        [SerializeField] private GameObject joueurOnControllerPrefab;
        [SerializeField] private JoueurOnController joueurOnController;
        [SerializeField] PhotonManager photonManager;

        public MatchEnLigneController MatchEnLigneController { get => matchEnLigneController; set => matchEnLigneController = value; }
        public MatchHorsLigneController MatchHorsLigneController { get => matchHorsLigneController; set => matchHorsLigneController = value; }
        public ConnexionCompteController ConnexionCompteController { get => connexionCompteController; set => connexionCompteController = value; }
        public ConnexionInternet ConnexionInternet { get => connexionInternet; set => connexionInternet = value; }
        public PhotonManager PhotonManager { get => photonManager; set => photonManager = value; }
        public JoueurOnController JoueurOnController { get => joueurOnController; set => joueurOnController = value; }

        void Awake()
        {
            //PlayerPrefs.SetInt("etatConnexionCompte", 1);
            //PlayerPrefs.DeleteAll(); return;
            //PlayerPrefs.SetString("id", "z6hhsbMJRyaPRGSaDTHpNQ8QiKj2");
            //PlayerPrefs.SetString("surnom","Glenneriss"); return;
            connexionInternet = Fonctions.instancierObjet(connexionInternetPrefab).GetComponent<ConnexionInternet>();

            if (Fonctions.sceneActuelle("SceneMenuPrincipal"))
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
                        PlayerPrefs.SetInt("etatConnexionCompte", 1);
                    }

                }
                else
                {
                    connexionCompteController = connexionCompteControllerPrefab.GetComponent<ConnexionCompteController>();
                    Fonctions.finChargement();
                }
                //PlayerPrefs.DeleteAll();
            }
            else if (Fonctions.sceneActuelle("SceneMatch1vs1"))
            {

            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                /*matchEnLigneController = Fonctions.instancierObjet(matchEnLigneControllerPrefab).GetComponentInChildren<MatchEnLigneController>();
                matchEnLigneController.SceneController=this;
                joueurOnController = Fonctions.instancierObjet(joueurOnControllerPrefab).GetComponentInChildren<JoueurOnController>();
                joueurOnController.SceneController=this;*/
                photonManager.instancierUnJoueur();
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
            Fonctions.changerDeScene(nomScene);
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
        public void bouton1vs1()
        {
            nomScene = "SceneMatch1vs1";
            Fonctions.changerDeScene(nomScene);
        }
        public void bouton1vsIA()
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
                nomScene = "SceneLobbyMatchEnLigne";
                Fonctions.changerDeScene(nomScene);
            }
            else
            {
                Fonctions.afficherMsgScene(ConnexionInternet.msgNonConnecte, "erreur");
            }
        }
        public void boutonRetourScene()
        {
            if (Fonctions.sceneActuelle("ScenePlay"))
            {
                nomScene = "SceneMenuPrincipal";
            }
            else if (Fonctions.sceneActuelle("SceneOffline"))
            {
                nomScene = "ScenePlay";
            }
            Fonctions.changerDeScene(nomScene);
        }
        public void commencerMatch1vs1()
        {
            matchHorsLigneController = Fonctions.instancierObjet(matchHorsLigneControllerPrefab).GetComponent<MatchHorsLigneController>();
            matchHorsLigneController.SceneController = this;
        }

    }
}
