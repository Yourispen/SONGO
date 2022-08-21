using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public class MatchHorsLigne : Match
    {
        [SerializeField] private int id;
        [SerializeField] private string scoreMatch;
        public static int nbJoueur;
        [SerializeField] private GameObject joueurOffPrefab;
        [SerializeField] private JoueurOff joueur1;
        [SerializeField] private JoueurOff joueur2;
        public string ScoreMatch { get => scoreMatch; set => scoreMatch = value; }
        public int Id { get => id; set => id = value; }
        public JoueurOff Joueur1 { get => joueur1; set => joueur1 = value; }
        public JoueurOff Joueur2 { get => joueur2; set => joueur2 = value; }

        void OnEnable()
        {
            nbJoueur = 0;
            tableMatch = Fonctions.instancierObjet(songoMatchPrefab).GetComponentInChildren<Table>();
            tableMatch.Match = ((Match)this);
        }

        private void Start()
        {
            typeDuMatch = TypeMatch.HorsLigne1vs1;
            initialiseJoueurs();


        }

        protected override void initialiseJoueurs()
        {
            this.joueur1 = Fonctions.instancierObjet(joueurOffPrefab).GetComponent<JoueurOff>();
            this.joueur1.MatchHorsLigne = this;
            this.joueur1.gameObject.name = "joueur" + (nbJoueur + 1).ToString();
            this.joueur1.NumPosition = 1;
            nbJoueur += 1;
            this.joueur2 = Fonctions.instancierObjet(joueurOffPrefab).GetComponent<JoueurOff>();
            this.joueur2.MatchHorsLigne = this;
            this.joueur2.gameObject.name = "joueur" + (nbJoueur + 1).ToString();
            this.joueur2.NumPosition = 2;
        }
        public override void tourJoueur(int numPosition)
        {
            if (numPosition == 1)
            {
                joueur1.Tour = Tour.MonTour;
                joueur2.Tour = Tour.SonTour;
            }
            else
            {
                joueur2.Tour = Tour.MonTour;
                joueur1.Tour = Tour.SonTour;
            }
        }
        public override void verifierEtatDuMatch(int idCase)
        {
            if (tableMatch.ListeCases[14].ListePions.Count > 35)
            {
                etatDuMatch = EtatMatch.Fin;
            }
            else if (tableMatch.ListeCases[15].ListePions.Count > 35)
            {
                etatDuMatch = EtatMatch.Fin;
            }
            else if (tableMatch.ListeCases[14].ListePions.Count == 35 && tableMatch.ListeCases[15].ListePions.Count == 35)
            {

                etatDuMatch = EtatMatch.Fin;
            }
            else
            {
                if (idCase < 7)
                {
                    tourJoueur(2);
                }
                else
                {
                    tourJoueur(1);
                }

            }
        }
    }
}