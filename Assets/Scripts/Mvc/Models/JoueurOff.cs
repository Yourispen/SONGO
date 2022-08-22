using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
namespace Mvc.Models
{
    public class JoueurOff : Joueur
    {
        [SerializeField] private int nombreVictoire;
        public int NombreVictoire { get => nombreVictoire; set => nombreVictoire = value; }

        void Start()
        {
            this.nombreVictoire = 0;
            swipe = this.gameObject.GetComponent<Swipe>();
            swipe.Joueur = ((Joueur)this);
            //matchHorsLigne = GameObject.Find("MatchHorsLigne").GetComponent<MatchHorsLigne>();
        }
        public override void victoireJoueur()
        {
            nombreVictoire += 1;
            swipe.enabled = true;
        }
        public override void defaiteJoueur()
        {
            swipe.enabled = false;
            nombreVictoire += 1;
        }
    }
}
