using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public class JoueurNonConnecte : JoueurOn
    {
        [SerializeField] private Swipe swipe;

        void Start()
        {
            this.matchEnLigne = GameObject.Find("matchEnligne").GetComponent<MatchEnLigne>();
        }
    }
}
