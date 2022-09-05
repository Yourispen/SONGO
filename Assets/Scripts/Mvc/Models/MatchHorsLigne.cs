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
            debuterMatch();
        }
        public override void debuterMatch()
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

        public override void initialiseJoueurs()
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

        public override void abandonMatch()
        {
            if (joueur1.Tour == Tour.MonTour)
            {
                resultatDuMatch = ResultatMatch.V2;
            }
            else if (joueur2.Tour == Tour.MonTour)
            {
                resultatDuMatch = ResultatMatch.V1;
            }
            finDuMatch();
        }
        public override void rejouerCoup(Case caseDepart)
        {
            if (joueur1.Tour == Tour.MonTour)
            {
                joueur1.Swipe.enabled = true;
                tourJ.activerTourjoueur(1);
            }
            else if (joueur2.Tour == Tour.MonTour)
            {
                joueur2.Swipe.enabled = true;
                tourJ.activerTourjoueur(2);
            }
        }

        public override void finDuMatch()
        {
            pauseMenu.EnPause = true;
            Fonctions.desactiverObjet(pauseMenu.BoutonPausePrefab);
            outilsJoueur.desactiverCompteursJoueurs();
            tourJ.desactiverToursjoueurs();
            Fonctions.activerObjet(finMatchMenu.MenuFinMatch);
            Fonctions.activerAudioSourceVictoire();
            if (resultatDuMatch == ResultatMatch.V1)
            {
                joueur1.victoireJoueur();
                joueur2.defaiteJoueur();
                resultatDuMatch = ResultatMatch.V1;
                Fonctions.changerTexte(finMatchMenu.TextVictoire, "Victoire de " + joueur1.Surnom + " !!!");
            }
            else if (resultatDuMatch == ResultatMatch.V2)
            {
                joueur2.victoireJoueur();
                joueur1.defaiteJoueur();
                resultatDuMatch = ResultatMatch.V2;
                Fonctions.changerTexte(finMatchMenu.TextVictoire, "Victoire de " + joueur2.Surnom + " !!!");

            }
            scoreMatch.afficherScoreMatch();
            etatDuMatch = EtatMatch.Fin;
        }


    }
}