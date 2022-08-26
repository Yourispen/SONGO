using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mvc.Core
{
    public class Message : MonoBehaviour
    {
        public Color colorError;
        public Color colorSucces;
        public static string msgScene = "";
        public GameObject textMsgScene;
        public Image imageBackground;
        [SerializeField] private float tempsAffichage = 3;
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
        public void attribuerMsg(string msg)
        {
            msgScene = msg;
        }
    }
}
