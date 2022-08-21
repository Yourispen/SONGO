using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Variables
    [SerializeField] private GameObject player_prefab;
    public bool debut_du_match;
    public GameObject Controller_Match_Online;
    public GameObject Controller_Scene_Match;

    public Controller_Scene_Match controller_scene;
    public Controller_Match_Online controller_match;

    public Connexion connexion;

    public int numero_joueur;

    public Joueur joueur_1;
    public Joueur joueur_2;

    float temps_actuel;
    float temps_depart;

    public DateTime focusDateTime;
    public int intervalle;
    public bool arriere_plan;
    bool applicationPause = false;

    //lorsqu'un joueur se reconnecte et qu'il ne trouve pas l'autre joueur
    [SerializeField] private bool il_manque_un_joueur;

    //le nom du match en cours
    public string match_en_cours;

    //le nombre de fois que le joueur s'est reconnecté
    [SerializeField] private int compteur_de_reconnexion;

    #endregion

    #region Fonctions Principales
    // Start is called before the first frame update

    private void Awake()
    {
        numero_joueur = Photon_Manager.numero_joueur;
        arriere_plan = false;
    }
    void Start()
    {
        il_manque_un_joueur = false;
        applicationPause = true;
        compteur_de_reconnexion = 0;
        //PhotonNetwork.CountOfPlayersInRooms.
        match_en_cours = PhotonNetwork.CurrentRoom.Name;
        //DisconnectCause.ClientTimeout = 1;
        //if (PhotonNetwork.IsMasterClient)
        //{
        
        //}
        initialise_GameCenter();
        debut_du_match = false;
        temps_actuel = 0;
        temps_depart = 0;
    }
    private void Update()
    {
        if (!debut_du_match)
        {
            if (PhotonNetwork.PlayerList.Length == 2)
            {
                debut_du_match = true;
                match_debute();
            }
        }
        temps_actuel = Time.timeSinceLevelLoad;
        if (temps_actuel - temps_depart >= 30 && !debut_du_match)
        {
            temps_depart = Time.timeSinceLevelLoad;
            quitter_le_match();
        }
        //quand un joueur se reconnecte et ne trouve personne dans le match
        if (PhotonNetwork.InRoom && debut_du_match)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount < 2 && compteur_de_reconnexion > 0)
            {
                if (!il_manque_un_joueur)
                {
                    il_manque_un_joueur = true;
                    adversaire_quitte_le_match();
                }

            }
        }
    }

    #endregion

    #region Fonctions Void

    public void quitter_le_match()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("je suis dans la room, donc je la quitte.");
            PhotonNetwork.LeaveRoom();
        }
        else if (PhotonNetwork.IsConnected)
        {
            Debug.Log("je suis connecté, donc je me déconnecte avant de quitter la scène.");
            PhotonNetwork.Disconnect();
        }
        else
        {
            Debug.Log("je ne suis pas connecté, donc je quitte la scène.");
            if (controller_scene.quitter)
            {
                SceneManager.LoadScene("Online");
            }
        }

    }

    void match_debute()
    {
        Debug.Log(PhotonNetwork.PlayerListOthers[0].ToString() + " a " + PhotonNetwork.PlayerListOthers[0].ToString().Length + " caractères");
        int nombre_de_lettres = PhotonNetwork.PlayerListOthers[0].ToString().Length - 6;
        PlayerPrefs.SetString("adversaire", PhotonNetwork.PlayerListOthers.GetValue(0).ToString().Substring(5, nombre_de_lettres));
        PhotonNetwork.CurrentRoom.IsVisible = false;
        Controller_Scene_Match.GetComponent<Controller_Scene_Match>().enabled = true;
        Controller_Match_Online.GetComponent<Controller_Match_Online>().enabled = true;
        if (numero_joueur == 1)
        {
            controller_scene.joueur_1.donner_le_nom(PlayerPrefs.GetString("pseudo"));
            controller_scene.joueur_2.donner_le_nom(PlayerPrefs.GetString("adversaire"));
        }
        else if (numero_joueur == 2)
        {
            controller_scene.joueur_2.donner_le_nom(PlayerPrefs.GetString("pseudo"));
            controller_scene.joueur_1.donner_le_nom(PlayerPrefs.GetString("adversaire"));
        }
        controller_scene.chargement.SetActive(false);
    }

    void initialise_GameCenter()
    {
        if (GameObject.Find("Joueur") == null)
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.Instantiate(player_prefab.name, player_prefab.transform.position, Quaternion.identity);
            }
        }
    }

    void adversaire_quitte_le_match()
    {
        string nom_joueur;
        if (numero_joueur == 1)
            nom_joueur = joueur_2.recupere_le_nom();
        else
            nom_joueur = joueur_1.recupere_le_nom();
        Debug.Log(nom_joueur + " a quitté le match.");

        if (!controller_match.match_fini)
        {
            controller_scene.en_pause = true;
            string msg = nom_joueur + " se reconnecte.\nVeuillez ne pas quitter le match.";
            controller_scene.afficher_message(msg, true);
            intervalle = 35;
            controller_scene.instancier_timer();
        }
    }

    public void rejoindre_le_match(string _nom_du_match)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(_nom_du_match);
    }

    #endregion

    #region Commentaires

    #endregion

    #region Photon  Callbacks

    public override void OnLeftRoom()
    {
        Debug.Log("Je quitte le match.");
        if (debut_du_match)
        {
            if (!controller_match.match_fini && !controller_scene.quitter && !arriere_plan)
            {
                if (GameObject.Find("Timer") == null)
                {
                    controller_scene.en_pause = true;
                    controller_match.deconnexion = true;
                    connexion.connect = false;
                    string msg = "Connexion impossible.\nEn attente de reconnexion...";
                    controller_scene.afficher_message(msg, true);
                    intervalle = 30;
                    controller_scene.instancier_timer();
                }
            }
            else if (controller_scene.quitter || controller_match.match_fini || arriere_plan)
            {
                PhotonNetwork.Disconnect();
            }
        }
        else
        {
            PhotonNetwork.Disconnect();
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause);
        Debug.Log("je me déconnecte");
        if (controller_scene.quitter || !debut_du_match)
            SceneManager.LoadScene("Online");

        /*switch (cause)
        {
            // the list here may be non exhaustive and is subject to review
            case DisconnectCause.ServerTimeout:
                SceneManager.LoadScene("Online");
                break;
            case DisconnectCause.Exception:
                SceneManager.LoadScene("Online");
                break;
            case DisconnectCause.DisconnectByServerReasonUnknown:
                SceneManager.LoadScene("Online");
                break;
        }*/
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (debut_du_match && !controller_match.match_fini)
        {
            controller_scene.en_pause = false;
            Debug.Log(newPlayer.NickName + " est dans le match.");
            controller_scene.detruit_timer();
            controller_scene.afficher_message("", false);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        adversaire_quitte_le_match();
    }



    #endregion

    #region Reconnexion

    public void active_reconnexion()
    {
        if (!controller_match.match_fini)
            StartCoroutine("reconnexion");
    }
    private IEnumerator reconnexion()
    {
        PhotonNetwork.Disconnect();
        yield return new WaitForSeconds(0.3f);

        Debug.Log("Je fais la reconnexion au match.");

        PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("pseudo");
        PhotonNetwork.ConnectUsingSettings();

    }
    public override void OnConnected()
    {
        Debug.Log("Connexion à internet...");
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.MaxResendsBeforeDisconnect = 1;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " est connecté(e) à photon...");
        rejoindre_le_match(match_en_cours);
    }
    public override void OnJoinedRoom()
    {
        if (debut_du_match && !controller_match.match_fini)
        {
            connexion.connect = true;
            compteur_de_reconnexion += 1;
            il_manque_un_joueur = false;
            controller_scene.en_pause = false;
            controller_scene.detruit_timer();
            controller_match.deconnexion = false;
            PhotonNetwork.MaxResendsBeforeDisconnect = 1;
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " s'est reconnecté au Match " + PhotonNetwork.CurrentRoom.Name);
            initialise_GameCenter();
            controller_scene.afficher_message("", false);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Je n'ai pas pu rejoindre le match car " + message);
        active_reconnexion();

    }

    private void OnApplicationPause(bool pause)
    {
        if (applicationPause)
        {
            if (pause)
            {
                if (!controller_match.match_fini)
                {
                    if (debut_du_match)
                    {
                        focusDateTime = DateTime.Now;
                        Debug.Log("pause : je suis en arriere plan");
                        arriere_plan = true;
                        string msg = "Connexion impossible.\nEn attente de reconnexion...";
                        controller_scene.afficher_message(msg, true);
                        controller_match.deconnexion = true;
                    }
                    quitter_le_match();
                }
            }
            else
            {
                Debug.Log("pause : je suis en avant plan");
                if (!PhotonNetwork.InRoom && debut_du_match)
                {
                    intervalle = 30 - (int)(DateTime.Now - focusDateTime).TotalSeconds;
                    arriere_plan = false;
                    if (intervalle >= 2)
                        connexion.connect = false;
                    controller_scene.instancier_timer();
                }
            }
        }
    }

    #endregion
}
