using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Entities;

namespace Mvc.Repositories
{
    public class SongoMatchEnLigne
    {
        [SerializeField] string idAdversaire;
        [SerializeField] string idVainqueur;
        [SerializeField] string dateMatch;
        //[SerializeField] int codeMatch;
        public static string chemin = "/songo_match_en_ligne/";

        public string IdAdversaire { get => idAdversaire; set => idAdversaire = value; }
        public string IdVainqueur { get => idVainqueur; set => idVainqueur = value; }
        public string DateMatch { get => dateMatch; set => dateMatch = value; }


        public SongoMatchEnLigne(string idAdversaire, string idVainqueur, string dateMatch)
        {
            this.idAdversaire = idAdversaire;
            this.idVainqueur = idVainqueur;
            this.dateMatch = dateMatch;
        }
    }
}