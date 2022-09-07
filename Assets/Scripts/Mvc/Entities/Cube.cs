using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Entities
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
        public Material CouleurDepotJoueur1 { get => couleurDepotJoueur1; set => couleurDepotJoueur1 = value; }
        public Material CouleurDepotJoueur2 { get => couleurDepotJoueur2; set => couleurDepotJoueur2 = value; }

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
            if (other.gameObject.name.Contains("pion"))
            {
                if (other.gameObject.GetComponent<Pion>().CaseActuelle.Id != cases.Id)
                {
                    other.gameObject.transform.position = other.gameObject.GetComponent<Pion>().CaseActuelle.gameObject.transform.position;
                }
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