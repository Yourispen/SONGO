using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mvc.Core;
using TMPro;

namespace Mvc.Entities
{
    public class OutilsJoueur : MonoBehaviour
    {
        [SerializeField] private GameObject outilsJoueur1;
        [SerializeField] private Image plaqueNom1;
        [SerializeField] private TMPro.TMP_Text textPlaqueNom1;
        [SerializeField] private GameObject plaqueCompteur1;
        [SerializeField] private Image backgroundCompteurPion1;
        [SerializeField] private TMPro.TMP_Text textPlaqueCompteur1;
        [SerializeField] private GameObject plaqueChat1;
        [SerializeField] private Image backgroundMessage1;
        [SerializeField] private TMPro.TMP_Text textMessage1;
        [SerializeField] private Image backgroundEmoji1;
        [SerializeField] private Image imageEmoji1;
        [SerializeField] private GameObject outilsJoueur2;
        [SerializeField] private Image plaqueNom2;
        [SerializeField] private TMPro.TMP_Text textPlaqueNom2;
        [SerializeField] private GameObject plaqueCompteur2;
        [SerializeField] private Image backgroundCompteurPion2;
        [SerializeField] private TMPro.TMP_Text textPlaqueCompteur2;
        [SerializeField] private GameObject plaqueChat2;
        [SerializeField] private Image backgroundMessage2;
        [SerializeField] private TMPro.TMP_Text textMessage2;
        [SerializeField] private Image backgroundEmoji2;
        [SerializeField] private Image imageEmoji2;
        [SerializeField] private Match match;

        public GameObject OutilsJoueur1 { get => outilsJoueur1; set => outilsJoueur1 = value; }
        public Image PlaqueNom1 { get => plaqueNom1; set => plaqueNom1 = value; }
        public TMP_Text TextPlaqueNom1 { get => textPlaqueNom1; set => textPlaqueNom1 = value; }
        public GameObject PlaqueCompteur1 { get => plaqueCompteur1; set => plaqueCompteur1 = value; }
        public Image BackgroundCompteurPion1 { get => backgroundCompteurPion1; set => backgroundCompteurPion1 = value; }
        public TMP_Text TextPlaqueCompteur1 { get => textPlaqueCompteur1; set => textPlaqueCompteur1 = value; }
        public GameObject OutilsJoueur2 { get => outilsJoueur2; set => outilsJoueur2 = value; }
        public Image PlaqueNom2 { get => plaqueNom2; set => plaqueNom2 = value; }
        public TMP_Text TextPlaqueNom2 { get => textPlaqueNom2; set => textPlaqueNom2 = value; }
        public GameObject PlaqueCompteur2 { get => plaqueCompteur2; set => plaqueCompteur2 = value; }
        public Image BackgroundCompteurPion2 { get => backgroundCompteurPion2; set => backgroundCompteurPion2 = value; }
        public TMP_Text TextPlaqueCompteur2 { get => textPlaqueCompteur2; set => textPlaqueCompteur2 = value; }
        public Match Match { get => match; set => match = value; }
        public Image BackgroundMessage1 { get => backgroundMessage1; set => backgroundMessage1 = value; }
        public Image BackgroundEmoji1 { get => backgroundEmoji1; set => backgroundEmoji1 = value; }
        public Image BackgroundMessage2 { get => backgroundMessage2; set => backgroundMessage2 = value; }
        public TMP_Text TextMessage2 { get => textMessage2; set => textMessage2 = value; }
        public GameObject PlaqueChat2 { get => plaqueChat2; set => plaqueChat2 = value; }
        public GameObject PlaqueChat1 { get => plaqueChat1; set => plaqueChat1 = value; }

        public void activerCompteurJoueur(int joueur)
        {
            if (Fonctions.sceneActuelle("SceneMatch1vs1") || Fonctions.sceneActuelle("SceneMatchEntrainement"))
            {
                if (joueur == 1)
                {
                    Fonctions.activerObjet(plaqueCompteur1);
                    Fonctions.desactiverObjet(plaqueCompteur2);
                }
                else if (joueur == 2)
                {
                    Fonctions.activerObjet(plaqueCompteur2);
                    Fonctions.desactiverObjet(plaqueCompteur1);
                }
            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                {
                    Fonctions.activerObjet(plaqueCompteur1);
                }
                else
                {
                    Fonctions.activerObjet(plaqueCompteur2);
                }
            }
        }
        public void desactiverCompteursJoueurs()
        {
            Fonctions.desactiverObjet(plaqueCompteur2);
            Fonctions.desactiverObjet(plaqueCompteur1);
        }
        public void afficherNomsJoueurs(string nom1, string nom2)
        {
            Fonctions.changerTexte(textPlaqueNom1, nom1);
            Fonctions.changerTexte(textPlaqueNom2, nom2);
        }
        public IEnumerator afficherMessage(string msg, int joueur, bool affichePermanent = false)
        {
            if (joueur == 1)
            {
                if (plaqueChat1.activeSelf)
                    yield break;
                Fonctions.desactiverObjet(backgroundEmoji1.gameObject);
                Fonctions.activerObjet(plaqueChat1);
                Fonctions.activerObjet(backgroundMessage1.gameObject);
                backgroundMessage1.rectTransform.sizeDelta = new Vector2(100 + msg.Length * 15, backgroundMessage1.rectTransform.sizeDelta.y);
                Fonctions.changerTexte(textMessage1, msg);
                yield return new WaitForSeconds(3);
                if (!affichePermanent)
                {
                    Fonctions.changerTexte(textMessage1);
                    Fonctions.desactiverObjet(backgroundMessage1.gameObject);
                    Fonctions.desactiverObjet(plaqueChat1);
                }
            }
            else
            {
                if (plaqueChat2.activeSelf)
                    yield break;
                Fonctions.desactiverObjet(backgroundEmoji2.gameObject);
                Fonctions.activerObjet(plaqueChat2);
                Fonctions.activerObjet(backgroundMessage2.gameObject);
                backgroundMessage2.rectTransform.sizeDelta = new Vector2(100 + msg.Length * 15, backgroundMessage2.rectTransform.sizeDelta.y);
                Fonctions.changerTexte(textMessage2, msg);
                yield return new WaitForSeconds(3);
                if (!affichePermanent)
                {
                    Fonctions.changerTexte(textMessage2);
                    Fonctions.desactiverObjet(backgroundMessage2.gameObject);
                    Fonctions.desactiverObjet(plaqueChat2);
                }
            }

        }
        public IEnumerator afficherEmoji(Image emoji, int joueur)
        {
            if (joueur == 1)
            {
                if (plaqueChat1.activeSelf)
                    yield break;
                Fonctions.desactiverObjet(backgroundMessage1.gameObject);
                Fonctions.activerObjet(plaqueChat1);
                Fonctions.changerSprite(imageEmoji1, emoji.sprite);
                Fonctions.activerObjet(backgroundEmoji1.gameObject);
                yield return new WaitForSeconds(3);
                Fonctions.desactiverObjet(backgroundEmoji1.gameObject);
                Fonctions.desactiverObjet(plaqueChat1);
            }
            else
            {
                if (plaqueChat2.activeSelf)
                    yield break;
                Fonctions.desactiverObjet(backgroundMessage2.gameObject);
                Fonctions.activerObjet(plaqueChat2);
                Fonctions.changerSprite(imageEmoji2, emoji.sprite);
                Fonctions.activerObjet(backgroundEmoji2.gameObject);
                yield return new WaitForSeconds(3);
                Fonctions.desactiverObjet(backgroundEmoji2.gameObject);
                Fonctions.desactiverObjet(plaqueChat2);
            }
        }

        public void initialiseOutilsCote2()
        {
            Vector3 positionTemp = plaqueNom1.rectTransform.position;
            plaqueNom1.rectTransform.position = plaqueNom2.rectTransform.position + new Vector3(0, -3, 0);
            plaqueNom2.rectTransform.position = positionTemp;

            positionTemp = plaqueChat1.transform.position;
            Quaternion rotationTemp = plaqueChat1.transform.rotation;
            plaqueChat1.transform.position = plaqueChat2.transform.position;
            plaqueChat1.transform.rotation = plaqueChat2.transform.rotation;
            plaqueChat2.transform.position = positionTemp;
            plaqueChat2.transform.rotation = rotationTemp;

            positionTemp = textMessage1.transform.localPosition;
            rotationTemp = textMessage1.transform.localRotation;
            textMessage1.transform.localPosition = textMessage2.transform.localPosition;
            textMessage1.transform.localRotation = textMessage2.transform.localRotation;
            textMessage2.transform.localPosition = positionTemp;
            textMessage2.transform.localRotation = rotationTemp;

            positionTemp = imageEmoji1.transform.localPosition;
            rotationTemp = imageEmoji1.transform.localRotation;
            imageEmoji1.transform.localPosition = imageEmoji2.transform.localPosition;
            imageEmoji1.transform.localRotation = imageEmoji2.transform.localRotation;
            imageEmoji2.transform.localPosition = positionTemp;
            imageEmoji2.transform.localRotation = rotationTemp;

        }
    }
}
