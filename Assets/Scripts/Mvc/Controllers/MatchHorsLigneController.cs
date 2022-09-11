using Mvc.Core;
using Mvc.Entities;
using UnityEngine;

namespace Mvc.Controllers
{
    public class MatchHorsLigneController : MonoBehaviour, IController
    {
        [SerializeField] private GameObject matchHorslignePrefab;
        [SerializeField] private GameObject matchEntrainementPrefab;
        [SerializeField] private MatchHorsLigne matchHorsligne;
        [SerializeField] private SceneController sceneController;

        public SceneController SceneController { get => sceneController; set => sceneController = value; }
        public MatchHorsLigne MatchHorsligne { get => matchHorsligne; set => matchHorsligne = value; }

        public void instancierLeMatch()
        {
            if (Fonctions.sceneActuelle("SceneMatch1vs1"))
            {
                matchHorsligne = Fonctions.instancierObjet(matchHorslignePrefab).GetComponent<MatchHorsLigne>();
                matchHorsligne.MatchHorsLigneController = this;
                matchHorsligne.debuterMatch();
            }
            else if (Fonctions.sceneActuelle("SceneMatchEntrainement"))
            {
                matchHorsligne = Fonctions.instancierObjet(matchEntrainementPrefab).GetComponent<MatchEntrainement>();
                matchHorsligne.MatchHorsLigneController = this;
                ((MatchEntrainement)matchHorsligne).debuterMatch();
            }
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