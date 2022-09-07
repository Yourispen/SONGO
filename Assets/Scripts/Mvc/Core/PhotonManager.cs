using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Mvc.Controllers;
using Mvc.Entities;

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
        [SerializeField] private bool quitterMatch;
        [SerializeField] private bool veutCreerMatch;
        [SerializeField] private bool veutRejoindreMatch;
        [SerializeField] private GameObject joueurOnPrefab;
        [SerializeField] private GameObject joueurOn;
        [SerializeField] private string nomSceneRetour;
        [SerializeField] private string nomSceneMatch = "SceneMatchEnLigne";
        public static bool reconnect;
        [SerializeField] private bool aderversaireDeconnecte;




        public string NomMatch { get => nomMatch; set => nomMatch = value; }
        public string IdJoueur { get => idJoueur; set => idJoueur = value; }
        public SceneController SceneController { get => sceneController; set => sceneController = value; }
        public LobbyMenu LobbyMenu { get => lobbyMenu; set => lobbyMenu = value; }
        public bool QuitterMatch { get => quitterMatch; set => quitterMatch = value; }
        public AttenteMenu AttenteMenu { get => attenteMenu; set => attenteMenu = value; }
        public bool VeutCreerMatch { get => veutCreerMatch; set => veutCreerMatch = value; }
        public bool VeutRejoindreMatch { get => veutRejoindreMatch; set => veutRejoindreMatch = value; }
        public bool AderversaireDeconnecte { get => aderversaireDeconnecte; set => aderversaireDeconnecte = value; }

        private void Awake()
        {
            if (Fonctions.sceneActuelle("SceneLobbyMatchEnLigne"))
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                listeMatchs = new Dictionary<string, RoomInfo>();
                Fonctions.activerObjet(lobbyMenu.CreerMatch);
                Fonctions.activerObjet(lobbyMenu.RejoindreMatch);
                veutCreerMatch = false;
                veutRejoindreMatch = false;
                quitterMatch = false;
                nomSceneRetour = "ScenePlay";
            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                aderversaireDeconnecte = false;
                nomSceneRetour = "SceneLobbyMatchEnLigne";
            }
            reconnect = false;
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
            }
            if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                if (ConnexionInternet.connect && sceneController.MatchEnLigneController.MatchEnligne.EtatDuMatch == EtatMatch.EnCours && !PhotonNetwork.IsConnected)
                {
                    seReconnecter();
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
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " a quitté le photon...");
            if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                if (sceneController.MatchEnLigneController.MatchEnligne.EtatDuMatch == EtatMatch.EnCours)
                {
                    reconnect = true;
                    sceneController.MatchEnLigneController.MatchEnligne.JoueurDeconnecte = true;
                }
            }
            if (quitterMatch)
            {
                Fonctions.changerDeScene(nomSceneRetour);
            }
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " est connecé(e) à photon...");
            if (Fonctions.sceneActuelle("SceneLobbyMatchEnLigne"))
            {
                if (veutCreerMatch)
                {
                    string matchEnCours = PlayerPrefs.GetString("codeMatchEnCours");
                    creerMatch(matchEnCours);
                }
                else if (veutRejoindreMatch)
                {
                    rejoindreMatch();
                }
            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                if (reconnect)
                {
                    PhotonNetwork.JoinRoom(PlayerPrefs.GetString("codeMatchEnCours"));
                }
            }
        }

        public void quitterLobby()
        {
            if (!PhotonNetwork.IsConnected)
            {
                Fonctions.changerDeScene(nomSceneRetour);
                return;
            }

            if (PhotonNetwork.InRoom)
            {
                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                {
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    Debug.Log("Match Ouvert : " + PhotonNetwork.CurrentRoom.IsOpen);
                }
            }

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
            PhotonNetwork.Disconnect();
        }
        public void rejoindreMatch()
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        public override void OnJoinedLobby()
        {
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
                lobbyMenu.EnConnexion = false;
                veutCreerMatch = false;
                veutRejoindreMatch = false;
                Fonctions.afficherMsgScene("Le code du match est incorrect", "erreur");
                PhotonNetwork.LeaveLobby();
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("vous n'avez pas pu rejoindre le match");
            if (Fonctions.sceneActuelle("SceneLobbyMatchEnLigne"))
            {
                lobbyMenu.EnConnexion = false;
                veutCreerMatch = false;
                veutRejoindreMatch = false;
                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                {
                    Fonctions.changerDeScene(nomSceneRetour);
                }
                else
                {
                    Fonctions.afficherMsgScene("Le code du match est incorrect", "erreur");
                    PhotonNetwork.LeaveLobby();
                }
            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                reconnect = false;
            }
        }

        public void creerMatch(string codeMatch)
        {
            initialiseJoueur();
            RoomOptions options = new RoomOptions { MaxPlayers = maxJoueurs, PlayerTtl = 0, EmptyRoomTtl = 5 };
            PhotonNetwork.CreateRoom(codeMatch, options, null);
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Match : " + PhotonNetwork.CurrentRoom.Name + " est crée");
            //PhotonNetwork.CurrentRoom.IsVisible = false;
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            lobbyMenu.EnConnexion = true;
            veutCreerMatch = false;
            Fonctions.afficherMsgScene(ConnexionInternet.msgNonConnecte, "erreur");
        }

        public override void OnJoinedRoom()
        {
            listeMatchs.Clear();
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 1000;
            PhotonNetwork.MaxResendsBeforeDisconnect = 3;
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " a rejoint le Match " + PhotonNetwork.CurrentRoom.Name);
            if (Fonctions.sceneActuelle("SceneLobbyMatchEnLigne"))
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("Je suis Master Client");
                    PhotonNetwork.LoadLevel(nomSceneMatch);
                }
            }
            if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                if (reconnect)
                {
                    instancierUnJoueur();
                }

            }
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
        public override void OnLeftRoom()
        {

        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log(otherPlayer.NickName + " a s'est déconnecté");
            if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                aderversaireDeconnecte = true;
                if (sceneController.MatchEnLigneController.MatchEnligne.EtatDuMatch == EtatMatch.EnCours)
                {
                    reconnect = true;
                    sceneController.MatchEnLigneController.MatchEnligne.JoueurDeconnecte = true;
                }
            }
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {

        }

        public void seReconnecter()
        {
            reconnect = true;
            initialiseJoueur();
        }
    }
}
