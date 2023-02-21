using Mvc.Core;
using Mvc.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Controllers
{
    public class PionController : MonoBehaviour
    {
        [SerializeField] private List<Material> listeCouleurs = new List<Material>();
        [SerializeField] private GameObject pionPrincipal;
        [SerializeField] private GameObject pion1;
        [SerializeField] private GameObject pion2;
        [SerializeField] private GameObject pion3;


        private void Awake()
        {
            attribuerCouleur(listeCouleurs[PlayerPrefs.GetInt("couleurPion")]);
        }

        public void choisirCouleur(int numeroCouleur)
        {
            if (numeroCouleur == 0)
            {
                PlayerPrefs.SetInt("couleurPion", 0);
            }
            else if (numeroCouleur == 1)
            {
                PlayerPrefs.SetInt("couleurPion", 1);
            }
            else if (numeroCouleur == 2)
            {
                PlayerPrefs.SetInt("couleurPion", 2);
            }
            else if (numeroCouleur == 3)
            {
                PlayerPrefs.SetInt("couleurPion", 3);
            }
            else if (numeroCouleur == 4)
            {
                PlayerPrefs.SetInt("couleurPion", 4);
            }
            else if (numeroCouleur == 5)
            {
                PlayerPrefs.SetInt("couleurPion", 5);
            }
            if (numeroCouleur == 6)
            {
                PlayerPrefs.SetInt("couleurPion", 6);
            }
            else if (numeroCouleur == 7)
            {
                PlayerPrefs.SetInt("couleurPion", 7);
            }
            else if (numeroCouleur == 8)
            {
                PlayerPrefs.SetInt("couleurPion", 8);
            }
            else if (numeroCouleur == 9)
            {
                PlayerPrefs.SetInt("couleurPion", 9);
            }
            attribuerCouleur(listeCouleurs[numeroCouleur]);
        }
        private void attribuerCouleur(Material material)
        {
            pionPrincipal.gameObject.GetComponent<Renderer>().material = material;
            pion1.gameObject.GetComponent<Renderer>().material = material;
            pion2.gameObject.GetComponent<Renderer>().material = material;
            pion3.gameObject.GetComponent<Renderer>().material = material;
        }
        public void boutonRetour()
        {
            Fonctions.changerDeScene("SceneSetting");
        }
    }
}
