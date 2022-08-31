using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mvc.Core;
using TMPro;

namespace Mvc.Models
{
    public class TourJoueur : MonoBehaviour
    {
        [SerializeField] private GameObject tour1;
        [SerializeField] private Image backgroundTour1;
        [SerializeField] private TMPro.TMP_Text textTour1;
        [SerializeField] private GameObject tour2;
        [SerializeField] private Image backgroundTour2;
        [SerializeField] private TMPro.TMP_Text textTour2;
        [SerializeField] private Match match;

        public GameObject Tour1 { get => tour1; set => tour1 = value; }
        public Image BackgroundTour1 { get => backgroundTour1; set => backgroundTour1 = value; }
        public TMP_Text TextTour1 { get => textTour1; set => textTour1 = value; }
        public GameObject Tour2 { get => tour2; set => tour2 = value; }
        public Image BackgroundTour2 { get => backgroundTour2; set => backgroundTour2 = value; }
        public TMP_Text TextTour2 { get => textTour2; set => textTour2 = value; }
        public Match Match { get => match; set => match = value; }

        void Start()
        {
            if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                {
                    Fonctions.changerTexte(textTour2, "à lui de jouer");
                }
                else
                {
                    Fonctions.changerTexte(textTour1, "à lui de jouer");
                }
            }
        }
        public void activerTourjoueur(int joueur)
        {
            if (joueur == 1)
            {
                Fonctions.desactiverObjet(tour2);
                Fonctions.activerObjet(tour1);
            }
            else
            {
                Fonctions.desactiverObjet(tour1);
                Fonctions.activerObjet(tour2);
            }
        }
        public void desactiverToursjoueurs()
        {
            Fonctions.desactiverObjet(tour2);
            Fonctions.desactiverObjet(tour1);
        }
    }
}
