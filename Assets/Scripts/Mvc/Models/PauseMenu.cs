using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mvc.Core;
using Mvc.Controllers;
using Photon.Pun;

namespace Mvc.Models
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private bool enPause;
        [SerializeField] private Match match;
        [SerializeField] private GameObject menuPausePrefab;
        [SerializeField] private GameObject boutonPausePrefab;


        public bool EnPause { get => enPause; set => enPause = value; }
        public Match Match { get => match; set => match = value; }
        public GameObject MenuPausePrefab { get => menuPausePrefab; set => menuPausePrefab = value; }
        public GameObject BoutonPausePrefab { get => boutonPausePrefab; set => boutonPausePrefab = value; }

        void Start()
        {
            enPause = false;
        }

        public void boutonPause()
        {
            enPause = true;
            match.TourJ.desactiverToursjoueurs();
            match.OutilsJoueur.desactiverCompteursJoueurs();
            Fonctions.activerObjet(menuPausePrefab);
            Fonctions.desactiverObjet(boutonPausePrefab);

        }
        public void BoutonAbondonner()
        {
            Fonctions.desactiverObjet(menuPausePrefab);
            if (SceneManager.GetActiveScene().name == "SceneMatch1vs1")
            {
                match.abandonMatch();
            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                if (!ConnexionInternet.connect)
                    return;
                //photonView.RPC("RPC_jouer", RpcTarget.AllBuffered, case_de_depart);
                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                {
                    match.Joueur1.PhotonView.RPC("abandonJoueur", RpcTarget.AllBuffered, PlayerPrefs.GetInt("numPositionMatchEnCours"));
                }
                else
                {
                    match.Joueur2.PhotonView.RPC("abandonJoueur", RpcTarget.AllBuffered, PlayerPrefs.GetInt("numPositionMatchEnCours"));
                }
            }
        }
        public void BoutonContinuer()
        {
            enPause = false;
            if (match.Joueur1.Tour == Tour.MonTour)
            {
                match.TourJ.activerTourjoueur(1);
                match.OutilsJoueur.activerCompteurJoueur(1);

            }
            else
            {
                match.TourJ.activerTourjoueur(2);
                match.OutilsJoueur.activerCompteurJoueur(2);
            }

            /*  else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
             {
                 if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                 {
                     match.TourJ.activerTourjoueur(1);
                     match.OutilsJoueur.activerCompteurJoueur(1);
                     if (match.Joueur1.Tour == Tour.MonTour)
                     {
                         match.TourJ.activerTourjoueur(1);
                         match.OutilsJoueur.activerCompteurJoueur(1);

                     }
                     else
                     {
                         match.TourJ.activerTourjoueur(2);
                         match.OutilsJoueur.activerCompteurJoueur(2);
                     }
                 }
                 else
                 {
                     match.TourJ.activerTourjoueur(2);
                     match.OutilsJoueur.activerCompteurJoueur(2);
                     if (match.Joueur1.Tour == Tour.MonTour)
                     {
                         match.TourJ.activerTourjoueur(1);
                         match.OutilsJoueur.activerCompteurJoueur(1);

                     }
                     else
                     {
                         match.TourJ.activerTourjoueur(2);
                         match.OutilsJoueur.activerCompteurJoueur(2);
                     }
                 }
             } */
            Fonctions.activerObjet(boutonPausePrefab);
            Fonctions.desactiverObjet(menuPausePrefab);
        }
        public void BoutonQuitter()
        {
            if (SceneManager.GetActiveScene().name == "SceneMatch1vs1")
            {
                Fonctions.changerDeScene("SceneOffline");
            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                ((MatchEnLigne)match).MatchEnLigneController.SceneController.PhotonManager.QuitterMatch = true;
                PhotonManager.connectePhoton = false;
                ((MatchEnLigne)match).MatchEnLigneController.SceneController.PhotonManager.quitterLobby();
            }
        }
    }
}
