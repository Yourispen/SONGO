using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Entities
{
    public class JoueurNonConnecte : JoueurOn
    {

        void Start()
        {
            this.match = ((Match)GameObject.Find("matchEnligne").GetComponent<MatchEnLigne>());
        }
    }
}
