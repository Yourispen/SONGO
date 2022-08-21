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

        private void OnEnable()
        {
            matchEnligne = Fonctions.instancierObjet(matchEnlignePrefab).GetComponent<MatchEnLigne>();
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

        public void recupererScoreDuMatch()
        {
            matchEnligne.MsgSuccess = this.name + " récupéré avec succes ";
            matchEnligne.MsgFailed = "Echec de la récupération du " + this.name;
            DatabaseReference refe = FirebaseDatabase.DefaultInstance.RootReference;
            Query requete = refe.Child(matchEnligne.table()).OrderByChild("idVainqueur").EqualTo(PlayerPrefs.GetString("id")).OrderByChild("idAdversaire").EqualTo(PlayerPrefs.GetString("idAdversaire"));
            matchEnligne.recupereScore(requete);
            DatabaseReference refe1 = FirebaseDatabase.DefaultInstance.RootReference;
            Query requete1 = refe1.Child(matchEnligne.table()).OrderByChild("idVainqueur").EqualTo(PlayerPrefs.GetString("idAdversaire")).OrderByChild("idAdversaire").EqualTo(PlayerPrefs.GetString("id"));
            matchEnligne.recupereScore(requete1);
        }
    }
}