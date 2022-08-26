using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


        //[SerializeField] private SongoMatchEnLigne songoMatchEnLigne;

        //[SerializeField] protected MatchEnLigne matchEnLigne = GameObject.Find("MatchEnLigne").GetComponent<MatchEnLigne>();

        //[SerializeField] private List<JoueurOn> listeJoueurOn = new List<JoueurOn>();


        public string NumeroMatch { get => numeroMatch; set => numeroMatch = value; }
        public DateTime DateMatch { get => dateMatch; set => dateMatch = value; }
        public MatchEnLigneController MatchEnLigneController { get => matchEnLigneController; set => matchEnLigneController = value; }

        void Awake()
        {
            //matchEnLigneController = this;
        }
        private void Start()
        {
            //joueurConnecte = GameObject.Find("JoueurConnecte").GetComponent<JoueurConnecte>();
            //JoueurNonConnecte joueurNonConnecte = GameObject.Find("JoueurNonConnecte").GetComponent<JoueurNonConnecte>();

        }
        private void OnEnable()
        {
            matchRejoue = false;
            typeDuMatch = TypeMatch.EnLigne;
            //tableMatch = Fonctions.instancierObjet(songoMatchPrefab).GetComponentInChildren<Table>();
            //pauseMenu = Fonctions.instancierObjet(GameObject.Find("PauseMenu")).GetComponentInChildren<PauseMenu>();
            //pauseMenu.Match = ((Match)this);
        }



        protected override void initialiseJoueurs()
        {
            /*GameObject j1 = GameObject.Find("Joueur1");
            GameObject j2 = GameObject.Find("Joueur2");
            if (j1 != null)
            {
                listeJoueurOn.Add(j1.GetComponent<JoueurOn>());
            }
            if (j2 != null)
            {
                listeJoueurOn.Add(j2.GetComponent<JoueurOn>());
            }*/
        }
        public override void verifierEtatDuMatch(Case caseArrivee)
        {

        }
        public override void tourJoueur(int numPosition)
        {

        }
        public override void jouerTable(Case caseDepart)
        {

        }
        public override void rejouerCoup()
        {

        }
        public override void finDuMatch()
        {

        }
        public override void rejouerMatch()
        {

        }
        public override void abandonMatch()
        {

        }

        public new void insert()
        {
            refe = FirebaseDatabase.DefaultInstance.RootReference;
            //DatabaseReference rootRef = FirebaseDatabase.DefaultInstance;
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

        public void recupereScore(Query requete)
        {
            Dictionary<string, Dictionary<string, string>> data;
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
                       //Fonctions.showDictionary(data);
                       Debug.Log(data.Count);
                       //Utilisateur user = JsonUtility.FromJson<Utilisateur>(snapshot.GetRawJsonValue());
                       //Debug.Log(user.Email);

                   }
                   else
                   {
                       Debug.Log(this.msgFailed);
                   }
                   //statut = StatutDatabase.Succes;
               });
        }


    }
}
