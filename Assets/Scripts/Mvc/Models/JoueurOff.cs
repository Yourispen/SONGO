using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class JoueurOff : Joueur
    {
        [SerializeField] private int nombreVictoire;

        [SerializeField] private MatchHorsLigne matchHorsLigne;// = GameObject.Find("MatchHorsLigne").GetComponent<MatchHorsLigne>();

        public int NombreVictoire { get => nombreVictoire; set => nombreVictoire = value; }
        public MatchHorsLigne MatchHorsLigne { get => matchHorsLigne; set => matchHorsLigne = value; }

        void Start()
        {
            matchHorsLigne = GameObject.Find("MatchHorsLigne").GetComponent<MatchHorsLigne>();
        }
    }
}
