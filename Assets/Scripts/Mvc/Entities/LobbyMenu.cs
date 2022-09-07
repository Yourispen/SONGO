using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mvc.Core;
using Mvc.Controllers;

namespace Mvc.Entities
{
    public class LobbyMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuLobby;
        [SerializeField] private GameObject creerMatch;
        [SerializeField] private GameObject rejoindreMatch;
        [SerializeField] PhotonManager photonManager;
        [SerializeField] private bool enConnexion;
        [SerializeField] private string code;




        public GameObject RejoindreMatch { get => rejoindreMatch; set => rejoindreMatch = value; }
        public GameObject MenuLobby { get => menuLobby; set => menuLobby = value; }
        public GameObject CreerMatch { get => creerMatch; set => creerMatch = value; }
        public PhotonManager PhotonManager { get => photonManager; set => photonManager = value; }
        public bool EnConnexion { get => enConnexion; set => enConnexion = value; }

        void Start()
        {
            enConnexion = false;
        }
        public void boutonCreerMatch()
        {
            if (enConnexion)
                return;
            if (ConnexionInternet.connect)
            {
                enConnexion = true;
                System.Random rand = new System.Random();
                string codeMatch = rand.Next(10000000, 100000000).ToString();
                PlayerPrefs.SetString("codeMatchEnCours", codeMatch);
                PlayerPrefs.SetInt("numPositionMatchEnCours", 1);
                Debug.Log("Code Match : " + codeMatch);
                photonManager.VeutCreerMatch = true;
                photonManager.initialiseJoueur();
            }
            else
            {
                enConnexion = false;
                Fonctions.afficherMsgScene(ConnexionInternet.msgNonConnecte, "erreur");
            }
        }
        public void saisirCodeMatch(string codeMacth)
        {
            code = codeMacth;
        }
        public void boutonRejoindreMatch()
        {
            if (enConnexion)
                return;
            if (ConnexionInternet.connect)
            {
                print("code : " + code);
                if (code.Length < 8)
                {
                    Fonctions.afficherMsgScene("Le code du match est incorrect", "erreur");
                    return;
                }
                enConnexion = true;
                PlayerPrefs.SetInt("numPositionMatchEnCours", 2);
                PlayerPrefs.SetString("codeMatchEnCours", code);
                Debug.Log("Code Match : " + code);
                photonManager.VeutRejoindreMatch = true;
                photonManager.initialiseJoueur();
            }
            else
            {
                enConnexion = false;
                Fonctions.afficherMsgScene(ConnexionInternet.msgNonConnecte, "erreur");
            }

        }

        public void boutonRetourScene()
        {
            if (enConnexion)
                return;
            Fonctions.changerDeScene("ScenePlay");
        }
    }
}