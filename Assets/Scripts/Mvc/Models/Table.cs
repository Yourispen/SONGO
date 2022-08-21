using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public class Table : ModelFirebase
    {
        [SerializeField] private string libelle;
        [SerializeField] private Material couleur;
        [SerializeField] private StatutTable statut;
        [SerializeField] private List<Case> listeCases = new List<Case>();
        [SerializeField] private Caisse caisse;


        public string Libelle { get => libelle; set => libelle = value; }
        public Material Couleur { get => couleur; set => couleur = value; }
        public List<Case> ListeCases { get => listeCases; }
        public StatutTable Statut { get => statut; set => statut = value; }
        public Caisse Caisse { get => caisse; set => caisse = value; }

        public void ajouterCase(Case case_)
        {
            listeCases.Add(case_);
        }

        // Start is called before the first frame update
        void Start()
        {
            //this.GetComponent<Renderer>().material = couleur;
        }

        public void parcourirLaTable(int idCase)
        {
            if (listeCases[idCase].ListePions.Count == 0)
            {

            }
            else if ((idCase == 6 || idCase == 13) && listeCases[idCase].ListePions.Count < 2 && peutTransmettrePion(idCase) && sommeDesCasesAdversaire(idCase) == 0)
            {

            }
            else if (((idCase < 6 && listeCases[idCase].ListePions.Count - 6 + idCase > 0) || (idCase < 13 && listeCases[idCase].ListePions.Count - 13 + idCase > 0)) && peutTransmettrePion(idCase) && sommeDesCasesAdversaire(idCase) == 0)
            {

            }
            else if ((idCase == 6 || idCase == 13) && listeCases[idCase].ListePions.Count == 1 && sommeDesCasesJoueur(idCase) > 1)
            {

            }
            else
            {
                listeCases[idCase].deplacerLesPionsCase();
            }
        }
        public IEnumerator mangerLesPions(int idCaseActuelle, int idDerniereCase)
        {
            yield break;
            //c'est la case qui transmet l'id de la derniere case et son id.
        }
        private int sommeDesCasesAdversaire(int idCase)
        {
            int somme = 0;
            if (idCase < 7)
            {
                somme = listeCases[7].ListePions.Count + listeCases[8].ListePions.Count +
                           listeCases[9].ListePions.Count + listeCases[10].ListePions.Count +
                           listeCases[11].ListePions.Count + listeCases[12].ListePions.Count +
                           listeCases[13].ListePions.Count;
            }
            else
            {
                somme = listeCases[0].ListePions.Count + listeCases[1].ListePions.Count +
                            listeCases[2].ListePions.Count + listeCases[3].ListePions.Count +
                            listeCases[4].ListePions.Count + listeCases[5].ListePions.Count +
                            listeCases[6].ListePions.Count;
            }
            return somme;
        }
        private int sommeDesCasesJoueur(int idCase)
        {
            int somme = 0;
            if (idCase >= 7)
            {
                somme = listeCases[7].ListePions.Count + listeCases[8].ListePions.Count +
                           listeCases[9].ListePions.Count + listeCases[10].ListePions.Count +
                           listeCases[11].ListePions.Count + listeCases[12].ListePions.Count +
                           listeCases[13].ListePions.Count;
            }
            else
            {
                somme = listeCases[0].ListePions.Count + listeCases[1].ListePions.Count +
                            listeCases[2].ListePions.Count + listeCases[3].ListePions.Count +
                            listeCases[4].ListePions.Count + listeCases[5].ListePions.Count +
                            listeCases[6].ListePions.Count;
            }
            return somme;
        }
        private bool peutTransmettrePion(int idCase)
        {
            bool resultat;
            if (idCase < 7)
            {
                resultat = (listeCases[6].ListePions.Count > 1 || listeCases[5].ListePions.Count > 1 ||
                                   listeCases[4].ListePions.Count > 2 || listeCases[3].ListePions.Count > 3 ||
                                   listeCases[2].ListePions.Count > 4 || listeCases[1].ListePions.Count > 5 ||
                                   listeCases[0].ListePions.Count > 6);
            }
            else
            {
                resultat = (listeCases[13].ListePions.Count > 1 || listeCases[12].ListePions.Count > 1 ||
                                listeCases[11].ListePions.Count > 2 || listeCases[10].ListePions.Count > 3 ||
                                listeCases[9].ListePions.Count > 4 || listeCases[8].ListePions.Count > 5 ||
                                listeCases[7].ListePions.Count > 6);

            }
            return resultat;
        }

    }
}
