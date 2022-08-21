using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public class Table : ModelFirebase
    {
        [SerializeField] private string libelle;
        [SerializeField] private List<Material> listeCouleurs = new List<Material>();
        [SerializeField] private Material couleur;
        [SerializeField] private StatutTable statut;
        [SerializeField] private List<Case> listeCases = new List<Case>();
        [SerializeField] private GameObject caissePrefab;
        [SerializeField] private Match match;
        [SerializeField] private Caisse caisse;
        public static int idCaseJoue;


        public string Libelle { get => libelle; set => libelle = value; }
        public Material Couleur { get => couleur; set => couleur = value; }
        public List<Case> ListeCases { get => listeCases; }
        public StatutTable Statut { get => statut; set => statut = value; }

        public Caisse Caisse { get => caisse; set => caisse = value; }
        public Match Match { get => match; set => match = value; }

        public void ajouterCase(Case case_)
        {
            listeCases.Add(case_);
        }

        void Awake()
        {
            idCaseJoue=-1;
            this.gameObject.GetComponent<Renderer>().material = listeCouleurs[PlayerPrefs.GetInt("couleurTable")];
            couleur = this.gameObject.GetComponent<Renderer>().material;
        }
        void OnEnable()
        {
            caisse = Fonctions.instancierObjet(caissePrefab, caissePrefab.transform.position).GetComponent<Caisse>();
            caisse.gameObject.transform.rotation = new Quaternion(90, 0, 0, 90);
            caisse.Table = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            //this.GetComponent<Renderer>().material = couleur;
            StartCoroutine(match.instancierLesPions());
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
            else if (!peutTransmettrePion(idCase) && sommeDesCasesAdversaire(idCase) == 0)
            {

            }
            else
            {
                idCaseJoue = idCase;
                listeCases[idCase].deplacerLesPionsCase();
            }
        }
        public IEnumerator mangerLesPions(int idCaseDepart, int idCaseArrivee)
        {
            //c'est la case qui transmet l'id de la derniere case et son id.
            if ((idCaseDepart < 7 && idCaseArrivee >= 7) || (idCaseDepart >= 7 && idCaseArrivee < 7))
            {
                if (idCaseArrivee == 0 || idCaseArrivee == 7)
                {

                }
                else if ((idCaseArrivee == 6 || idCaseArrivee == 13) && peutMangerToutesCasesAdversaire(idCaseDepart))
                {

                }
                else
                {
                    int idCaseActuelle = idCaseArrivee;
                    while (idCaseActuelle != 0 && idCaseActuelle != 6)
                    {
                        if (1 < listeCases[idCaseActuelle].ListePions.Count && listeCases[idCaseActuelle].ListePions.Count < 5)
                        {
                            break;
                        }
                        listeCases[idCaseActuelle].deplacerLesPionsGrandeCase();
                        yield return new WaitForSeconds(Case.tempsDepotCase);
                    }
                    yield return new WaitForSeconds(Case.tempsAttente);
                }
                listeCases[idCaseDepart].gameObject.GetComponent<Clignote>().enabled = false;
            }
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

        private bool peutMangerToutesCasesAdversaire(int idCase)
        {
            bool resultat;
            if (idCase < 7)
            {
                resultat = ((1 < listeCases[13].ListePions.Count && listeCases[13].ListePions.Count < 5) &&
                            (1 < listeCases[12].ListePions.Count && listeCases[12].ListePions.Count < 5) &&
                            (1 < listeCases[11].ListePions.Count && listeCases[11].ListePions.Count < 5) &&
                            (1 < listeCases[10].ListePions.Count && listeCases[10].ListePions.Count < 5) &&
                            (1 < listeCases[9].ListePions.Count && listeCases[9].ListePions.Count < 5) &&
                            (1 < listeCases[8].ListePions.Count && listeCases[8].ListePions.Count < 5) &&
                            (1 < listeCases[7].ListePions.Count && listeCases[7].ListePions.Count < 5));
            }
            else
            {
                resultat = ((1 < listeCases[6].ListePions.Count && listeCases[6].ListePions.Count < 5) &&
                            (1 < listeCases[5].ListePions.Count && listeCases[5].ListePions.Count < 5) &&
                            (1 < listeCases[4].ListePions.Count && listeCases[4].ListePions.Count < 5) &&
                            (1 < listeCases[3].ListePions.Count && listeCases[3].ListePions.Count < 5) &&
                            (1 < listeCases[2].ListePions.Count && listeCases[2].ListePions.Count < 5) &&
                            (1 < listeCases[1].ListePions.Count && listeCases[1].ListePions.Count < 5) &&
                            (1 < listeCases[0].ListePions.Count && listeCases[0].ListePions.Count < 5));
            }
            return resultat;
        }

    }
}
