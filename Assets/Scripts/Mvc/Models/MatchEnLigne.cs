using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mvc.Core;
using Mvc.Controllers;
using Mvc.Entities;
using System;
using Firebase.Database;
using Newtonsoft.Json;

namespace Mvc.Models
{
    public class MatchEnLigne : Match
    {

        [SerializeField] private string numeroMatch;
        [SerializeField] private DateTime dateMatch;
        [SerializeField] private MatchEnLigneController matchEnLigneController;
        [SerializeField] protected StatutDatabase statutDatabase;
        [SerializeField] private GameObject cameraP1;
        [SerializeField] private GameObject cameraP2;
        [SerializeField] private bool abandon;
        [SerializeField] private bool joueurDeconnecte;


        public string NumeroMatch { get => numeroMatch; set => numeroMatch = value; }
        public DateTime DateMatch { get => dateMatch; set => dateMatch = value; }
        public MatchEnLigneController MatchEnLigneController { get => matchEnLigneController; set => matchEnLigneController = value; }
        public bool Abandon { get => abandon; set => abandon = value; }
        public bool JoueurDeconnecte { get => joueurDeconnecte; set => joueurDeconnecte = value; }

        void Awake()
        {
            if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
            {
                Fonctions.activerObjet(cameraP1);
            }
            else
            {
                Fonctions.activerObjet(cameraP2);
            }
        }
        private void Start()
        {
            abandon = false;
            joueurDeconnecte = false;
        }
        private void OnEnable()
        {
            matchRejoue = false;
            typeDuMatch = TypeMatch.EnLigne;
            statutDatabase = StatutDatabase.Debut;
            //tableMatch = Fonctions.instancierObjet(songoMatchPrefab).GetComponentInChildren<Table>();
            //pauseMenu = Fonctions.instancierObjet(GameObject.Find("PauseMenu")).GetComponentInChildren<PauseMenu>();
            //pauseMenu.Match = ((Match)this);
        }

        void Update()
        {
            if (statutDatabase == StatutDatabase.Succes)
            {
                statutDatabase = StatutDatabase.Debut;
                afficheScore();
            }
            if (etatDuMatch == EtatMatch.EnCours)
            {
                if (!joueurDeconnecte)
                    return;
                joueurDeconnecte = false;
                tourJ.desactiverToursjoueurs();
                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                {
                    if (joueur1 == null)
                    {
                        Fonctions.afficherMsgScene("Connexion impossible,\nen attente de reconnexion.", "erreur", 30);
                    }
                    else if (joueur2 == null)
                    {
                        Fonctions.afficherMsgScene(PlayerPrefs.GetString("surnomAdversaire") + " se reconnecte.\nVeuillez ne pas quitter le match.", "erreur", 30);
                    }

                }
                else if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 2)
                {
                    if (joueur2 == null)
                    {
                        Fonctions.afficherMsgScene("Connexion impossible,\nen attente de reconnexion.", "erreur", 30);

                    }
                    else if (joueur1 == null)
                    {
                        Fonctions.afficherMsgScene(PlayerPrefs.GetString("surnomAdversaire") + " se reconnecte.\nVeuillez ne pas quitter le match.", "erreur", 30);

                    }
                }
            }
        }

        public override void debuterMatch()
        {
            matchRejoue = false;
            initialiseJoueurs();
            tableMatch = Fonctions.instancierObjet(songoMatchPrefab).GetComponentInChildren<Table>();
            tableMatch.Match = ((Match)this);
            if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
            {
                Table.idCaseJoue = 14;
            }
            else
            {
                Table.idCaseJoue = 15;
                Fonctions.activerObjet(cameraP2);
            }
            Fonctions.desactiverObjet(matchEnLigneController.SceneController.PhotonManager.AttenteMenu.gameObject);
        }

        public override void initialiseJoueurs()
        {
            joueur1.NumPosition = 1;
            joueur1.CouleurTouche = joueur1.CouleurToucheJoueur1;
            joueur2.NumPosition = 2;
            joueur2.CouleurTouche = joueur2.CouleurToucheJoueur2;
            PlayerPrefs.SetInt("premierAjouer", 1);
            PlayerPrefs.SetInt("tourMatchEnCours", 1);
        }

        public override void finDuMatch()
        {
            Debug.Log("Resultat du match : " + resultatDuMatch);
            pauseMenu.EnPause = true;
            etatDuMatch = EtatMatch.Fin;
            Fonctions.desactiverObjet(pauseMenu.BoutonPausePrefab);
            outilsJoueur.desactiverCompteursJoueurs();
            tourJ.desactiverToursjoueurs();
            Fonctions.activerObjet(finMatchMenu.MenuFinMatch);
            if (resultatDuMatch == ResultatMatch.V1)
            {
                if (joueur1 != null)
                    joueur1.victoireJoueur();

                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                {
                    finMatchMenu.TextVictoire.colorGradientPreset = finMatchMenu.CouleurVictoire;
                    Fonctions.changerTexte(finMatchMenu.TextVictoire, "Félicitations, vous avez gagné !!!");
                    if (abandon)
                    {
                        Fonctions.activerObjet(finMatchMenu.BackgroundVictoireMini);
                        finMatchMenu.TextVictoireMini.colorGradientPreset = finMatchMenu.CouleurMatchNul;
                        string msg = PlayerPrefs.GetString("surnomAdversaire") + " a abandonné.";
                        Fonctions.changerTexte(finMatchMenu.TextVictoireMini, msg);
                    }
                    matchEnLigneController.ajouter();
                }
                else
                {
                    finMatchMenu.TextVictoire.colorGradientPreset = finMatchMenu.CouleurDefaite;
                    Fonctions.changerTexte(finMatchMenu.TextVictoire, "Désolé, vous avez perdu !!!");
                    if (abandon)
                    {
                        Fonctions.activerObjet(finMatchMenu.BackgroundVictoireMini);
                        finMatchMenu.TextVictoireMini.colorGradientPreset = finMatchMenu.CouleurMatchNul;
                        string msg = " Vous avez abandonné.";
                        Fonctions.changerTexte(finMatchMenu.TextVictoireMini, msg);
                    }

                }


            }
            else if (resultatDuMatch == ResultatMatch.V2)
            {
                if (joueur2 != null)
                    joueur2.victoireJoueur();

                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 2)
                {
                    finMatchMenu.TextVictoire.colorGradientPreset = finMatchMenu.CouleurVictoire;
                    Fonctions.changerTexte(finMatchMenu.TextVictoire, "Félicitations, vous avez gagné !!!");
                    if (abandon)
                    {
                        Fonctions.activerObjet(finMatchMenu.BackgroundVictoireMini);
                        finMatchMenu.TextVictoireMini.colorGradientPreset = finMatchMenu.CouleurMatchNul;
                        string msg = PlayerPrefs.GetString("surnomAdversaire") + " a abandonné.";
                        Fonctions.changerTexte(finMatchMenu.TextVictoireMini, msg);
                    }
                    matchEnLigneController.ajouter();
                }
                else
                {
                    finMatchMenu.TextVictoire.colorGradientPreset = finMatchMenu.CouleurDefaite;
                    Fonctions.changerTexte(finMatchMenu.TextVictoire, "Désolé, vous avez perdu !!!");
                    if (abandon)
                    {
                        Fonctions.activerObjet(finMatchMenu.BackgroundVictoireMini);
                        finMatchMenu.TextVictoireMini.colorGradientPreset = finMatchMenu.CouleurMatchNul;
                        string msg = " Vous avez abandonné.";
                        Fonctions.changerTexte(finMatchMenu.TextVictoireMini, msg);
                    }

                }


            }
            scoreMatch.afficherScoreMatch();
        }

        public override void abandonMatch()
        {
            if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
            {
                resultatDuMatch = ResultatMatch.V2;
            }
            else
            {
                resultatDuMatch = ResultatMatch.V1;
            }
            finDuMatch();
        }
        public void abandonMatch(int numPosition)
        {
            if (numPosition == 1)
            {
                resultatDuMatch = ResultatMatch.V2;
            }
            else
            {
                resultatDuMatch = ResultatMatch.V1;
            }
            finDuMatch();
        }
        public override void rejouerCoup(Case caseDepart)
        {
            Debug.Log("coup rejoué");
            if (caseDepart.Id < 7 || caseDepart.Id == 14)
            {
                joueur1.Tour = Tour.MonTour;
                joueur1.Swipe.enabled = true;
                joueur2.Swipe.enabled = false;
                joueur2.Tour = Tour.SonTour;
                tourJ.activerTourjoueur(1);
            }
            else if ((caseDepart.Id >= 7 && caseDepart.Id < 14) || caseDepart.Id == 15)
            {
                joueur2.Tour = Tour.MonTour;
                joueur2.Swipe.enabled = true;
                joueur1.Swipe.enabled = false;
                joueur1.Tour = Tour.SonTour;
                tourJ.activerTourjoueur(2);
            }
        }

        public new void insert()
        {
            refe = FirebaseDatabase.DefaultInstance.RootReference;
            String key = refe.Push().Key.ToString();
            Debug.Log("Id Match : " + key);
            SongoMatchEnLigne songoMatchEnLigne = new SongoMatchEnLigne(PlayerPrefs.GetString("idAdversaire"), PlayerPrefs.GetString("idVainqueur"), DateTime.Now.ToString("yyyy'-'MM'-'dd"));
            string songoMatchEnLigne_json = JsonUtility.ToJson(songoMatchEnLigne);
            refe.Child(table()).Child(key).SetRawJsonValueAsync(songoMatchEnLigne_json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log(this.msgSuccess);
                    refe = null;
                }
                else
                {
                    Debug.Log(this.msgFailed);
                    refe = null;
                }
            });
        }

        public void recupereMatch(int joueur, string idJoueur, string idAversaire)
        {
            Dictionary<string, Dictionary<string, string>> data;
            DatabaseReference refe = FirebaseDatabase.DefaultInstance.RootReference;
            Query requete = refe.Child(table()).OrderByChild("idVainqueur").EqualTo(idJoueur);
            requete.GetValueAsync().ContinueWith(task =>
               {
                   if (task.IsCompleted)
                   {
                       Debug.Log(this.MsgSuccess);
                       DataSnapshot snapshot = task.Result;
                       string dataResult = "";
                       if (snapshot.Value != null)
                       {
                           dataResult = snapshot.GetRawJsonValue();
                           data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(dataResult);
                           Debug.Log(dataResult);

                       }
                       else
                       {
                           data = new Dictionary<string, Dictionary<string, string>>();
                       }
                       Debug.Log(data.Count);
                       int cpt = 0;
                       foreach (var value in data.Values)
                       {
                           if (value["idAdversaire"].CompareTo(idAversaire) == 0)
                           {
                               cpt += 1;
                           }
                       }
                       if (joueur == 1)
                       {
                           joueur1.NombreVictoire = cpt;
                       }
                       else
                       {
                           joueur2.NombreVictoire = cpt;
                       }
                       statutDatabase = StatutDatabase.Succes;

                   }
                   else
                   {
                       Debug.Log(this.msgFailed);
                   }
               });
        }

        public void afficheScore()
        {
            scoreMatch.afficherScoreMatch();
        }


    }
}
