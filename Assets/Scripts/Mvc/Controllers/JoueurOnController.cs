using Mvc.Core;
using Mvc.Entities;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Repositories;
using System;

namespace Mvc.Controllers
{
    public class JoueurOnController : MonoBehaviour, IController
    {
        [SerializeField] private GameObject joueurConnectePrefab;
        [SerializeField] private JoueurConnecte joueurConnecte;
        [SerializeField] private GameObject joueurOnPrefab;
        [SerializeField] private JoueurOn joueurOn;
        [SerializeField] private SceneController sceneController;


        public SceneController SceneController { get => sceneController; set => sceneController = value; }
        public JoueurOn JoueurOn { get => joueurOn; set => joueurOn = value; }
        public JoueurConnecte JoueurConnecte { get => joueurConnecte; set => joueurConnecte = value; }

        private void OnEnable()
        {
            if (Fonctions.sceneActuelle("SceneMenuPrincipal"))
            {
                joueurConnecte = Fonctions.instancierObjet(joueurConnectePrefab).GetComponent<JoueurConnecte>();
                joueurConnecte.JoueurOnController = this;
            }
            /*else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                sceneController.PhotonManager.instancierUnJoueur();
            }*/
        }

        public void lister(bool single = false)
        {
            joueurConnecte.MsgSuccess = this.name + " listés avec succes ";
            joueurConnecte.MsgFailed = "Echec de listage des " + this.name;
            if (!single)
            {
                joueurConnecte.selectAll();
            }
            else
            {
                joueurConnecte.selectById(PlayerPrefs.GetString("id"));
            }
            //Debug.Log(joueurConnecte.Data.Count);
        }
        public void ajouter()
        {
            //insereData();
            joueurConnecte.MsgSuccess = this.name + " créé avec succes ";
            joueurConnecte.MsgFailed = "Echec de la création du " + this.name;
            joueurConnecte.insert();
        }
        public void supprimer()
        {
            joueurConnecte.MsgSuccess = this.name + " supprimé avec succes";
            joueurConnecte.MsgFailed = "Echec de la suppression du " + this.name;
            joueurConnecte.delete(PlayerPrefs.GetString("id"));
        }
        public void modifier()
        {

        }
        public void modifierNiveauJoueurOn()
        {
            //print("Type Connexion : " + TypeConnexionCompte.Facebook.ToString());
            joueurConnecte.MsgSuccess = "idNiveau du " + this.name + " modifié avec succes";
            joueurConnecte.MsgFailed = "Echec de la modification du " + this.name;
            string cheminAttribut = SongoJoueurOnline.chemin + PlayerPrefs.GetString("id");
            Dictionary<string, object> cleValeur = new Dictionary<string, object>();
            cleValeur = Fonctions.toDictinary(joueurConnecte.table());
            joueurConnecte.update(cheminAttribut, cleValeur);
        }
        public void recupereJoueurConnecte()
        {
            insereData();
            joueurConnecte.MsgSuccess = this.name + " récupéré avec succes ";
            joueurConnecte.MsgFailed = "Echec de la récupération du " + this.name;
            joueurConnecte.recupereJoueur(PlayerPrefs.GetString("id"));
        }

        private void insereData()
        {
            joueurConnecte.Surnom = PlayerPrefs.GetString("surnom");
            joueurConnecte.Id = PlayerPrefs.GetString("id");
            joueurConnecte.Email = PlayerPrefs.GetString("email");
            joueurConnecte.DateInscription = DateTime.ParseExact(PlayerPrefs.GetString("dateInscription"), "yyyy'-'MM'-'dd", null);
            joueurConnecte.HeureInscription = DateTime.ParseExact(PlayerPrefs.GetString("heureInscription"), "HH:mm", null);
            ConnexionCompte connexionCompte = new ConnexionCompte();
            connexionCompte.TypeConnexionCompte = PlayerPrefs.GetInt("idConnexionCompte") == 1 ? TypeConnexionCompte.Facebook : TypeConnexionCompte.Google;
            joueurConnecte.ConnexionCompte = connexionCompte;
            Niveau niveau = new Niveau();
            niveau.Id = PlayerPrefs.GetInt("idNiveau");
            joueurConnecte.Niveau = niveau;
        }
    }
}