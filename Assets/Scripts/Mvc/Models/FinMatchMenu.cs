using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Mvc.Models
{
    public class FinMatchMenu : MonoBehaviour
    {
        [SerializeField] private float delaiPourRejouer;
        [SerializeField] private Match match;
        [SerializeField] private GameObject menuFinMatch;
        [SerializeField] private GameObject backgroundVictoire;
        [SerializeField] private GameObject backgroundVictoireMini;
        [SerializeField] private TMPro.TMP_Text textVictoire;
        [SerializeField] private TMPro.TMP_Text textVictoireMini;


        public Match Match { get => match; set => match = value; }
        public TMP_Text TextVictoire { get => textVictoire; set => textVictoire = value; }
        public TMP_Text TextVictoireMini { get => textVictoireMini; set => textVictoireMini = value; }
        public float DelaiPourRejouer { get => delaiPourRejouer; set => delaiPourRejouer = value; }
        public GameObject MenuFinMatch { get => menuFinMatch; set => menuFinMatch = value; }
        public GameObject BackgroundVictoire { get => backgroundVictoire; set => backgroundVictoire = value; }
        public GameObject BackgroundVictoireMini { get => backgroundVictoireMini; set => backgroundVictoireMini = value; }

        //Name_P1.GetComponent<TMPro.TMP_Text>().text
        public void boutonQuitter()
        {
            if (SceneManager.GetActiveScene().name == "SceneMatch1vs1")
            {
                Fonctions.changerDeScene("SceneOffline");
            }
        }
        public void boutonRejouer()
        {
            Fonctions.activerObjet(backgroundVictoire);
            Fonctions.changerTexte(textVictoire);
            Fonctions.changerTexte(textVictoireMini);
            Fonctions.desactiverObjet(backgroundVictoireMini);
            Fonctions.activerObjet(match.PauseMenu.BoutonPausePrefab);
            match.rejouerMatch();
        }
        public void boutonVoirTable()
        {
            match.PauseMenu.EnPause = false;
            Fonctions.activerObjet(backgroundVictoireMini);
            Fonctions.changerTexte(textVictoireMini, textVictoire.text);
            Fonctions.desactiverObjet(backgroundVictoire);
            if (SceneManager.GetActiveScene().name == "SceneMatch1vs1")
            {
                match.Joueur1.Tour = Tour.MonTour;
                match.Joueur2.Tour = Tour.SonTour;
                match.OutilsJoueur.activerCompteurJoueur(1);
            }

        }
    }
}
