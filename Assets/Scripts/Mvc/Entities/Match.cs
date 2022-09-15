using Mvc.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Entities
{
    public abstract class Match : ModelFirebase
    {
        public const int NOMBREMAXJOUEUR = 2;
        [SerializeField] protected string id;
        [SerializeField] protected ResultatMatch resultatDuMatch;
        [SerializeField] protected EtatMatch etatDuMatch;
        [SerializeField] protected TypeMatch typeDuMatch;
        [SerializeField] protected GameObject songoMatchPrefab;
        [SerializeField] protected Table tableMatch;
        [SerializeField] protected PauseMenu pauseMenu;
        [SerializeField] protected FinMatchMenu finMatchMenu;
        [SerializeField] protected List<GameObject> typePionPrefab = new List<GameObject>();
        [SerializeField] protected List<Pion> listePions = new List<Pion>();
        [SerializeField] protected Joueur joueur1;// = GameObject.Find("JoueurConnecte").GetComponent<JoueurConnecte>();

        [SerializeField] protected Joueur joueur2;// = GameObject.Find("JoueurNonConnecte").GetComponent<JoueurNonConnecte>();
        [SerializeField] protected bool matchRejoue;
        [SerializeField] protected ScoreMatch scoreMatch;
        [SerializeField] protected OutilsJoueur outilsJoueur;
        [SerializeField] protected TourJoueur tourJ;



        protected Match()
        {
            //this.pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        }

        public ResultatMatch ResultatDuMatch { get => resultatDuMatch; set => resultatDuMatch = value; }
        public EtatMatch EtatDuMatch { get => etatDuMatch; set => etatDuMatch = value; }
        public TypeMatch TypeDuMatch { get => typeDuMatch; set => typeDuMatch = value; }
        public Table TableMatch { get => tableMatch; set => tableMatch = value; }
        public Joueur Joueur1 { get => joueur1; set => joueur1 = value; }
        public Joueur Joueur2 { get => joueur2; set => joueur2 = value; }
        public string Id { get => id; set => id = value; }
        public PauseMenu PauseMenu { get => pauseMenu; set => pauseMenu = value; }
        public FinMatchMenu FinMatchMenu { get => finMatchMenu; set => finMatchMenu = value; }
        public List<GameObject> TypePionPrefab { get => typePionPrefab; set => typePionPrefab = value; }
        public ScoreMatch ScoreMatch { get => scoreMatch; set => scoreMatch = value; }
        public OutilsJoueur OutilsJoueur { get => outilsJoueur; set => outilsJoueur = value; }
        public TourJoueur TourJ { get => tourJ; set => tourJ = value; }
        public bool MatchRejoue { get => matchRejoue; set => matchRejoue = value; }

        public IEnumerator instancierLesPions()
        {
            etatDuMatch = EtatMatch.Debut;
            for (int i = 0; i < 70; i++)
            {
                listePions.Add(Fonctions.instancierObjet(typePionPrefab[PlayerPrefs.GetInt("typePion")], tableMatch.Caisse.transform.position + new Vector3(Random.Range(-1.9f, 1.9f), 5f, Random.Range(-1.9f, 1.9f))).GetComponent<Pion>());
                listePions[i].gameObject.name = "pion" + (i + 1).ToString();
                yield return new WaitForSeconds(Case.tempsDepotCaisse);
            }
            StartCoroutine(partagerLesPions());
        }
        public IEnumerator partagerLesPions()
        {
            if (!Fonctions.sceneActuelle("SceneMatchEnLigne"))
                Fonctions.finChargement();
            tableMatch.reinitialiseCases();
            int i = 0;
            while (i < 70)
            {
                for (int j = 0; j < 14; j++)
                {
                    listePions[i].transform.position = tableMatch.ListeCases[j].transform.position;
                    tableMatch.ListeCases[j].ajouterPion(listePions[i]);
                    listePions[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    i += 1;
                }
                yield return new WaitForSeconds(Case.tempsAttente);
            }
            if (matchRejoue)
            {
                if (joueur1.NumPosition == 1)
                {
                    joueur1.premierAJouer();
                    joueur2.deuxiemeAJouer();
                    outilsJoueur.activerCompteurJoueur(1);
                    tourJ.activerTourjoueur(1);
                }
                else if (joueur2.NumPosition == 1)
                {
                    joueur2.premierAJouer();
                    joueur1.deuxiemeAJouer();
                    outilsJoueur.activerCompteurJoueur(2);
                    tourJ.activerTourjoueur(2);
                }
            }
            else
            {
                joueur1.premierAJouer();
                joueur2.deuxiemeAJouer();
                outilsJoueur.activerCompteurJoueur(1);
                tourJ.activerTourjoueur(1);
            }
            scoreMatch.afficherScoreMatch();
            etatDuMatch = EtatMatch.EnCours;
            pauseMenu.EnPause = false;
            /*if (SceneManager.GetActiveScene().name == "SceneMatch1vs1")
            {

            }
            else if (SceneManager.GetActiveScene().name == "SceneMatchEnLigne")
            {

            }*/

        }
        public void verifierEtatDuMatch(Case caseDepart)
        {
            if (tableMatch.ListeCases[14].ListePions.Count > 35)
            {
                resultatDuMatch = ResultatMatch.V1;
                etatDuMatch = EtatMatch.Fin;
                finDuMatch();
            }
            else if (tableMatch.ListeCases[15].ListePions.Count > 35)
            {
                resultatDuMatch = ResultatMatch.V2;
                etatDuMatch = EtatMatch.Fin;
                finDuMatch();
            }
            else if (tableMatch.ListeCases[14].ListePions.Count == 35 && tableMatch.ListeCases[15].ListePions.Count == 35)
            {
                resultatDuMatch = ResultatMatch.MatchNul;
                pauseMenu.EnPause = false;
                Fonctions.desactiverObjet(pauseMenu.BoutonPausePrefab);
                Fonctions.activerObjet(finMatchMenu.MenuFinMatch);
                finMatchMenu.TextVictoire.colorGradientPreset = finMatchMenu.CouleurMatchNul;
                Fonctions.activerAudioSourceVictoire();
                Fonctions.changerTexte(finMatchMenu.TextVictoire, "Match Nul !!!");
                etatDuMatch = EtatMatch.Fin;
            }
            else
            {
                tourJoueur(caseDepart.Id);

            }
        }
        public void tourJoueur(int idCaseDepart)
        {
            if (idCaseDepart >= 7)
            {
                if (joueur1 != null)
                {
                    joueur1.Tour = Tour.MonTour;
                    joueur1.Swipe.enabled = true;
                }
                if (joueur2 != null)
                {
                    joueur2.Tour = Tour.SonTour;
                    joueur2.Swipe.enabled = false;
                }
                if(etatDuMatch==EtatMatch.EnCours){
                    Fonctions.faireVibrer();
                    Fonctions.activerAudioSourceDebutMatch();
                }
                outilsJoueur.activerCompteurJoueur(1);
                tourJ.activerTourjoueur(1);
            }
            else
            {
                if (joueur2 != null)
                {
                    joueur2.Tour = Tour.MonTour;
                    joueur2.Swipe.enabled = true;
                }
                if (joueur1 != null)
                {
                    joueur1.Tour = Tour.SonTour;
                    joueur1.Swipe.enabled = false;
                }
                if (etatDuMatch == EtatMatch.EnCours)
                {
                    Fonctions.faireVibrer();
                    Fonctions.activerAudioSourceDebutMatch();
                }
                outilsJoueur.activerCompteurJoueur(2);
                tourJ.activerTourjoueur(2);
            }
        }
        public void jouerTable(Case caseDepart)
        {
            if (etatDuMatch == EtatMatch.Debut || etatDuMatch == EtatMatch.Fin || caseDepart.Id == 14 || caseDepart.Id == 15)
            {
                Debug.Log("je rejoue le coup");
                rejouerCoup(caseDepart);
            }
            else
            {
                tableMatch.parcourirLaTable(caseDepart);
            }
        }
        public abstract void rejouerCoup(Case caseDepart);
        public abstract void finDuMatch();
        public void rejouerMatch()
        {
            matchRejoue = true;
            Fonctions.activerObjet(pauseMenu.BoutonPausePrefab);
            Fonctions.desactiverObjet(finMatchMenu.MenuFinMatch);
            joueur1.NumPosition = joueur1.NumPosition == 1 ? 2 : 1;
            joueur2.NumPosition = joueur2.NumPosition == 1 ? 2 : 1;
            StartCoroutine(partagerLesPions());
        }

        public abstract void initialiseJoueurs();
        public abstract void abandonMatch();
        public abstract void debuterMatch();

    }
}