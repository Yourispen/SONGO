using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Core
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]private float tempsAttente;
        [SerializeField] private float tempsDepart;
        [SerializeField] private float tempsActuel;
        [SerializeField] private int compteur;
        [SerializeField] private bool tempsFini;

        public int Compteur { get => compteur; set => compteur = value; }
        public float TempsAttente { get => tempsAttente; set => tempsAttente = value; }
        public bool TempsFini { get => tempsFini; set => tempsFini = value; }

        IEnumerator tempsEcoule()
        {
            while (compteur < tempsAttente)
            {
                Debug.Log(compteur++);
                yield return new WaitForSeconds(1f);
            }
            
        }
    }
}
