using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public abstract class Joueur : ModelFirebase
    {
        [SerializeField] protected string id;
        [SerializeField] protected string surnom;
        [SerializeField] protected int numPosition;
        [SerializeField] protected Tour tour;
        [SerializeField] protected Match match;
        [SerializeField] protected Material couleurToucheJoueur1;
        [SerializeField] protected Material couleurToucheJoueur2;
        [SerializeField] protected Material couleurTouche;
        [SerializeField] protected Swipe swipe;
        [SerializeField] protected int nombreVictoire;


        public Joueur()
        {
            numPosition = 0;
            tour = Tour.Aucun;
        }

        public string Surnom { get => surnom; set => surnom = value; }
        public int NumPosition { get => numPosition; set => numPosition = value; }
        public Tour Tour { get => tour; set => tour = value; }
        public Match Match { get => match; set => match = value; }
        public Swipe Swipe { get => swipe; set => swipe = value; }
        public string Id { get => id; set => id = value; }
        public Material CouleurToucheJoueur1 { get => couleurToucheJoueur1; set => couleurToucheJoueur1 = value; }
        public Material CouleurToucheJoueur2 { get => couleurToucheJoueur2; set => couleurToucheJoueur2 = value; }
        public Material CouleurTouche { get => couleurTouche; set => couleurTouche = value; }
        public int NombreVictoire { get => nombreVictoire; set => nombreVictoire = value; }

        public void jouerMatch(Case caseDepart)
        {
            swipe.enabled = false;
            match.jouerTable(caseDepart);
        }
        public abstract void victoireJoueur();
        public abstract void defaiteJoueur();
        public int premierAJouer()
        {
            numPosition = 1;
            tour = Tour.MonTour;
            swipe.enabled = true;
            return numPosition;
        }
        public int deuxiemeAJouer()
        {
            numPosition = 2;
            tour = Tour.SonTour;
            swipe.enabled = false;
            return numPosition;
        }
        public abstract void abandonJoueur();
    }
}
