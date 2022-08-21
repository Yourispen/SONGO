using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Newtonsoft.Json;
using Mvc.Entities;
using Mvc.Core;


namespace Mvc.Models
{
    public class JoueurOn : Joueur
    {
        [SerializeField] protected string id;
        [SerializeField] protected DateTime dateInscription;
        [SerializeField] protected DateTime heureInscription;
        [SerializeField] protected string email;
        [SerializeField] protected TypeJoueur typeJoueur;
        [SerializeField] protected StatutJoueur statutJoueur;
        [SerializeField] protected ConnexionCompte connexionCompte;
        [SerializeField] protected Niveau niveau;
        [SerializeField] private SongoJoueurOnline songoJoueurOnline;
        [SerializeField] private StatutDatabase statutDatabase;
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

        public JoueurOn(string surnom, DateTime dateInscription, DateTime heureInscription, string email, ConnexionCompte connexionCompte, Niveau niveau)
        {
            Surnom = surnom;
            DateInscription = dateInscription;
            HeureInscription = heureInscription;
            Email = email;
            ConnexionCompte = connexionCompte;
            Niveau = niveau;
        }

        public string Id { get => id; set => id = value; }
        public DateTime DateInscription { get => dateInscription; set => dateInscription = value; }
        public DateTime HeureInscription { get => heureInscription; set => heureInscription = value; }
        public string Email { get => email; set => email = value; }
        public TypeJoueur TypeJoueur { get => typeJoueur; set => typeJoueur = value; }
        public StatutJoueur StatutJoueur { get => statutJoueur; set => statutJoueur = value; }
        public Niveau Niveau { get => niveau; set => niveau = value; }
        public ConnexionCompte ConnexionCompte { get => connexionCompte; set => connexionCompte = value; }

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
        public void recupereJoueur(string id)
        {
            statutDatabase = StatutDatabase.Debut;
            DatabaseReference refe = FirebaseDatabase.DefaultInstance.RootReference;
            Query requete = refe.Child(this.table()).Child(PlayerPrefs.GetString("id")).OrderByChild("email");
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

                       }
                       else
                       {
                           Debug.Log("Ce joueur n'existe pas");
                           /* refe = null;
                            msgSuccess = "Le Joueur a été créé avec succes ";
                            msgFailed = "Echec de la création du Joueur";
                           /// this.insert();*/
                           /// 
                       }
                       statutDatabase = StatutDatabase.Succes;
                       //Fonctions.showDictionary(data);
                       //Debug.Log("OK");
                       //Debug.Log(data.Count);

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

        private void recupData()
        {
            //si je suis dans la scene MenuPrincipal
            Fonctions.desactiverObjet(GameObject.Find("PageDeConnexionCompte"));
            if (songoJoueurOnline != null)
            {
                Fonctions.desactiverObjet(GameObject.Find("PageDeSaisiDuSurnom"));
                DateInscription = DateTime.ParseExact(songoJoueurOnline.DateInscription, "yyyy'-'MM'-'dd", null);//songoJoueurOnline.DateInscription
                heureInscription = DateTime.ParseExact(songoJoueurOnline.HeureInscription, "HH:mm", null);
                surnom = songoJoueurOnline.Surnom;
                ConnexionCompte connexionCompte = new ConnexionCompte();
                connexionCompte.TypeConnexionCompte = songoJoueurOnline.IdConnexionCompte == 1 ? TypeConnexionCompte.Facebook : TypeConnexionCompte.Google;
                ConnexionCompte = connexionCompte;
                Niveau niveau = new Niveau();
                niveau.Id = songoJoueurOnline.IdNiveau;
                Niveau = niveau;
                songoJoueurOnline = null;
                PlayerPrefs.SetString("surnom", Surnom);
                PlayerPrefs.SetString("dateInscription", dateInscription.ToString("yyyy'-'MM'-'dd"));
                PlayerPrefs.SetString("heureInscription", dateInscription.ToString("HH:mm"));
                PlayerPrefs.SetInt("idConnexionCompte", ((int)connexionCompte.TypeConnexionCompte));
                PlayerPrefs.SetInt("idNiveau", Niveau.Id);
                Fonctions.afficherMsgScene(FacebookAuth.msgConnexion, "succes");
            }
            Fonctions.finChargement();

        }


    }
}
