using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private string libelle;
        [SerializeField] private Case cases;
        [SerializeField] private Material couleurDepotJoueur1;
        [SerializeField] private Material couleurDepotJoueur2;

        public int Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public Case Cases { get => cases; set => cases = value; }

        void Start()
        {
            couleurDepotJoueur1 = this.cases.Table.ListeCases[0].CouleurMouvement;
            couleurDepotJoueur2 = this.cases.Table.ListeCases[7].CouleurMouvement;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Table.idCaseJoue < 7 && Table.idCaseJoue > -1)
            {
                StartCoroutine(changeCouleurCase(couleurDepotJoueur1));
            }
            else if (Table.idCaseJoue >= 7)
            {
                StartCoroutine(changeCouleurCase(couleurDepotJoueur2));
            }
        }
        public IEnumerator changeCouleurCase(Material couleur)
        {
            this.cases.gameObject.GetComponent<Renderer>().material = couleur;
            yield return new WaitForSeconds(0.2f);
            this.cases.gameObject.GetComponent<Renderer>().material = this.cases.CouleurInitiale;
        }
    }
}