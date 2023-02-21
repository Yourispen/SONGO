using Mvc.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Controllers
{
    public class TableController : MonoBehaviour
    {
        [SerializeField] private List<Material> listeCouleurs = new List<Material>();
        [SerializeField] private GameObject table;

        void Awake()
        {
            table.gameObject.GetComponent<Renderer>().material = listeCouleurs[PlayerPrefs.GetInt("couleurTable")];
        }
        
        public void choisirCouleur(int numeroCouleur)
        {
            if (numeroCouleur == 0)
            {
                PlayerPrefs.SetInt("couleurTable", 0);
            }
            else if (numeroCouleur == 1)
            {
                PlayerPrefs.SetInt("couleurTable", 1);
            }
            else if (numeroCouleur == 2)
            {
                PlayerPrefs.SetInt("couleurTable", 2);
            }
            else if (numeroCouleur == 3)
            {
                PlayerPrefs.SetInt("couleurTable", 3);
            }
            else if (numeroCouleur == 4)
            {
                PlayerPrefs.SetInt("couleurTable", 4);
            }
            else if (numeroCouleur == 5)
            {
                PlayerPrefs.SetInt("couleurTable", 5);
            }
            table.gameObject.GetComponent<Renderer>().material = listeCouleurs[numeroCouleur];
        }

        public void boutonRetour()
        {
            Fonctions.changerDeScene("SceneSetting");
        }

    }
}