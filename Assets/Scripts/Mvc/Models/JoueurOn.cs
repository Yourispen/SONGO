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

        void Start()
        {
            if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                msgSuccess = this.name + " récupéré avec succes ";
                msgFailed = "Echec de la récupération du " + this.name;
                int num = PlayerPrefs.GetInt("numPositionMatchEnCours");
                Debug.Log("Reconnexion : " + PhotonManager.reconnect);
                if (photonView.IsMine && num == 1)
                {
                    gameObject.name = "joueur1";
                    match.Joueur1 = ((JoueurOn)this);
                    Debug.Log("Id Joueur1 : " + PhotonNetwork.PlayerList.ToStringFull());
                    if (!PhotonManager.reconnect)
                    {
                        PlayerPrefs.SetString("idVainqueur", PhotonNetwork.PlayerList[0].NickName);
                        Fonctions.desactiverObjet(match.OutilsJoueur.PlaqueCompteur2.gameObject);
                    }
                    surnom = PlayerPrefs.GetString("surnom");
                    email = PlayerPrefs.GetString("email");

                }
                else if (!photonView.IsMine && num == 1)
                {
                    gameObject.name = "joueur2";
                    match = ((Match)Fonctions.instancierObjet(GameObject.Find("matchEnLigne")).GetComponent<MatchEnLigne>());
                    match.Joueur2 = ((JoueurOn)this);
                    joueurOnController = Fonctions.instancierObjet(GameObject.Find("joueurOnController")).GetComponent<JoueurOnController>();
                    Debug.Log("Id Joueur2 : " + PhotonNetwork.PlayerList.ToStringFull());
                    if (!PhotonManager.reconnect)
                    {
                        PlayerPrefs.SetString("idAdversaire", PhotonNetwork.PlayerList[1].NickName);
                        recupereJoueur(PlayerPrefs.GetString("idAdversaire"));
                        ((MatchEnLigne)match).MatchEnLigneController.recupererLesMatchGagneUnJoueur(1, PlayerPrefs.GetString("idVainqueur"), PlayerPrefs.GetString("idAdversaire"));
                        ((MatchEnLigne)match).MatchEnLigneController.recupererLesMatchGagneUnJoueur(2, PlayerPrefs.GetString("idAdversaire"), PlayerPrefs.GetString("idVainqueur"));
                    }
                    else
                    {
                        Fonctions.afficherMsgScene("", "primaire", 0);
                        surnom = PlayerPrefs.GetString("surnomAdversaire");
                        if (((MatchEnLigne)match).MatchEnLigneController.SceneController.PhotonManager.AderversaireDeconnecte)
                        {

                            PhotonView.RPC("reconnecterJoueur", RpcTarget.AllBuffered, Table.idCaseJoue, PlayerPrefs.GetInt("numPositionMatchEnCours"), match.Joueur1.NumPosition);

                            ((MatchEnLigne)match).MatchEnLigneController.SceneController.PhotonManager.AderversaireDeconnecte = false;

                        }

                    }
                    PhotonManager.reconnect = false;
                }
                else if (photonView.IsMine && num == 2)
                {
                    gameObject.name = "joueur2";
                    match.Joueur2 = ((JoueurOn)this);
                    Debug.Log("Id Joueur2 : " + PhotonNetwork.PlayerList.ToStringFull());
                    if (!PhotonManager.reconnect)
                    {
                        //match = ((Match)Fonctions.instancierObjet(GameObject.Find("matchEnligne")).GetComponent<MatchEnLigne>());
                        PlayerPrefs.SetString("idVainqueur", PhotonNetwork.PlayerList[1].NickName);
                        Fonctions.desactiverObjet(match.OutilsJoueur.PlaqueCompteur1.gameObject);
                        Vector3 positionTemp = match.OutilsJoueur.PlaqueNom1.rectTransform.position;
                        match.OutilsJoueur.PlaqueNom1.rectTransform.position = match.OutilsJoueur.PlaqueNom2.rectTransform.position;
                        match.OutilsJoueur.PlaqueNom2.rectTransform.position = positionTemp;
                    }

                    surnom = PlayerPrefs.GetString("surnom");
                    email = PlayerPrefs.GetString("email");
                }
                else if (!photonView.IsMine && num == 2)
                {
                    gameObject.name = "joueur1";
                    match = ((Match)Fonctions.instancierObjet(GameObject.Find("matchEnLigne")).GetComponent<MatchEnLigne>());
                    match.Joueur1 = ((JoueurOn)this);
                    joueurOnController = Fonctions.instancierObjet(GameObject.Find("joueurOnController")).GetComponent<JoueurOnController>();
                    Debug.Log("Id Joueur1 : " + PhotonNetwork.PlayerList.ToStringFull());
                    if (!PhotonManager.reconnect)
                    {
                        PlayerPrefs.SetString("idAdversaire", PhotonNetwork.PlayerList[0].NickName);
                        recupereJoueur(PlayerPrefs.GetString("idAdversaire"));
                        ((MatchEnLigne)match).MatchEnLigneController.recupererLesMatchGagneUnJoueur(2, PlayerPrefs.GetString("idVainqueur"), PlayerPrefs.GetString("idAdversaire"));
                        ((MatchEnLigne)match).MatchEnLigneController.recupererLesMatchGagneUnJoueur(1, PlayerPrefs.GetString("idAdversaire"), PlayerPrefs.GetString("idVainqueur"));
                    }
                    else
                    {
                        Fonctions.afficherMsgScene("", "primaire", 0);
                        surnom = PlayerPrefs.GetString("surnomAdversaire");
                        if (((MatchEnLigne)match).MatchEnLigneController.SceneController.PhotonManager.AderversaireDeconnecte)
                        {
                            photonView.RPC("reconnecterJoueur", RpcTarget.AllBuffered, Table.idCaseJoue, PlayerPrefs.GetInt("numPositionMatchEnCours"), match.Joueur2.NumPosition);
                            ((MatchEnLigne)match).MatchEnLigneController.SceneController.PhotonManager.AderversaireDeconnecte = false;
                        }
                    }
                    PhotonManager.reconnect = false;
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

        public void copyJoueurOn()
        {
            surnom = songoJoueurOnline.Surnom;
            email = songoJoueurOnline.Email;
            dateInscription = DateTime.ParseExact(songoJoueurOnline.DateInscription, "yyyy'-'MM'-'dd", null);
            heureInscription = DateTime.ParseExact(songoJoueurOnline.HeureInscription, "HH:mm", null);
            surnom = songoJoueurOnline.Surnom;
            ConnexionCompte connexionCompte = new ConnexionCompte();
            connexionCompte.TypeConnexionCompte = songoJoueurOnline.IdConnexionCompte == 1 ? TypeConnexionCompte.Facebook : TypeConnexionCompte.Google;
            ConnexionCompte = connexionCompte;
            Niveau niveau = new Niveau();
            niveau.Id = songoJoueurOnline.IdNiveau;
            Niveau = niveau;
            songoJoueurOnline = null;
        }
        void Update()
        {
            if (statutDatabase == StatutDatabase.Succes)
            {
                statutDatabase = StatutDatabase.Debut;
                StartCoroutine(recupData());
            }
        }

        public new void jouerMatch(Case caseDepart)
        {
            swipe.enabled = false;
            match.jouerTable(caseDepart);
        }
        [PunRPC]
        public void jouerMatch(int idCaseDepart)
        {
            swipe.enabled = false;
            match.jouerTable(match.TableMatch.ListeCases[idCaseDepart]);
        }

        [PunRPC]
        public void reconnecterJoueur(int idCaseJoue, int numPositionMatchEnCours, int numPosition)
        {
            match.Joueur1.CouleurTouche = match.Joueur1.CouleurToucheJoueur1;
            match.Joueur2.CouleurTouche = match.Joueur2.CouleurToucheJoueur2;

            if (numPosition == 1)
            {
                match.Joueur1.NumPosition = numPositionMatchEnCours == 1 ? 1 : 2;
                match.Joueur2.NumPosition = numPositionMatchEnCours == 2 ? 1 : 2;
            }
            else
            {
                match.Joueur1.NumPosition = numPositionMatchEnCours == 2 ? 1 : 2;
                match.Joueur2.NumPosition = numPositionMatchEnCours == 1 ? 1 : 2;
            }

            if (idCaseJoue == 14)
            {
                match.tourJoueur(numPosition == 1 ? 7 : 0);
            }
            else if (idCaseJoue == 15)
            {
                match.tourJoueur(numPosition == 1 ? 0 : 7);
            }
            else
            {
                Table.idCaseJoue = idCaseJoue;

                if (match.TableMatch.ListeCases[idCaseJoue].ListePions.Count == 0)
                {
                    match.tourJoueur(idCaseJoue);
                }
                else
                {
                    Debug.Log(gameObject.name + " a joué la case " + idCaseJoue);
                    match.jouerTable(match.TableMatch.ListeCases[idCaseJoue]);
                }
            }
            Debug.Log("Case jouée : " + Table.idCaseJoue);

        }

        [PunRPC]
        public void abandonJoueur(int numPosition)
        {
            ((MatchEnLigne)match).Abandon = true;
            ((MatchEnLigne)match).abandonMatch(numPosition);
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
        public IEnumerator recupData()
        {
            if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
            {
                Fonctions.changerTexte(joueurOnController.SceneController.PhotonManager.AttenteMenu.TextPlaqueNom2, songoJoueurOnline.Surnom);
                Fonctions.changerTexte(match.OutilsJoueur.TextPlaqueNom2, songoJoueurOnline.Surnom);
                Fonctions.changerTexte(match.OutilsJoueur.TextPlaqueNom1, PlayerPrefs.GetString("surnom"));
                Fonctions.activerObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.PlaqueNom2.gameObject);

            }
            else if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 2)
            {
                Fonctions.changerTexte(joueurOnController.SceneController.PhotonManager.AttenteMenu.TextPlaqueNom1, songoJoueurOnline.Surnom);
                Fonctions.changerTexte(match.OutilsJoueur.TextPlaqueNom1, songoJoueurOnline.Surnom);
                Fonctions.changerTexte(match.OutilsJoueur.TextPlaqueNom2, PlayerPrefs.GetString("surnom"));
                Fonctions.activerObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.PlaqueNom1.gameObject);
            }
            PlayerPrefs.SetString("surnomAdversaire", songoJoueurOnline.Surnom);
            copyJoueurOn();
            //Fonctions.desactiverObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.TextAttente.gameObject);
            Fonctions.changerTexte(joueurOnController.SceneController.PhotonManager.AttenteMenu.TextAttente, "Le match va commencer...");
            Fonctions.desactiverObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.BoutonRetour.gameObject);
            Fonctions.desactiverObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.CodeMatch.gameObject);
            yield return new WaitForSeconds(3);
            match.ScoreMatch.afficherScoreMatch();
            Fonctions.desactiverObjet(joueurOnController.SceneController.PhotonManager.AttenteMenu.AttenteJoueur);
            ((MatchEnLigne)match).debuterMatch();


        }


    }
}
