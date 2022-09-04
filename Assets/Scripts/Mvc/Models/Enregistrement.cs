using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
using Mvc.Controllers;
using UnityEngine.SceneManagement;


namespace Mvc.Models
{
    public class Enregistrement : MonoBehaviour
    {
        [SerializeField] private string nomJoueur1;
        [SerializeField] private string nomJoueur2;
        [SerializeField] private Match match;
        [SerializeField] private SceneController sceneController;
        [SerializeField] private string nomScene;

        public Match Match { get => match; set => match = value; }
        public string NomJoueur1 { get => nomJoueur1; set => nomJoueur1 = value; }
        public string NomJoueur2 { get => nomJoueur2; set => nomJoueur2 = value; }

        public void saisirNomJoueur1(string nom)
        {
            nomJoueur1 = nom;
        }
        public void saisirNomJoueur2(string nom)
        {
            nomJoueur2 = nom;
        }
        public void boutonEntrer()
        {
            if (nomJoueur1.Length > 1 && nomJoueur2.Length > 1)
            {
                /*  match.Joueur1.Surnom = nomJoueur1;
                 match.Joueur2.Surnom = nomJoueur2;
                 match.ScoreMatch.afficherScoreMatch(); */
                //Fonctions.desactiverObjet(this.gameObject);
                sceneController.commencerMatch1vs1();
            }
            else
            {
                Fonctions.afficherMsgScene("Deux lettres au minimum ", "erreur");
            }
        }
        public void boutonRetour()
        {
            if (SceneManager.GetActiveScene().name == "SceneMatch1vs1")
            {
                nomScene = "ScenePlay";
                Fonctions.changerDeScene(nomScene);
            }

        }
    }
}
