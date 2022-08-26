using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.Networking;
using Mvc.Controllers;
using Mvc.Core;
using Mvc.Models;

namespace Mvc.Core
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private string idJoueur;
        [SerializeField] private string nomMatch;
        [SerializeField] private Dictionary<string, RoomInfo> listeMatchs;
        [SerializeField] SceneController sceneController;
        [SerializeField] LobbyMenu lobbyMenu;
        [SerializeField] private AttenteMenu attenteMenu;
        [SerializeField] private string versionJeu = "0.1";
        [SerializeField] private byte maxJoueurs = 2;
        public static bool connectePhoton = false;
        [SerializeField] private bool quitterMatch = false;
        [SerializeField] private GameObject joueurOnPrefab;
        [SerializeField] private GameObject joueurOn;



        public string NomMatch { get => nomMatch; set => nomMatch = value; }
        public string IdJoueur { get => idJoueur; set => idJoueur = value; }
        public SceneController SceneController { get => sceneController; set => sceneController = value; }
        public LobbyMenu LobbyMenu { get => lobbyMenu; set => lobbyMenu = value; }
        public bool QuitterMatch { get => quitterMatch; set => quitterMatch = value; }
        public AttenteMenu AttenteMenu { get => attenteMenu; set => attenteMenu = value; }

        private void Awake()
        {
            if (Fonctions.sceneActuelle("SceneLobbyMatchEnLigne"))
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                listeMatchs = new Dictionary<string, RoomInfo>();

                initialiseJoueur();
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            listeMatchs = new Dictionary<string, RoomInfo>();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("�tat du r�seau : "+PhotonNetwork.NetworkClientState);
            if (Fonctions.sceneActuelle("SceneLobbyMatchEnLigne"))
            {
                if (!ConnexionInternet.connect)
                {
                    Fonctions.afficherMsgScene(ConnexionInternet.msgNonConnecte, "erreur");
                }
                else if (ConnexionInternet.connect && !connectePhoton)
                {
                    initialiseJoueur();
                }
            }
        }

        public void initialiseJoueur()
        {
            if (ConnexionInternet.connect)
            {
                if (!PhotonNetwork.IsConnected)
                {
                    PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("id");
                    PhotonNetwork.GameVersion = versionJeu;
                    PhotonNetwork.ConnectUsingSettings();
                }
            }
            else
            {
                Fonctions.afficherMsgScene(ConnexionInternet.msgNonConnecte, "erreur");
            }
        }
        public override void OnConnected()
        {
            Debug.Log("Connexion à internet...");
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            connectePhoton = false;
            //PhotonNetwork.DestroyAll();
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " a quitté le photon...");
            if (quitterMatch)
            {
                string nomScene = "ScenePlay";
                Fonctions.changerDeScene(nomScene);
            }
        }
        public override void OnConnectedToMaster()
        {
            connectePhoton = true;
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " est connecé(e) à photon...");
            Fonctions.activerObjet(lobbyMenu.CreerMatch);
            Fonctions.activerObjet(lobbyMenu.RejoindreMatch);
        }

        public void quitterLobby()
        {
            if (!PhotonNetwork.IsConnected)
            {
                return;
            }
            if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                Debug.Log("Match Ouvert : " + PhotonNetwork.CurrentRoom.IsOpen);
                //PhotonNetwork.CurrentRoom.remove;
            }

            Debug.Log("Connecte au Lobby : " + PhotonNetwork.InLobby);
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }
            else
            {
                PhotonNetwork.Disconnect();
            }
        }
        public override void OnLeftLobby()
        {
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " a quitté le lobby...");
            listeMatchs.Clear();
            if (!connectePhoton)
                PhotonNetwork.Disconnect();
        }
        public void rejoindreMatch()
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
            //string matchEnCours = PlayerPrefs.GetString("codeMatchEnCours");
            //PhotonNetwork.JoinRoom(matchEnCours);
        }

        public override void OnJoinedLobby()
        {
            // whenever this joins a new lobby, clear any previous room lists
            listeMatchs.Clear();
            Debug.Log("Rejoins le Lobby");
        }
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            string matchEnCours = PlayerPrefs.GetString("codeMatchEnCours");
            Debug.Log("Lister match");
            int i = 0;
            foreach (RoomInfo match in roomList)
            {
                if (!match.IsOpen || !match.IsVisible || match.RemovedFromList)
                {
                    if (listeMatchs.ContainsKey(match.Name))
                    {
                        listeMatchs.Remove(match.Name);
                    }
                }
                else
                {
                    if (match.PlayerCount == 1)
                    {
                        i++;
                        Debug.Log("Match " + i + " : " + match.Name);
                        if (listeMatchs.ContainsKey(match.Name))
                        {
                            listeMatchs[match.Name] = match;
                        }
                        else
                        {
                            listeMatchs.Add(match.Name, match);
                        }
                    }
                }
            }
            if (listeMatchs.ContainsKey(matchEnCours))
            {
                PhotonNetwork.JoinRoom(matchEnCours);
            }
            else
            {
                Fonctions.afficherMsgScene("Le code du match est incorrect", "erreur");
                PhotonNetwork.LeaveLobby();
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("vous n'avez pas pu rejoindre le match");
            if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
            {
                Fonctions.changerDeScene("ScenePlay");
            }
            else
            {
                Fonctions.afficherMsgScene("Le code du match est incorrect", "erreur");
                PhotonNetwork.LeaveLobby();
            }
        }

        public void creerMatch(string codeMatch)
        {
            RoomOptions options = new RoomOptions { MaxPlayers = maxJoueurs, PlayerTtl = 10000, EmptyRoomTtl = 60000 };
            PhotonNetwork.CreateRoom(codeMatch, options, null);
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Match : " + PhotonNetwork.CurrentRoom.Name + " est crée");
            //PhotonNetwork.CurrentRoom.IsVisible = false;
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Fonctions.afficherMsgScene(ConnexionInternet.msgNonConnecte, "erreur");
        }

        public override void OnJoinedRoom()
        {
            listeMatchs.Clear();
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 5000;
            PhotonNetwork.MaxResendsBeforeDisconnect = 1;
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " a rejoint le Match " + PhotonNetwork.CurrentRoom.Name);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Je suis Master Client");
                PhotonNetwork.LoadLevel("SceneMatchEnLigne");
            }
            //instancierUnJoueur();
        }

        public void instancierUnJoueur()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.IsConnectedAndReady)
                {
                    joueurOn = PhotonNetwork.Instantiate(joueurOnPrefab.name, joueurOnPrefab.transform.position, Quaternion.identity);
                    joueurOn.gameObject.GetComponent<JoueurOn>().JoueurOnController = sceneController.JoueurOnController;
                    sceneController.JoueurOnController.JoueurOn = joueurOn.gameObject.GetComponent<JoueurOn>();
                    joueurOn.gameObject.GetComponent<JoueurOn>().Match = ((Match)sceneController.MatchEnLigneController.MatchEnligne);
                }
            }
        }
        public int nbJoueursDansLeMatch()
        {
            return PhotonNetwork.CurrentRoom.PlayerCount;
        }
    }
}
