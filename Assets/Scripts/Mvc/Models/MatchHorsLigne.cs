using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
using Mvc.Controllers;

namespace Mvc.Models
{
    public class MatchHorsLigne : Match
    {
        public static int nbJoueur;
        [SerializeField] private GameObject joueurOffPrefab;
        [SerializeField] private MatchHorsLigneController matchHorsLigneController;
        [SerializeField] private Enregistrement enregistrement;
        public MatchHorsLigneController MatchHorsLigneController { get => matchHorsLigneController; set => matchHorsLigneController = value; }

        void OnEnable()
        {
            matchRejoue = false;
            nbJoueur = 0;
            tableMatch = Fonctions.instancierObjet(songoMatchPrefab).GetComponentInChildren<Table>();
            tableMatch.Match = ((Match)this);

            pauseMenu = Fonctions.instancierObjet(GameObject.Find("PauseMenu")).GetComponentInChildren<PauseMenu>();
            pauseMenu.Match = ((Match)this);

            finMatchMenu = Fonctions.instancierObjet(GameObject.Find("FinMatchMenu")).GetComponentInChildren<FinMatchMenu>();
            finMatchMenu.Match = ((Match)this);

            tourJ = Fonctions.instancierObjet(GameObject.Find("TourJoueur")).GetComponentInChildren<TourJoueur>();
            tourJ.Match = ((Match)this);

            outilsJoueur = Fonctions.instancierObjet(GameObject.Find("OutilsJoueur")).GetComponentInChildren<OutilsJoueur>();
            outilsJoueur.Match = ((Match)this);

            scoreMatch = Fonctions.instancierObjet(GameObject.Find("ScoreMatch")).GetComponentInChildren<ScoreMatch>();
            scoreMatch.Match = ((Match)this);

            enregistrement = Fonctions.instancierObjet(GameObject.Find("Enregistrement")).GetComponentInChildren<Enregistrement>();
            enregistrement.Match = ((Match)this);

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
            joueur1.Surnom = enregistrement.NomJoueur1;
            joueur1.CouleurTouche = joueur1.CouleurToucheJoueur1;
            nbJoueur += 1;
            joueur2 = ((Joueur)Fonctions.instancierObjet(joueurOffPrefab).GetComponent<JoueurOff>());
            joueur2.Match = ((Match)this);
            joueur2.gameObject.name = "joueur" + (nbJoueur + 1).ToString();
            joueur2.Id = "2";
            joueur2.Surnom = enregistrement.NomJoueur2;
            joueur2.CouleurTouche = joueur2.CouleurToucheJoueur2;
            Fonctions.desactiverObjet(enregistrement.gameObject);
            outilsJoueur.afficherNomsJoueurs(joueur1.Surnom, joueur2.Surnom);
        }
        public override void tourJoueur(int idCaseDepart)
        {
            if (idCaseDepart >= 7)
            {
                joueur1.Tour = Tour.MonTour;
                joueur1.Swipe.enabled = true;
                joueur2.Tour = Tour.SonTour;
                outilsJoueur.activerCompteurJoueur(1);
                tourJ.activerTourjoueur(1);
            }
            else
            {
                joueur2.Tour = Tour.MonTour;
                joueur2.Swipe.enabled = true;
                joueur1.Tour = Tour.SonTour;
                outilsJoueur.activerCompteurJoueur(2);
                tourJ.activerTourjoueur(2);
            }
        }
        public override void verifierEtatDuMatch(Case caseDepart)
        {
            if (tableMatch.ListeCases[14].ListePions.Count > 35)
            {
                etatDuMatch = EtatMatch.Fin;
                finDuMatch();
            }
            else if (tableMatch.ListeCases[15].ListePions.Count > 35)
            {
                etatDuMatch = EtatMatch.Fin;
                finDuMatch();
            }
            else if (tableMatch.ListeCases[14].ListePions.Count == 35 && tableMatch.ListeCases[15].ListePions.Count == 35)
            {
                pauseMenu.EnPause = false;
                Fonctions.desactiverObjet(pauseMenu.BoutonPausePrefab);
                Fonctions.activerObjet(finMatchMenu.MenuFinMatch);
                Fonctions.changerTexte(finMatchMenu.TextVictoire, "Match Nul !!!");
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
            pauseMenu.EnPause = true;
            Fonctions.desactiverObjet(pauseMenu.BoutonPausePrefab);
            outilsJoueur.desactiverCompteursJoueurs();
            tourJ.desactiverToursjoueurs();
            Fonctions.activerObjet(finMatchMenu.MenuFinMatch);
            if (joueur1.Tour == Tour.MonTour)
            {
                joueur1.victoireJoueur();
                joueur2.defaiteJoueur();
                resultatDuMatch = ResultatMatch.V1;
                Fonctions.changerTexte(finMatchMenu.TextVictoire, "Victoire de " + joueur1.Surnom + " !!!");
            }
            else if (joueur2.Tour == Tour.MonTour)
            {
                joueur2.victoireJoueur();
                joueur1.defaiteJoueur();
                resultatDuMatch = ResultatMatch.V2;
                Fonctions.changerTexte(finMatchMenu.TextVictoire, "Victoire de " + joueur2.Surnom + " !!!");

            }
            scoreMatch.afficherScoreMatch();
            etatDuMatch = EtatMatch.Fin;
        }
        public override void rejouerMatch()
        {
            matchRejoue = true;
            Fonctions.activerObjet(pauseMenu.BoutonPausePrefab);
            Fonctions.desactiverObjet(finMatchMenu.MenuFinMatch);
            joueur1.NumPosition = joueur1.NumPosition == 1 ? 2 : 1;
            joueur2.NumPosition = joueur2.NumPosition == 1 ? 2 : 1;
            StartCoroutine(partagerLesPions());
        }

        public override void abandonMatch()
        {
            joueur1.Tour = joueur1.Tour == Tour.MonTour ? Tour.SonTour : Tour.MonTour;
            joueur2.Tour = joueur2.Tour == Tour.MonTour ? Tour.SonTour : Tour.MonTour;
            finDuMatch();
        }
    }
}