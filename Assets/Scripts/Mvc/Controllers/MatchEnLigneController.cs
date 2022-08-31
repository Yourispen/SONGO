using Mvc.Core;
using Mvc.Models;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Entities;
using System;
using Firebase.Database;

namespace Mvc.Controllers
{
    public class MatchEnLigneController : MonoBehaviour, IController
    {
        [SerializeField] private GameObject matchEnlignePrefab;
        [SerializeField] private MatchEnLigne matchEnligne;
        [SerializeField] private SceneController sceneController;

        public MatchEnLigne MatchEnligne { get => matchEnligne; set => matchEnligne = value; }
        public SceneController SceneController { get => sceneController; set => sceneController = value; }

        private void Awake()
        {
            /*matchEnligne = Fonctions.instancierObjet(matchEnlignePrefab).GetComponent<MatchEnLigne>();
            matchEnligne.MatchEnLigneController = this;*/
        }
        public void lister(bool single = false)
        {
            matchEnligne.MsgSuccess = this.name + " listés avec succes ";
            matchEnligne.MsgFailed = "Echec du listage des " + this.name;
            matchEnligne.select();
        }
        public void ajouter()
        {
            matchEnligne.MsgSuccess = this.name + " créé avec succes ";
            matchEnligne.MsgFailed = "Echec de la création du " + this.name;
            matchEnligne.insert();
        }
        public void supprimer()
        {

        }
        public void modifier()
        {

        }

        public void recupererLesMatchGagneUnJoueur(int joueur, string idJoueur, string idAversaire)
        {
            matchEnligne.MsgSuccess = this.name + " récupéré avec succes ";
            matchEnligne.MsgFailed = "Echec de la récupération du " + this.name;
            /*int score1 = 1;
            int score2 = 2;
            if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 2)
            {
                score1 = 2;
                score2 = 1;
            }*/
            matchEnligne.recupereMatch(joueur, idJoueur, idAversaire);
            /*DatabaseReference refe1 = FirebaseDatabase.DefaultInstance.RootReference;
            Query requete1 = refe1.Child(matchEnligne.table()).OrderByChild("idVainqueur").EqualTo(PlayerPrefs.GetString("idAdversaire"));
            matchEnligne.recupereScore(requete1,score2);*/
        }

    }
}