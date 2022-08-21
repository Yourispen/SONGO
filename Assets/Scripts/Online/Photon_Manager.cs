using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class Photon_Manager : MonoBehaviourPunCallbacks
{
    [Header("Nom du joueur")]
    [SerializeField] private string nom;
    [Header("Nom du Match")]
    [SerializeField] private string nom_du_match;
    [Header("Listes des matchs")]
    [SerializeField] private Dictionary<string, RoomInfo> liste_des_matchs;

    public Controller_Scene_Online controller_scene;

    public static int numero_joueur;

    public bool connected;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        connected = false;
        liste_des_matchs = new Dictionary<string, RoomInfo>();
        numero_joueur = 2;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("état du réseau : "+PhotonNetwork.NetworkClientState);
    }

    public void bouton_match()
    {
        StartCoroutine(checkInternetConnection((est_connecte) =>
        {
            if (est_connecte)
            {
                //si le joueur est connecté à internet
                controller_scene.Chargement.SetActive(true);
                controller_scene.Menu.SetActive(false);

                //va à la scène Match
                PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("pseudo");
                PhotonNetwork.ConnectUsingSettings();
            }
        }));

    }

    public void lister_les_matchs()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        Debug.Log("Vérifie s'il y a des matchs disponibles");

    }
    public void rejoindre_le_match(string _nom_du_match)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(_nom_du_match);
    }

    public void creer_le_match()
    {
        //creation du match
        nom_du_match = renommer_le_match();
        RoomOptions option_du_match = new RoomOptions();
        option_du_match.PlayerTtl = 0;
        option_du_match.EmptyRoomTtl = 30000;
        option_du_match.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(nom_du_match, option_du_match);
    }

    public string renommer_le_match()
    {
        string _nom_du_match="";
        System.Random rnd2 = new System.Random();
        int ascii_index2; //ASCII character codes 97-123
        for (int i = 1; i <= 15; i++)
        {
            ascii_index2 = rnd2.Next(97, 123); //ASCII character codes 97-123
            char lettre_minuscule = Convert.ToChar(ascii_index2); //produces any char a-z
            _nom_du_match += lettre_minuscule;
            if (i % 3 == 0 && i < 15)
                _nom_du_match += "-";
        }
        return _nom_du_match;
    }

    #region Photon Callback

    public override void OnConnected()
    {
        //base.OnConnected();
        Debug.Log("Connexion à internet...");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("vous n'avez pas pu créer le match");
        SceneManager.LoadScene("Online");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("vous n'avez pas pu rejoindre le match");
        SceneManager.LoadScene("Online");
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.Log(PhotonNetwork.LocalPlayer.NickName+" est connecté(e) à photon...");

        lister_les_matchs();
    }

    public override void OnCreatedRoom()
    {
        //base.OnCreatedRoom();
        numero_joueur = 1;
        Debug.Log("Match : "+PhotonNetwork.CurrentRoom.Name+" est crée");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 10000;
        PhotonNetwork.MaxResendsBeforeDisconnect = 1;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " a rejoint le Match "+ PhotonNetwork.CurrentRoom.Name);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Je suis Master Client");
            PhotonNetwork.LoadLevel("Match");
        }
    }

    public override void OnLeftLobby()
    {
        //base.OnLeftLobby();
        liste_des_matchs.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //base.OnRoomListUpdate(roomList);
        int i = 0;
       // Debug.Log("Le match " + PhotonNetwork.ro + " est invisible");
        foreach (RoomInfo match in roomList)
        {
            if (!match.IsOpen || !match.IsVisible || match.RemovedFromList)
            {
                if (liste_des_matchs.ContainsKey(match.Name))
                {
                    liste_des_matchs.Remove(match.Name);
                }

            }
            else
            {
                if (match.PlayerCount == 1)
                {
                    i++;
                    Debug.Log("Match " + i + " : " + match.Name);
                    if (liste_des_matchs.ContainsKey(match.Name))
                    {
                        liste_des_matchs[match.Name] = match;
                    }
                    else
                    {
                        liste_des_matchs.Add(match.Name, match);
                    }
                }
            }
        }
        if (i == 0)
        {
            Debug.Log("Il n'y a pas de Match disponible");
            creer_le_match();
        }
        else
        {
            foreach(var match in liste_des_matchs)
            {
                Debug.Log(PhotonNetwork.NickName + " va rejoindre le match " + match.Key);
                rejoindre_le_match(match.Key);
                break;
            }
        }
    }

    #endregion

    #region Coroutines

    IEnumerator checkInternetConnection(Action<bool> action)
    {
        Debug.Log("debut");
        UnityWebRequest request = new("http://www.bing.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log("Error");
            controller_scene.TextError.SetActive(true);
            yield return new WaitForSeconds(4f);
            controller_scene.TextError.SetActive(false);
            action(false);
        }
        else
        {
            Debug.Log("Succes");
            action(true);
        }
    }

    #endregion
}