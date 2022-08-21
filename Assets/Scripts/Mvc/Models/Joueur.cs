using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public class Joueur : ModelFirebase
    {
        [SerializeField] protected string surnom;
        [SerializeField] protected int numPosition;
        [SerializeField] protected Tour tour;

        public Joueur()
        {
            numPosition = 0;
            tour = Tour.Aucun;
        }

        public string Surnom { get => surnom; set => surnom = value; }
        public int NumPosition { get => numPosition; set => numPosition = value; }

    }
}
