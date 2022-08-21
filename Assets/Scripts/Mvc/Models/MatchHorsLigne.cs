using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class MatchHorsLigne : Match
    {
        [SerializeField] private int id;
        [SerializeField] private string scoreMatch;

        [SerializeField] private List<JoueurOff> listeJoueurOff = new List<JoueurOff>();

        public string ScoreMatch { get => scoreMatch; set => scoreMatch = value; }
        public int Id { get => id; set => id = value; }

        private void Start()
        {
            typeDuMatch = TypeMatch.HorsLigne1vs1;
        }

        protected override void initialiseJoueurs()
        {
            GameObject j1 = GameObject.Find("Joueur1");
            GameObject j2 = GameObject.Find("Joueur2");
            if (j1 != null)
            {
                listeJoueurOff.Add(j1.GetComponent<JoueurOff>());
            }
            if (j2 != null)
            {
                listeJoueurOff.Add(j2.GetComponent<JoueurOff>());
            }
        }
    }
}