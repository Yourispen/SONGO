using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
namespace Mvc.Models
{
    public class JoueurOff : Joueur
    {
        [SerializeField] private int nombreVictoire;
        [SerializeField] private MatchHorsLigne matchHorsLigne;// = GameObject.Find("MatchHorsLigne").GetComponent<MatchHorsLigne>();
        [SerializeField] private Swipe swipe;
        public int NombreVictoire { get => nombreVictoire; set => nombreVictoire = value; }
        public MatchHorsLigne MatchHorsLigne { get => matchHorsLigne; set => matchHorsLigne = value; }
        public Swipe Swipe { get => swipe; set => swipe = value; }

        void Start()
        {
            this.nombreVictoire = 0;
            swipe=this.gameObject.GetComponent<Swipe>();
            swipe.Joueur=((Joueur)this);
            //matchHorsLigne = GameObject.Find("MatchHorsLigne").GetComponent<MatchHorsLigne>();
        }
    }
}
