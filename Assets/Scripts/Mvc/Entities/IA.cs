using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Entities
{
    public class IA : MonoBehaviour
    {
        [Header("Table")]

        [SerializeField] private List<int> listeCases = new List<int>(16);

        [SerializeField] private int idCase;

        void Start()
        {
            for (int i = 0; i < 14; i++)
            {
                listeCases[i] = 5;
            }
            jouer();
        }

        void Update()
        {
            if (Input.GetKeyDown("q"))
            {
                parcourirLaTable(listeCases, idCase);
            }
        }

        public void jouer(Table table)
        {
            initialiseCases(table);
        }
        public void jouer()
        {
            List<int> copyListeCases = listeCases;
            Debug.Log(copyListeCases.Count);
            for (int i = 0; i < 16; i++)
            {
                Debug.Log("case " + (i + 1) + " : " + copyListeCases[i]);
            }
        }

        #region Table
        public List<int> parcourirLaTable(List<int> listeCases, int caseDepart)
        {
            EtatMatch etatDuMatch = EtatMatch.EnCours;
            int idCase = caseDepart;
            if (listeCases[idCase] == 0)
            {
                Debug.Log("Interdit 1");
                return jeuInterdit(caseDepart);
            }
            else if ((idCase == 6 || idCase == 13) && listeCases[idCase] < 2 && peutTransmettrePion(listeCases, idCase) && sommeDesCasesAdversaire(listeCases, idCase) == 0)
            {
                Debug.Log("Interdit 2");
                return jeuInterdit(caseDepart);
            }
            else if (((idCase < 6 && listeCases[idCase] - 6 + idCase <= 0) || (idCase > 6 && idCase < 13 && listeCases[idCase] - 13 + idCase <= 0)) && peutTransmettrePion(listeCases, idCase) && sommeDesCasesAdversaire(listeCases, idCase) == 0)
            {
                Debug.Log("Interdit 3");
                return jeuInterdit(caseDepart);
            }
            else if ((idCase == 6 || idCase == 13) && listeCases[idCase] == 1 && sommeDesCasesJoueur(listeCases, idCase) > 1)
            {
                Debug.Log("Interdit 4");
                return jeuInterdit(caseDepart);
            }
            else if (!peutTransmettrePion(listeCases, idCase) && sommeDesCasesAdversaire(listeCases, idCase) == 0)
            {
                ResultatMatch resultatDuMatch = ResultatMatch.MatchNul;
                if (idCase < 7)
                    resultatDuMatch = ResultatMatch.V1;
                else if (idCase >= 7 && idCase < 14)
                    resultatDuMatch = ResultatMatch.V2;

                return new List<int> { caseDepart, caseDepart, 0, (int)EtatMatch.Fin, (int)resultatDuMatch };
            }
            else
            {
                return deplacerLesPionsCase(listeCases, idCase);
            }
        }
        public List<int> mangerLesPions(int idCaseDepart, int idCaseArrivee)
        {
            int nombrePion = 0;
            if (listeCases[idCaseArrivee] <= 1 || listeCases[idCaseArrivee] > 4 || idCaseArrivee == 14 || idCaseArrivee == 15)
            {
                nombrePion = 0;
            }

            //c'est la case qui transmet l'id de la derniere case et son id.
            if ((idCaseDepart < 7 && idCaseArrivee >= 7) || (idCaseDepart >= 7 && idCaseArrivee < 7))
            {
                if (idCaseArrivee == 0 || idCaseArrivee == 7)
                {
                    nombrePion = 0;
                }
                else if ((idCaseArrivee == 6 || idCaseArrivee == 13) && peutMangerToutesCasesAdversaire(listeCases, idCaseDepart))
                {
                    nombrePion = 0;
                }
                else
                {
                    int idCaseActuelle = idCaseArrivee;
                    while ((idCaseActuelle != -1 && idCaseDepart >= 7) || (idCaseActuelle != 6 && idCaseDepart < 7))
                    {
                        if (listeCases[idCaseActuelle] <= 1 || listeCases[idCaseActuelle] > 4)
                        {
                            break;
                        }
                        //listeCases[idCaseActuelle].deplacerLesPionsGrandeCase();
                        nombrePion += listeCases[idCaseActuelle];
                        listeCases[idCaseActuelle] = 0;
                        idCaseActuelle -= 1;
                    }
                }
            }
            return new List<int> { idCaseDepart, idCaseArrivee, nombrePion, (int)EtatMatch.EnCours, (int)ResultatMatch.MatchNul };
            ;
        }
        private int sommeDesCasesAdversaire(List<int> listeCases, int idCase)
        {
            int somme = 0;
            if (idCase < 7)
            {
                for (int i = 7; i < 14; i++)
                {
                    somme += listeCases[i];
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    somme += listeCases[i];
                }
            }
            return somme;
        }
        private int sommeDesCasesJoueur(List<int> listeCases, int idCase)
        {
            int somme = 0;
            if (idCase >= 7)
            {
                for (int i = 7; i < 14; i++)
                {
                    somme += listeCases[i];
                }

            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    somme += listeCases[i];
                }

            }
            return somme;
        }
        private bool peutTransmettrePion(List<int> listeCases, int idCase)
        {
            int nbPion = 1;
            if (idCase < 7)
            {
                if (listeCases[6] > nbPion)
                    return true;
                for (int i = 5; i >= 0; i--)
                {
                    if (listeCases[i] > nbPion)
                    {
                        return true;
                    }
                    nbPion += 1;
                }

            }
            else
            {
                if (listeCases[13] > nbPion)
                    return true;
                for (int i = 12; i >= 7; i--)
                {
                    if (listeCases[i] > nbPion)
                    {
                        return true;
                    }
                    nbPion += 1;
                }

            }
            return false;
        }

        private bool peutMangerToutesCasesAdversaire(List<int> listeCases, int idCase)
        {
            if (idCase < 7)
            {
                for (int i = 13; i >= 7; i--)
                {
                    if (listeCases[i] <= 1 || listeCases[i] > 4)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 6; i >= 0; i--)
                {
                    if (listeCases[i] <= 1 || listeCases[i] > 4)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public List<int> jeuInterdit(int idCaseDepart)
        {
            return new List<int>();
            //match.rejouerCoup(caseDepart);
        }

        public void initialiseCases(Table table)
        {
            foreach (var cases in table.ListeCases)
            {
                listeCases.Add(cases.ListePions.Count);
            }
        }
        #endregion Table

        #region Match

        #endregion Match

        #region Case
        public List<int> deplacerLesPionsCase(List<int> listeCases, int id)
        {
            int idCaseSuivante = id + 1;
            if (listeCases[id] == 1)
            {
                if (id == 6 || id == 13)
                {
                    idCaseSuivante = id == 6 ? 14 : 15;
                }
                listeCases[idCaseSuivante] += 1;
            }
            else
            {

                bool tourComplet = false;
                int nbPionsDeplace = 0;
                int idCaseTourComplet = id;
                for (int i = 0; i < listeCases[id]; i++)
                {
                    idCaseSuivante = idCaseSuivante == 14 ? 0 : idCaseSuivante;
                    tourComplet = idCaseSuivante == idCaseTourComplet ? true : false;
                    if (tourComplet)
                    {
                        if (id < 7)
                        {
                            idCaseTourComplet = 0;
                            if (listeCases[id] - nbPionsDeplace == 1)
                            {
                                listeCases[14] += 1;
                                idCaseSuivante = 14;
                                break;
                            }
                            idCaseSuivante = 7;

                        }
                        else if (id >= 7)
                        {
                            idCaseTourComplet = 7;
                            if (listeCases[id] - nbPionsDeplace == 1)
                            {
                                listeCases[15] += 1;
                                idCaseSuivante = 15;
                                break;
                            }
                            idCaseSuivante = 0;
                        }
                    }
                    listeCases[idCaseSuivante] += 1;
                    nbPionsDeplace += 1;
                    idCaseSuivante += 1;
                }
            }
            listeCases[id] = 0;
            return mangerLesPions(id, idCaseSuivante - 1);
        }

        #endregion Case

        #region IA

        public int analyse(List<int> listeCases)
        {
            for (int idCase = 0; idCase < 7; idCase++)
            {
                if (listeCases[idCase] - 6 + idCase > 0)
                {

                }
            }
            return 0;
        }
        public void menaceSimple()
        {

        }
        public int menaceDouble()
        {
            return 0;
        }
        public int eviterMenace()
        {
            return 0;
        }
        public int bloquerMenace()
        {
            return 0;
        }
        public int contrerMenace()
        {
            return 0;
        }
        public int limiterMenace()
        {
            return 0;
        }
        #endregion IA
    }
}
