using Mvc.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class Ami : JoueurNonConnecte
    {
        [SerializeField] private JoueurConnecte joueurConnecte; //= GameObject.Find("JoueurConnecte").GetComponent<JoueurConnecte>();

        public JoueurConnecte JoueurConnecte { get => joueurConnecte; set => joueurConnecte = value; }

        void Start()
        {
            joueurConnecte = GameObject.Find("JoueurConnecte").GetComponent<JoueurConnecte>();
        }
    }
}
