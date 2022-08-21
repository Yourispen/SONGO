using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class JoueurNonConnecte : JoueurOn
    {
        [SerializeField] private List<MatchEnLigne> listeMatchEnLigne = new List<MatchEnLigne>();

        public List<MatchEnLigne> ListeMatchEnLigne { get => listeMatchEnLigne;}
        public void ajouterMatchEnLigne(MatchEnLigne matchEnLigne)
        {
            listeMatchEnLigne.Add(matchEnLigne);
        }
    }
}
