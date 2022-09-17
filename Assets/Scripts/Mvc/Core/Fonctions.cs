using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mvc.Entities;
using Mvc.Controllers;
using UnityEngine.UI;

namespace Mvc.Core
{
    public class Fonctions : MonoBehaviour
    {
        public static void faireVibrer()
        {
            //Handheld.Vibrate();
        }

        public static void nonVeille()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public static void changerDeScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public static void changerDeSceneAsynchrone(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }

        public WWWForm ajoutFormulaire(Dictionary<string, string> datas)
        {
            WWWForm form = new WWWForm();
            foreach (var data in datas)
            {
                //form.AddField()
            }
            return form;
        }

        public static GameObject instancierObjet(GameObject gameObjetPrefab, Vector3 position = new Vector3(), string nomGameObject = "")
        {
            if (GameObject.Find(gameObjetPrefab.name) != null)
            {
                return GameObject.Find(gameObjetPrefab.name);
            }
            if (position.x == 0.00 && position.y == 0.00 && position.y == 0.00)
            {
                position = gameObjetPrefab.transform.position;
            }
            GameObject gameObject = Instantiate(gameObjetPrefab, position, Quaternion.identity);
            if (nomGameObject == "")
                gameObject.name = gameObjetPrefab.name;
            else
                gameObject.name = nomGameObject;
            return gameObject;
        }

        public static void detruireObjet(GameObject gameObject)
        {
            if (GameObject.Find(gameObject.name) != null)
                Destroy(gameObject);
        }

        public static Dictionary<string, object> toDictinary(string table)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (table.CompareTo("songo_joueur_online") == 0)
            {
                data["surnom"] = PlayerPrefs.GetString("surnom");
                data["dateInscription"] = PlayerPrefs.GetString("dateInscription");
                data["heureInscription"] = PlayerPrefs.GetString("heureInscription");
                data["email"] = PlayerPrefs.GetString("email");
                data["idConnexionCompte"] = PlayerPrefs.GetInt("idConnexionCompte");
                data["idNiveau"] = PlayerPrefs.GetInt("idNiveau");
                /*if (attribut.CompareTo("idNiveau") == 0)
                {
                    data["idNiveau"] = Int32.Parse(attribut);
                }*/
            }
            return data;

        }

        public static void showDictionary(Dictionary<string, Dictionary<string, string>> dico)
        {
            int i = 1;
            foreach (var key in dico.Keys)
            {
                Debug.Log("Id" + i + " : " + key);
                foreach (var key1 in dico[key].Keys)
                {
                    Debug.Log(key1 + " : " + dico[key][key1]);
                }
                i += 1;
            }
        }

        public static void desactiverObjet(GameObject gameObject)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }
        public static void activerObjet(GameObject gameObject)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(true);
            }
        }
        public static void fermerApplication()
        {
            Application.Quit();
        }

        public static void debutChargement()
        {
            GameObject.Find("sceneController").GetComponent<SceneController>().chargement.SetActive(true);
        }
        public static void finChargement()
        {
            GameObject.Find("sceneController").GetComponent<SceneController>().chargement.SetActive(false);
        }

        public static void afficherMsgScene(string msg, string typeMsg, float delai = 3)
        {
            //FF2828
            SceneController sceneController = GameObject.Find("sceneController").GetComponent<SceneController>();
            Message message = sceneController.msgScene.GetComponent<Message>();
            sceneController.msgScene.SetActive(false);
            message.TempsAffichage = delai;
            message.attribuerMsg(msg);
            if (typeMsg.CompareTo("erreur") == 0)
            {
                message.imageError();
            }
            else if (typeMsg.CompareTo("succes") == 0)
            {
                message.imageSucces();
            }
            else if (typeMsg.CompareTo("primaire") == 0)
            {
                message.imagePrimaire();
            }
            sceneController.msgScene.SetActive(true);

        }
        public static void changerTexte(Text texteDepart, string texteArrivee = "")
        {
            texteDepart.text = "";
            texteDepart.text = texteArrivee;
        }
        public static void changerTexte(TMPro.TMP_Text texteDepart, string texteArrivee = "")
        {
            texteDepart.text = "";
            texteDepart.text = texteArrivee;
        }

        public static void changerSprite(Image image, Sprite sprite)
        {
            if (image.gameObject == null)
                return;
            image.sprite = sprite;
        }

        public static bool sceneActuelle(string nomScene)
        {
            return SceneManager.GetActiveScene().name.CompareTo(nomScene) == 0;
        }

        public static void activerAudioSourceDebutMatch()
        {
            GameObject.Find("AudioSourceDebutMatch").GetComponent<AudioSource>().Play();
        }
        public static void activerAudioSourceVictoire()
        {
            GameObject.Find("AudioSourceVictoire").GetComponent<AudioSource>().Play();
        }
        public static void activerAudioSourceDefaite()
        {
            GameObject.Find("AudioSourceDefaite").GetComponent<AudioSource>().Play();
        }
    }
}
