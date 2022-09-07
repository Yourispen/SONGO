using Mvc.Core;
using Mvc.Entities;
using UnityEngine;

namespace Mvc.Controllers
{
    public class MatchHorsLigneController : MonoBehaviour, IController
    {
        [SerializeField] private GameObject matchHorslignePrefab;
        [SerializeField] private MatchHorsLigne matchHorsligne;
        [SerializeField] private SceneController sceneController;

        public SceneController SceneController { get => sceneController; set => sceneController = value; }
        public MatchHorsLigne MatchHorsligne { get => matchHorsligne; set => matchHorsligne = value; }

        private void OnEnable()
        {
            matchHorsligne = Fonctions.instancierObjet(matchHorslignePrefab).GetComponent<MatchHorsLigne>();
            matchHorsligne.MatchHorsLigneController = this;
        }
        public void lister(bool single = false)
        {

        }
        public void ajouter()
        {

        }
        public void supprimer()
        {

        }
        public void modifier()
        {

        }

    }
}