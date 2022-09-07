using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
namespace Mvc.Entities
{
    public class JoueurOff : Joueur
    {
        void Start()
        {
            this.nombreVictoire = 0;
            swipe = this.gameObject.GetComponent<Swipe>();
            swipe.Joueur = ((Joueur)this);
            //matchHorsLigne = GameObject.Find("MatchHorsLigne").GetComponent<MatchHorsLigne>();
        }

    }
}
