using Mvc.Core;
using Mvc.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Mvc.Controllers
{
    public class ConnexionCompteController : MonoBehaviour, IController
    {
        [SerializeField] private GameObject connexionComptePrefab;
        [SerializeField] private ConnexionCompte connexionCompte;
        [SerializeField] private SceneController sceneController;
        [SerializeField] public Button boutonConnexionFacebook;

        public SceneController SceneController { get => sceneController; set => sceneController = value; }

        void OnEnable()
        {
            //connexionCompte = connexionComptePrefab.GetComponent<ConnexionCompte>();
            connexionCompte = Fonctions.instancierObjet(connexionComptePrefab).GetComponent<ConnexionCompte>();
            connexionCompte.ConnexionCompteController = this;
            //GameObject.Find("BoutonConnexionFacebook").GetComponent<Button>(); // connexionAuCompteFacebook();
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

        public void boutonConnexionAuCompteFacebook()
        {
            //this. = GameObject.Find("connexionCompteController").GetComponent<ConnexionCompteController>();
            if (ConnexionInternet.connect)
            {
                Debug.Log("Connexion Facebook");
                PlayerPrefs.SetInt("idConnexionCompte", 1);
                PlayerPrefs.SetInt("etatConnexionCompte", 1);
                PlayerPrefs.SetInt("typePion", 0);
                PlayerPrefs.SetInt("couleurPion", 0);
                PlayerPrefs.SetInt("couleurTable", 0);
                Fonctions.debutChargement();
                connexionCompte.connexionFacebook();
            }
            else
            {
                //Fonctions.activerObjet(sceneController.PageDeConnexionCompte);
                //Fonctions.finChargement();
                Fonctions.afficherMsgScene(ConnexionInternet.msgNonConnecte, "erreur");
            }
        }
        public void boutonConnexionInvite()
        {
            PlayerPrefs.SetInt("idConnexionCompte", 0);
            PlayerPrefs.SetInt("idNiveau", 1);
            PlayerPrefs.SetInt("etatConnexionCompte", 1);
            PlayerPrefs.SetInt("typePion", 0);
            PlayerPrefs.SetInt("couleurPion", 0);
            PlayerPrefs.SetInt("couleurTable", 0);
            //
            //PlayerPrefs.SetString("id", "z6hhsbMJRyaPRGSaDTHpNQ8QiKj2");
            //
            Fonctions.desactiverObjet(sceneController.PageDeConnexionCompte);
        }
    }
}