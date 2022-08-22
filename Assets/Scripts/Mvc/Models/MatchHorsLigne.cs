using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public class MatchHorsLigne : Match
    {
        [SerializeField] private string scoreMatch;
        public static int nbJoueur;
        [SerializeField] private GameObject joueurOffPrefab;
        public string ScoreMatch { get => scoreMatch; set => scoreMatch = value; }

        void OnEnable()
        {
            matchRejoue = false;
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
            joueur1 = ((Joueur)Fonctions.instancierObjet(joueurOffPrefab).GetComponent<JoueurOff>());
            joueur1.Match = ((Match)this);
            joueur1.gameObject.name = "joueur" + (nbJoueur + 1).ToString();
            joueur1.Id = "1";
            joueur1.CouleurTouche = joueur1.CouleurToucheJoueur1;
            nbJoueur += 1;
            joueur2 = ((Joueur)Fonctions.instancierObjet(joueurOffPrefab).GetComponent<JoueurOff>());
            joueur2.Match = ((Match)this);
            joueur2.gameObject.name = "joueur" + (nbJoueur + 1).ToString();
            joueur2.Id = "2";
            joueur2.CouleurTouche = joueur2.CouleurToucheJoueur2;
        }
        public override void tourJoueur(int idCaseDepart)
        {
            if (idCaseDepart >= 7)
            {
                joueur1.Tour = Tour.MonTour;
                joueur1.Swipe.enabled = true;
                joueur2.Tour = Tour.SonTour;
            }
            else
            {
                joueur2.Tour = Tour.MonTour;
                joueur2.Swipe.enabled = true;
                joueur1.Tour = Tour.SonTour;
            }
        }
        public override void verifierEtatDuMatch(Case caseDepart)
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
                tourJoueur(caseDepart.Id);

            }
        }
        public override void jouerTable(Case caseDepart)
        {
            if (etatDuMatch == EtatMatch.Debut || etatDuMatch == EtatMatch.Fin || caseDepart.Id == 14 || caseDepart.Id == 15)
            {
                rejouerCoup();
            }
            else
            {
                tableMatch.parcourirLaTable(caseDepart);
            }
        }
        public override void rejouerCoup()
        {
            if (joueur1.Tour == Tour.MonTour)
            {
                joueur1.Swipe.enabled = true;
            }
            else if (joueur2.Tour == Tour.MonTour)
            {
                joueur2.Swipe.enabled = true;
            }
        }
        public override void finDuMatch()
        {
            if (joueur1.Tour == Tour.MonTour)
            {
                joueur1.victoireJoueur();
                resultatDuMatch = ResultatMatch.V1;
            }
            else if (joueur2.Tour == Tour.MonTour)
            {
                joueur2.victoireJoueur();
                resultatDuMatch = ResultatMatch.V1;
            }
            etatDuMatch = EtatMatch.Fin;
        }
        public override void rejouerMatch()
        {
            joueur1.NumPosition = joueur1.NumPosition == 1 ? 2 : 1;
            joueur2.NumPosition = joueur2.NumPosition == 1 ? 2 : 1;
            StartCoroutine(partagerLesPions());
        }
    }
}