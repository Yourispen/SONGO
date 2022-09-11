using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Entities
{
    public class IA : MonoBehaviour
    {
        [Header("Table")]
        [SerializeField] private List<int> listeCases = new List<int>();

        [Header("Match")]
        [SerializeField] protected ResultatMatch resultatDuMatch;
        [SerializeField] protected EtatMatch etatDuMatch;

        public void jouer(Table table)
        {
            initialiseCases(table);
        }

        #region Table
        public int parcourirLaTable(int caseDepart)
        {
            int idCase = caseDepart;
            if (listeCases[idCase] == 0)
            {
                Debug.Log("Interdit 1");
                return jeuInterdit(caseDepart);
            }
            else if ((idCase == 6 || idCase == 13) && listeCases[idCase] < 2 && peutTransmettrePion(idCase) && sommeDesCasesAdversaire(idCase) == 0)
            {
                Debug.Log("Interdit 2");
                return jeuInterdit(caseDepart);
            }
            else if (((idCase < 6 && listeCases[idCase] - 6 + idCase <= 0) || (idCase > 6 && idCase < 13 && listeCases[idCase] - 13 + idCase <= 0)) && peutTransmettrePion(idCase) && sommeDesCasesAdversaire(idCase) == 0)
            {
                Debug.Log("Interdit 3");
                return jeuInterdit(caseDepart);
            }
            else if ((idCase == 6 || idCase == 13) && listeCases[idCase] == 1 && sommeDesCasesJoueur(idCase) > 1)
            {
                Debug.Log("Interdit 4");
                return jeuInterdit(caseDepart);
            }
            else if (!peutTransmettrePion(idCase) && sommeDesCasesAdversaire(idCase) == 0)
            {
                if (idCase < 7)
                    resultatDuMatch = ResultatMatch.V1;
                else if (idCase >= 7 && idCase < 14)
                    resultatDuMatch = ResultatMatch.V2;
                
                return 0;
            }
            else
            {
                return deplacerLesPionsCase(idCase);
            }
        }
        public int mangerLesPions(int idCaseDepart, int idCaseArrivee)
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
                else if ((idCaseArrivee == 6 || idCaseArrivee == 13) && peutMangerToutesCasesAdversaire(idCaseDepart))
                {
                    return nombrePion = 0;
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
            return nombrePion;
        }
        private int sommeDesCasesAdversaire(int idCase)
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
        private int sommeDesCasesJoueur(int idCase)
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
        private bool peutTransmettrePion(int idCase)
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

        private bool peutMangerToutesCasesAdversaire(int idCase)
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
        public int jeuInterdit(int idCaseDepart)
        {
            return -1;
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
        public int deplacerLesPionsCase(int id)
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

        #region Pion

        #endregion Pion
    }
}
