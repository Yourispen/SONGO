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
        [SerializeField] private GameObject outilsJoueur2;
        [SerializeField] private Image plaqueNom2;
        [SerializeField] private TMPro.TMP_Text textPlaqueNom2;
        [SerializeField] private GameObject plaqueCompteur2;
        [SerializeField] private Image backgroundCompteurPion2;
        [SerializeField] private TMPro.TMP_Text textPlaqueCompteur2;
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
    }
}
