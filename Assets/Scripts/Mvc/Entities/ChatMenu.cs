using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mvc.Core;
using TMPro;

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
        [SerializeField] private List<GameObject> listeBoutonMessage = new List<GameObject>();
        [SerializeField] private List<int> packMessage1 = new List<int> { 0, 1, 2, 3, 5, 6 };
        [SerializeField] private List<int> packMessage2 = new List<int> { 4, 7, 8, 9, 10, 11, 12 };
        [SerializeField] private List<GameObject> listeBoutonEmoji = new List<GameObject>();
        [SerializeField] private List<int> packEmoji1 = new List<int> { 1, 9, 17, 15 };
        [SerializeField] private List<int> packEmoji2 = new List<int> { 0, 11, 7, 5 };
        [SerializeField] private List<int> packEmoji3 = new List<int> { 10, 2, 12, 14 };
        [SerializeField] private List<int> packEmoji4 = new List<int> { 6, 13, 3, 16 };
        [SerializeField] private List<int> packEmoji5 = new List<int> { 8, 4, 22, 18 };
        [SerializeField] private List<int> packEmoji6 = new List<int> { 21, 23, 20, 19 };
        [SerializeField] private List<Sprite> listeEmojiSquareBleu = new List<Sprite>();



        [SerializeField] private OutilsJoueur outilsJoueur;

        public Button BoutonChat { get => boutonChat; set => boutonChat = value; }
        public GameObject MenuChat { get => menuChat; set => menuChat = value; }
        public Button BoutonEmoji { get => boutonEmoji; set => boutonEmoji = value; }
        public Button BoutonMessage { get => boutonMessage; set => boutonMessage = value; }
        public List<GameObject> ListeBoutonMessage { get => listeBoutonMessage; set => listeBoutonMessage = value; }
        public MatchEnLigne MatchEnLigne { get => matchEnLigne; set => matchEnLigne = value; }

        void Start()
        {
            instancierMenuChat();
        }
        public void instancierMenuChat()
        {
            foreach (var message in packMessage1)
            {
                Fonctions.activerObjet(listeBoutonMessage[message]);
            }
            foreach (var emoji in packEmoji1)
            {
                Fonctions.activerObjet(listeBoutonEmoji[emoji]);
            }
        }
        public void buttonChat()
        {
            //matchEnLigne.PauseMenu.EnPause = !matchEnLigne.PauseMenu.EnPause;
            menuChat.SetActive(!menuChat.activeSelf);
        }
        public void buttonEmoji()
        {

        }
        public void buttonMessage()
        {

        }
        public void emoji(int indiceEmoji)
        {
            StartCoroutine(outilsJoueur.afficherEmoji(listeBoutonEmoji[indiceEmoji].GetComponentInChildren<Image>(), 1));
        }
        public void message(int indiceMessage)
        {
            //Debug.Log(listeBoutonMessage[indiceMessage].GetComponentInChildren<TMPro.TMP_Text>().text);
            StartCoroutine(outilsJoueur.afficherMessage(listeBoutonMessage[indiceMessage].GetComponentInChildren<TMPro.TMP_Text>().text, 1));
        }

    }
}
