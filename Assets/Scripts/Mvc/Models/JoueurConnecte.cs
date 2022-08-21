using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System;
using Mvc.Entities;

namespace Mvc.Models
{
    public class JoueurConnecte : JoueurOn
    {


        [SerializeField] private List<Ami> listeAmis = new List<Ami>();

        [SerializeField] private List<MatchEnLigne> listeMatchEnLigne = new List<MatchEnLigne>();

        [SerializeField] private EtatConnectionCompte etatConnectionCompte;

        public List<Ami> ListeAmis { get => listeAmis; }
        public List<MatchEnLigne> ListeMatchEnLigne { get => listeMatchEnLigne; }
        public EtatConnectionCompte EtatConnectionCompte1 { get => etatConnectionCompte; set => etatConnectionCompte = value; }

        public void ajouterMatchEnLigne(MatchEnLigne matchEnLigne)
        {
            listeMatchEnLigne.Add(matchEnLigne);
        }

        public void ajouterAmi(Ami ami)
        {
            listeAmis.Add(ami);
        }

        private void OnEnable()
        {
            typeJoueur = TypeJoueur.JoueurConnecte;
        }

        public void insertSql()
        {
            //Debug.Log(PlayerPrefs.GetString("surnom"));
            WWWForm form = new WWWForm();
            form.AddField("surnom", PlayerPrefs.GetString("surnom"));
            form.AddField("dateInscription", PlayerPrefs.GetString("dateInscription"));
            form.AddField("heureInscription", PlayerPrefs.GetString("heureInscription"));
            form.AddField("email", PlayerPrefs.GetString("email"));
            form.AddField("idConnexionCompte", PlayerPrefs.GetString("idConnexionCompte"));
            form.AddField("idNiveau", PlayerPrefs.GetString("idNiveau"));
            form.AddField("table", table());
            form.AddField("action", "ajouter");
            //StartCoroutine(request(form));
        }

    }
}
