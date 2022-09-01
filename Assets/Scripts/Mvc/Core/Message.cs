using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mvc.Core
{
    public class Message : MonoBehaviour
    {
        public Color colorError;
        public Color colorSucces;
        public Color colorPrimaire;
        public static string msgScene = "";
        public GameObject textMsgScene;
        public Image imageBackground;
        [SerializeField] private float tempsAffichage = 3;

        public float TempsAffichage { get => tempsAffichage; set => tempsAffichage = value; }

        //Name_P1.GetComponent<TMPro.TMP_Text>().text
        void OnEnable()
        {
            StartCoroutine(afficheMessage());
        }

        public IEnumerator afficheMessage()
        {
            textMsgScene.GetComponent<TMPro.TMP_Text>().text = msgScene;
            msgScene = "";
            yield return new WaitForSeconds(tempsAffichage);
            this.gameObject.SetActive(false);
        }
        public void imageError()
        {
            imageBackground.color = colorError;
        }
        public void imageSucces()
        {
            imageBackground.color = colorSucces;
        }
        public void imagePrimaire()
        {
            imageBackground.color = colorPrimaire;
        }
        public void attribuerMsg(string msg)
        {
            msgScene = msg;
        }
    }
}
