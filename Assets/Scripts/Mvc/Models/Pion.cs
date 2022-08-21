using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public class Pion : ModelFirebase
    {
        [SerializeField] private Material couleur;
        public static int nombreDePions = 0;
        [SerializeField] private int id = 0;
        [SerializeField] private Case caseActuelle;

        public int Id { get => id; set => id = value; }
        public Material Couleur { get => couleur; set => couleur = value; }
        public Case CaseActuelle { get => caseActuelle; set => caseActuelle = value; }

        void Start()
        {
            //this.GetComponent<Renderer>().material = couleur;
            nombreDePions++;
            id = nombreDePions;
            Debug.Log("Je suis un pion");
        }
        public void deplacerPionCase(int idCase)
        {
            //this.gameObject.transform.position=
        }

        public void deplacerPionCase(Case cases, Vector3 ajoutPosition)
        {
            this.transform.position = cases.transform.position+ajoutPosition;
        }
        public void deplacerPionCaisse(Caisse caisse)
        {
            this.transform.position = caisse.transform.position + new Vector3(Random.Range(-1.9f, 1.9f), -0.5f, Random.Range(-1.9f, 1.9f));
        }

    }
}
