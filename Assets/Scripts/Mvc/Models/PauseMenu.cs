using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mvc.Core;
using Mvc.Controllers;

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
            Fonctions.activerObjet(boutonPausePrefab);
            Fonctions.desactiverObjet(menuPausePrefab);
        }
        public void BoutonQuitter()
        {
            if (SceneManager.GetActiveScene().name == "SceneMatch1vs1")
            {
                Fonctions.changerDeScene("SceneOffline");
            }
        }
    }
}
