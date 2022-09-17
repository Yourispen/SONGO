using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mvc.Core;
using TMPro;
using Photon.Pun;

namespace Mvc.Entities
{
    public class ChatMenu : MonoBehaviour
    {
        [SerializeField] private Button boutonChat;
        [SerializeField] private GameObject menuChat;
        [SerializeField] private Button boutonEmoji;
        [SerializeField] private Button boutonMessage;
        [SerializeField] private MatchEnLigne matchEnLigne;
        [SerializeField] private GameObject messageChatPrefab;
        [SerializeField] private Transform listeParentMessageChat;
        [SerializeField] private GameObject listeMessageChatScrollView;
        [SerializeField] private List<GameObject> listeBoutonMessage = new List<GameObject>();
        [SerializeField] private List<int> packMessage1 = new List<int> { 0, 1, 2, 3, 5, 6 };
        [SerializeField] private List<int> packMessage2 = new List<int> { 4, 7, 8, 9, 10 };
        [SerializeField] private List<int> packMessage3 = new List<int> { 11, 12, 13, 14 };
        [SerializeField] private List<List<int>> listePackMessage = new List<List<int>>();
        [SerializeField] private GameObject listeEmojiChatScrollView;
        [SerializeField] private List<GameObject> listeBoutonEmoji = new List<GameObject>();
        [SerializeField] private List<GameObject> listePackEmoji = new List<GameObject>();
        [SerializeField] private List<Sprite> listeEmojiSquareBleu = new List<Sprite>();
        [SerializeField] private Color couleurSelect;
        [SerializeField] private Color couleurNormal;
        [SerializeField] private bool enChat;

        public Button BoutonChat { get => boutonChat; set => boutonChat = value; }
        public GameObject MenuChat { get => menuChat; set => menuChat = value; }
        public Button BoutonEmoji { get => boutonEmoji; set => boutonEmoji = value; }
        public Button BoutonMessage { get => boutonMessage; set => boutonMessage = value; }
        public List<GameObject> ListeBoutonMessage { get => listeBoutonMessage; set => listeBoutonMessage = value; }
        public MatchEnLigne MatchEnLigne { get => matchEnLigne; set => matchEnLigne = value; }
        public List<GameObject> ListeBoutonEmoji { get => listeBoutonEmoji; set => listeBoutonEmoji = value; }
        public bool EnChat { get => enChat; set => enChat = value; }

        void Start()
        {
            EnChat = false;
            instancierMenuChat();
        }
        public void instancierMenuChat()
        {
            listePackMessage.Add(packMessage1);
            listePackMessage.Add(packMessage2);
            listePackMessage.Add(packMessage3);
            foreach (var packMessage in listePackMessage)
            {
                foreach (var message in packMessage)
                {
                    Fonctions.activerObjet(listeBoutonMessage[message]);
                }
                //packMessage.ForEach(value => Debug.Log(value));
            }
            foreach (var packEmoji in listePackEmoji)
            {
                Fonctions.activerObjet(packEmoji);
            }
        }
        public void buttonChat()
        {
            enChat = !enChat;
            initialiseChatMenu();
            menuChat.SetActive(!menuChat.activeSelf);
        }
        public void buttonEmoji()
        {
            boutonMessage.GetComponent<Image>().color = couleurNormal;
            boutonEmoji.GetComponent<Image>().color = couleurSelect;
            Fonctions.activerObjet(listeEmojiChatScrollView);
            Fonctions.desactiverObjet(listeMessageChatScrollView);
        }
        public void buttonMessage()
        {
            boutonEmoji.GetComponent<Image>().color = couleurNormal;
            boutonMessage.GetComponent<Image>().color = couleurSelect;
            Fonctions.activerObjet(listeMessageChatScrollView);
            Fonctions.desactiverObjet(listeEmojiChatScrollView);

        }
        public void emoji(int indiceEmoji)
        {
            buttonChat();
            int numPosition = PlayerPrefs.GetInt("numPositionMatchEnCours");
            if (numPosition == 1)
            {
                if (PhotonNetwork.IsConnected)
                {
                    matchEnLigne.Joueur1.PhotonView.RPC("envoyerEmoji", RpcTarget.AllBuffered, indiceEmoji, numPosition);
                }
            }
            else
            {
                if (PhotonNetwork.IsConnected)
                {
                    matchEnLigne.Joueur2.PhotonView.RPC("envoyerEmoji", RpcTarget.AllBuffered, indiceEmoji, numPosition);
                }
            }
        }
        public void message(int indiceMessage)
        {
            buttonChat();
            int numPosition = PlayerPrefs.GetInt("numPositionMatchEnCours");
            if (numPosition == 1)
            {
                if (PhotonNetwork.IsConnected)
                {
                    matchEnLigne.Joueur1.PhotonView.RPC("envoyerMessage", RpcTarget.AllBuffered, indiceMessage, numPosition);
                }
            }
            else
            {
                if (PhotonNetwork.IsConnected)
                {
                    matchEnLigne.Joueur2.PhotonView.RPC("envoyerMessage", RpcTarget.AllBuffered, indiceMessage, numPosition);
                }
            }
        }
        public void initialiseChatMenu()
        {
            Fonctions.desactiverObjet(listeMessageChatScrollView);
            Fonctions.activerObjet(listeEmojiChatScrollView);
            boutonEmoji.GetComponent<Image>().color = couleurSelect;
            boutonMessage.GetComponent<Image>().color = couleurNormal;
        }

    }
}
