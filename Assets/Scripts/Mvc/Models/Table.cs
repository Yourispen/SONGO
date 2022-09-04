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
            StartCoroutine(match.instancierLesPions());
        }

        public void parcourirLaTable(Case caseDepart)
        {
            int idCase = caseDepart.Id;

            if (listeCases[idCase].ListePions.Count == 0)
            {
                Debug.Log("Interdit 1");
                jeuInterdit(caseDepart);
            }
            else if ((idCase == 6 || idCase == 13) && listeCases[idCase].ListePions.Count < 2 && peutTransmettrePion(idCase) && sommeDesCasesAdversaire(idCase) == 0)
            {
                Debug.Log("Interdit 2");
                jeuInterdit(caseDepart);
            }
            else if (((idCase < 6 && listeCases[idCase].ListePions.Count - 6 + idCase <= 0) || (idCase > 6 && idCase < 13 && listeCases[idCase].ListePions.Count - 13 + idCase <= 0)) && peutTransmettrePion(idCase) && sommeDesCasesAdversaire(idCase) == 0)
            {
                Debug.Log("Interdit 3");
                jeuInterdit(caseDepart);
            }
            else if ((idCase == 6 || idCase == 13) && listeCases[idCase].ListePions.Count == 1 && sommeDesCasesJoueur(idCase) > 1)
            {
                Debug.Log("Interdit 4");
                jeuInterdit(caseDepart);
            }
            else if (!peutTransmettrePion(idCase) && sommeDesCasesAdversaire(idCase) == 0)
            {
                idCaseJoue = idCase;

                if (idCase < 7)
                    match.ResultatDuMatch = ResultatMatch.V1;
                else if (idCase >= 7 && idCase < 14)
                    match.ResultatDuMatch = ResultatMatch.V2;
                match.finDuMatch();
            }
            else
            {
                idCaseJoue = idCase;
                StartCoroutine(listeCases[idCase].deplacerLesPionsCase());
            }
        }
        public void jeuInterdit(Case caseDepart)
        {
            match.rejouerCoup(caseDepart);
        }
        public IEnumerator mangerLesPions(Case caseDepart, Case caseArrivee)
        {
            int idCaseDepart = caseDepart.Id;
            int idCaseArrivee = caseArrivee.Id;
            if (caseArrivee.ListePions.Count <= 1 || caseArrivee.ListePions.Count > 4 || idCaseArrivee == 14 || idCaseArrivee == 15)
            {
                caseDepart.gameObject.GetComponent<Clignote>().enabled = false;
                match.verifierEtatDuMatch(caseDepart);
                yield break;
            }

            //c'est la case qui transmet l'id de la derniere case et son id.
            if ((idCaseDepart < 7 && idCaseArrivee >= 7) || (idCaseDepart >= 7 && idCaseArrivee < 7))
            {
                if (idCaseArrivee == 0 || idCaseArrivee == 7)
                {
                    match.verifierEtatDuMatch(caseDepart);
                }
                else if ((idCaseArrivee == 6 || idCaseArrivee == 13) && peutMangerToutesCasesAdversaire(idCaseDepart))
                {
                    match.verifierEtatDuMatch(caseDepart);
                }
                else
                {
                    int idCaseActuelle = idCaseArrivee;
                    while ((idCaseActuelle != -1 && idCaseDepart >= 7) || (idCaseActuelle != 6 && idCaseDepart < 7))
                    {
                        if (listeCases[idCaseActuelle].ListePions.Count <= 1 || listeCases[idCaseActuelle].ListePions.Count > 4)
                        {
                            break;
                        }
                        listeCases[idCaseActuelle].deplacerLesPionsGrandeCase();
                        idCaseActuelle -= 1;
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield return new WaitForSeconds(Case.tempsAttente);
                }
                caseDepart.gameObject.GetComponent<Clignote>().enabled = false;

                if (sommeDesCasesAdversaire(idCaseDepart) == 0)
                {
                    match.finDuMatch();
                }
                else
                {
                    match.verifierEtatDuMatch(caseDepart);
                }
            }
            else
            {
                caseDepart.gameObject.GetComponent<Clignote>().enabled = false;
                match.verifierEtatDuMatch(caseDepart);
            }
        }
        private int sommeDesCasesAdversaire(int idCase)
        {
            int somme = 0;
            if (idCase < 7)
            {
                for (int i = 7; i < 14; i++)
                {
                    somme += listeCases[i].ListePions.Count;
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    somme += listeCases[i].ListePions.Count;
                }
            }
            return somme;
        }
        private int sommeDesCasesJoueur(int idCase)
        {
            int somme = 0;
            if (idCase >= 7)
            {
                for (int i = 7; i < 14; i++)
                {
                    somme += listeCases[i].ListePions.Count;
                }

            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    somme += listeCases[i].ListePions.Count;
                }

            }
            return somme;
        }
        private bool peutTransmettrePion(int idCase)
        {
            int nbPion = 1;
            if (idCase < 7)
            {
                if (listeCases[6].ListePions.Count > nbPion)
                    return true;
                for (int i = 5; i >= 0; i--)
                {
                    if (listeCases[i].ListePions.Count > nbPion)
                    {
                        return true;
                    }
                    nbPion += 1;
                }

            }
            else
            {
                if (listeCases[13].ListePions.Count > nbPion)
                    return true;
                for (int i = 12; i >= 7; i--)
                {
                    if (listeCases[i].ListePions.Count > nbPion)
                    {
                        return true;
                    }
                    nbPion += 1;
                }

            }
            return false;
        }

        private bool peutMangerToutesCasesAdversaire(int idCase)
        {
            if (idCase < 7)
            {
                for (int i = 13; i >= 7; i--)
                {
                    if (listeCases[i].ListePions.Count <= 1 || listeCases[i].ListePions.Count > 4)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 6; i >= 0; i--)
                {
                    if (listeCases[i].ListePions.Count <= 1 || listeCases[i].ListePions.Count > 4)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void reinitialiseCases()
        {
            foreach (var cases in listeCases)
            {
                cases.ListePions.Clear();
            }
        }

    }
}
