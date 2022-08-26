using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Newtonsoft.Json;
using Mvc.Entities;
using Mvc.Core;
using Mvc.Controllers;
using Photon.Pun;
using Photon.Realtime;


namespace Mvc.Models
{
    public class JoueurOn : Joueur
    {

        [SerializeField] protected DateTime dateInscription;
        [SerializeField] protected DateTime heureInscription;
        [SerializeField] protected string email;
        [SerializeField] protected TypeJoueur typeJoueur;
        [SerializeField] protected StatutJoueur statutJoueur;
        [SerializeField] protected ConnexionCompte connexionCompte;
        [SerializeField] protected Niveau niveau;
        [SerializeField] protected SongoJoueurOnline songoJoueurOnline;
        [SerializeField] protected StatutDatabase statutDatabase;
        [SerializeField] protected JoueurOnController joueurOnController;
        [SerializeField] private PhotonView photonView;

        //public Swipe Swipe { get => swipe; set => swipe = value; }

        void Start()
        {
            if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                msgSuccess = this.name + " récupéré avec succes ";
                msgFailed = "Echec de la récupération du " + this.name;
                int num = PlayerPrefs.GetInt("numPositionMatchEnCours");
                if (photonView.IsMine && num == 1)
                {
                    gameObject.name = "joueur1";
                    Debug.Log("Id Joueur1 : " + PhotonNetwork.PlayerList.ToStringFull());
                    match.Joueur1 = ((JoueurOn)this);
                    PlayerPrefs.SetString("idVainqueur", PhotonNetwork.PlayerList[0].NickName);

                }
                else if (!photonView.IsMine && num == 1)
                {
                    gameObject.name = "joueur2";
                    Debug.Log("Id Joueur2 : " + PhotonNetwork.PlayerList.ToStringFull());
                    match = ((Match)Fonctions.instancierObjet(GameObject.Find("matchEnLigne")).GetComponent<MatchEnLigne>());
                    match.Joueur2 = ((JoueurOn)this);
                    joueurOnController = Fonctions.instancierObjet(GameObject.Find("joueurOnController")).GetComponent<JoueurOnController>();
                    PlayerPrefs.SetString("idAdversaire", PhotonNetwork.PlayerList[1].NickName);
                    recupereJoueur(PlayerPrefs.GetString("idAdversaire"));
                }
                else if (photonView.IsMine && num == 2)
                {
                    gameObject.name = "joueur2";
                    Debug.Log("Id Joueur2 : " + PhotonNetwork.PlayerList.ToStringFull());
                    //match = ((Match)Fonctions.instancierObjet(GameObject.Find("matchEnligne")).GetComponent<MatchEnLigne>());
                    match.Joueur2 = ((JoueurOn)this);
                    PlayerPrefs.SetString("idVainqueur", PhotonNetwork.PlayerList[1].NickName);
                }
                else if (!photonView.IsMine && num == 2)
                {
                    gameObject.name = "joueur1";
                    Debug.Log("Id Joueur1 : " + PhotonNetwork.PlayerList.ToStringFull());
                    match = ((Match)Fonctions.instancierObjet(GameObject.Find("matchEnLigne")).GetComponent<MatchEnLigne>());
                    match.Joueur1 = ((JoueurOn)this);
                    joueurOnController = Fonctions.instancierObjet(GameObject.Find("joueurOnController")).GetComponent<JoueurOnController>();
                    PlayerPrefs.SetString("idAdversaire", PhotonNetwork.PlayerList[0].NickName);
                    recupereJoueur(PlayerPrefs.GetString("idAdversaire"));
                }
            }
        }
        void OnEnable()
        {
            statutDatabase = StatutDatabase.Debut;
        }

        public new string table()
        {
            return "songo_joueur_online";
        }

        public JoueurOn()
        {

        }
        private void addMatchEnLigne()
        {

        }

        public JoueurOn(string surnom, DateTime dateInscription, DateTime heureInscription, string email, ConnexionCompte connexionCompte, Niveau niveau)
        {
            Surnom = surnom;
            DateInscription = dateInscription;
            HeureInscription = heureInscription;
            Email = email;
            ConnexionCompte = connexionCompte;
            Niveau = niveau;
        }

        public DateTime DateInscription { get => dateInscription; set => dateInscription = value; }
        public DateTime HeureInscription { get => heureInscription; set => heureInscription = value; }
        public string Email { get => email; set => email = value; }
        public TypeJoueur TypeJoueur { get => typeJoueur; set => typeJoueur = value; }
        public StatutJoueur StatutJoueur { get => statutJoueur; set => statutJoueur = value; }
        public Niveau Niveau { get => niveau; set => niveau = value; }
        public ConnexionCompte ConnexionCompte { get => connexionCompte; set => connexionCompte = value; }
        public JoueurOnController JoueurOnController { get => joueurOnController; set => joueurOnController = value; }

        public void copyJoueurOn(JoueurOn joueurOn)
        {
            this.surnom = joueurOn.Surnom;
            this.email = joueurOn.Email;
            this.dateInscription = joueurOn.DateInscription;
            this.heureInscription = joueurOn.HeureInscription;
        }
        void Update()
        {
            if (statutDatabase == StatutDatabase.Succes)
            {
                statutDatabase = StatutDatabase.Debut;
                recupData();
            }
        }
        public override void victoireJoueur()
        {

        }
        public override void defaiteJoueur()
        {

        }
        public override void abandonJoueur()
        {

        }
        public void recupereJoueur(string id)
        {
            statutDatabase = StatutDatabase.Debut;
            DatabaseReference refe = FirebaseDatabase.DefaultInstance.RootReference;
            Query requete = refe.Child(this.table()).Child(id).OrderByChild("email");
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
                           Debug.Log(dataResult);
                           songoJoueurOnline = JsonConvert.DeserializeObject<SongoJoueurOnline>(dataResult);
                           Debug.Log("surnom de l'adversaire : " + songoJoueurOnline.Surnom);
                       }
                       else
                       {
                           Debug.Log("Ce joueur n'existe pas");
                       }
                       statutDatabase = StatutDatabase.Succes;
                   }
                   else
                   {
                       Debug.Log(this.msgFailed);
                       statutDatabase = StatutDatabase.Echec;
                   }
               });
        }

        public new void insert()
        {
            statutDatabase = StatutDatabase.Debut;
            refe = FirebaseDatabase.DefaultInstance.RootReference;
            songoJoueurOnline = new SongoJoueurOnline(PlayerPrefs.GetString("surnom"), DateTime.Now.ToString("yyyy'-'MM'-'dd"), DateTime.Now.ToString("HH:mm"), PlayerPrefs.GetString("email"), PlayerPrefs.GetInt("idConnexionCompte"), PlayerPrefs.GetInt("idNiveau"), PlayerPrefs.GetString("role"));
            string songoJoueurOnline_json = JsonUtility.ToJson(songoJoueurOnline);
            refe.Child(this.table()).Child(PlayerPrefs.GetString("id")).SetRawJsonValueAsync(songoJoueurOnline_json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log(this.msgSuccess);
                    statutDatabase = StatutDatabase.Succes;
                    refe = null;
                }
                else
                {
                    Debug.Log(this.msgFailed);
                    statutDatabase = StatutDatabase.Echec;
                    refe = null;
                }
            });
        }
        public void recupData()
        {
            if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
            {
                Fonctions.changerTexte(joueurOnController.SceneController.PhotonManager.AttenteMenu.TextPlaqueNom2, songoJoueurOnline.Surnom);
                Fonctions.activerObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.PlaqueNom2.gameObject);
            }
            else if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 2)
            {
                Fonctions.changerTexte(joueurOnController.SceneController.PhotonManager.AttenteMenu.TextPlaqueNom1, songoJoueurOnline.Surnom);
                Fonctions.activerObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.PlaqueNom1.gameObject);
            }
            Fonctions.desactiverObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.TextAttente.gameObject);
            Fonctions.changerTexte(joueurOnController.SceneController.PhotonManager.AttenteMenu.TextAttente);
            //Fonctions.desactiverObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.BoutonRetour.gameObject);
            //Fonctions.activerObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.TextAttente.gameObject);

        }


    }
}
