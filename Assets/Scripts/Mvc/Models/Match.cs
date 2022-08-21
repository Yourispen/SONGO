using Mvc.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public abstract class Match : ModelFirebase
    {
        public const int NOMBREMAXJOUEUR = 2;
        [SerializeField] protected ResultatMatch resultatDuMatch;
        [SerializeField] protected EtatMatch etatDuMatch;
        [SerializeField] protected TypeMatch typeDuMatch;

        [SerializeField] protected PauseMenu pauseMenu;

        protected Match()
        {
            //this.pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        }

        public ResultatMatch ResultatDuMatch { get => resultatDuMatch; set => resultatDuMatch = value; }
        public EtatMatch EtatDuMatch { get => etatDuMatch; set => etatDuMatch = value; }
        public TypeMatch TypeDuMatch { get => typeDuMatch; set => typeDuMatch = value; }

        protected abstract void initialiseJoueurs();
    }
}