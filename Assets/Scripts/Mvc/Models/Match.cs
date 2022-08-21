using Mvc.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public abstract class Match : ModelFirebase
    {
        public const int NOMBREMAXJOUEUR = 2;
        [SerializeField] protected ResultatMatch resultatDuMatch;
        [SerializeField] protected EtatMatch etatDuMatch;
        [SerializeField] protected TypeMatch typeDuMatch;
        [SerializeField] protected GameObject songoMatchPrefab;
        [SerializeField] protected Table tableMatch;
        [SerializeField] protected PauseMenu pauseMenu;
        [SerializeField] protected GameObject pionPrefab;
        [SerializeField] protected List<GameObject> typePionPrefab = new List<GameObject>();
        [SerializeField] protected List<Pion> listePions = new List<Pion>();


        protected Match()
        {
            //this.pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        }

        public ResultatMatch ResultatDuMatch { get => resultatDuMatch; set => resultatDuMatch = value; }
        public EtatMatch EtatDuMatch { get => etatDuMatch; set => etatDuMatch = value; }
        public TypeMatch TypeDuMatch { get => typeDuMatch; set => typeDuMatch = value; }
        public Table TableMatch { get => tableMatch; set => tableMatch = value; }

        protected abstract void initialiseJoueurs();
        public IEnumerator instancierLesPions()
        {
            etatDuMatch = EtatMatch.Debut;
            for (int i = 0; i < 70; i++)
            {
                listePions.Add(Fonctions.instancierObjet(typePionPrefab[PlayerPrefs.GetInt("typePion")], tableMatch.Caisse.transform.position + new Vector3(Random.Range(-1.9f, 1.9f), 5f, Random.Range(-1.9f, 1.9f))).GetComponent<Pion>());
                listePions[i].gameObject.name = "pion" + (i + 1).ToString();
                yield return new WaitForSeconds(Case.tempsDepotCaisse);
            }
            StartCoroutine(partagerLesPions());
        }
        public IEnumerator partagerLesPions()
        {
            int i = 0;
            while (i < 70)
            {
                for (int j = 0; j < 14; j++)
                {
                    listePions[i].transform.position = tableMatch.ListeCases[j].transform.position;
                    tableMatch.ListeCases[j].ajouterPion(listePions[i]);
                    i += 1;
                }
                yield return new WaitForSeconds(Case.tempsAttente);
            }
        }
        public abstract void verifierEtatDuMatch(int idCase);
        public abstract void tourJoueur(int numPosition);
       
    }
}