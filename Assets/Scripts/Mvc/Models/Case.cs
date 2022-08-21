using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class Case : MonoBehaviour
    {
        [SerializeField] private string libelle;
        [SerializeField] private Material couleurInitiale;
        [SerializeField] private Material couleurMouvement;
        [SerializeField] private bool peutJouer;
        [SerializeField] private int numeroCase;
        [SerializeField] private Table table;
        [SerializeField] private List<Pion> listePions = new List<Pion>();
        [SerializeField] private Cube cube;
        [SerializeField] private int id;
        protected float tempsDepotCaisse = 0.02f;
        protected float tempsDepotCase = 0.2f;


        public string Libelle { get => libelle; set => libelle = value; }
        public Material CouleurInitiale { get => couleurInitiale; set => couleurInitiale = value; }
        public Material CouleurMouvement { get => couleurMouvement; set => couleurMouvement = value; }
        public bool PeutJouer { get => peutJouer; set => peutJouer = value; }
        public int NumeroCase { get => numeroCase; set => numeroCase = value; }
        public List<Pion> ListePions { get => listePions; }
        public Table Table { get => table; set => table = value; }
        public int Id { get => id; set => id = value; }

        public void ajouterPion(Pion pion)
        {
            listePions.Add(pion);
            pion.CaseActuelle = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            couleurInitiale = this.GetComponent<Renderer>().material;
        }

        public IEnumerator deplacerLesPionsCase()
        {
            Vector3 ajoutPosition=new Vector3(0, 0, 0);
            int idCaseSuivante = id;
            if (listePions.Count == 1)
            {
                if (id == 6 || id==13)
                {
                    idCaseSuivante = id==6?14:15;
                }
                else
                {
                    idCaseSuivante = id + 1;
                }
                listePions[0].deplacerPionCase(Table.ListeCases[idCaseSuivante], ajoutPosition);
            }
            else
            {
                
                foreach (var pion in listePions)
                {
                    pion.deplacerPionCaisse(Table.Caisse);
                    yield return new WaitForSeconds(tempsDepotCaisse);
                }
                yield return new WaitForSeconds(1);
                bool tourComplet = false;
                int nbPionsDeplace=0;
                foreach (var pion in listePions)
                {
                    idCaseSuivante = idCaseSuivante == 13 ? 0 : idCaseSuivante + 1;
                    tourComplet=idCaseSuivante==id?true:false;
                    if(tourComplet){
                        if(id<7 && idCaseSuivante==0){
                            if (listePions.Count-nbPionsDeplace == 1)
                            {
                                listePions[0].deplacerPionCase(Table.ListeCases[14], ajoutPosition);
                                break;
                            }
                            idCaseSuivante=7;
                        }
                        else if(id>=7 && idCaseSuivante==7){
                            if(listePions.Count - nbPionsDeplace ==1){
                                listePions[0].deplacerPionCase(Table.ListeCases[15], ajoutPosition);
                                break;
                            }
                            idCaseSuivante = 0;
                        }
                    }
                    pion.deplacerPionCase(Table.ListeCases[idCaseSuivante],ajoutPosition);
                    nbPionsDeplace+=1;
                    yield return new WaitForSeconds(tempsDepotCase);
                }
                listePions.Clear();
                yield return new WaitForSeconds(1);
            }
        }

        public void deplacerLesPionsGrandeCase(){
            float z = -3f;
            Vector3 ajoutPosition = new Vector3(Random.Range(-0.2f, 0.2f), 0, z);
            foreach (var pion in listePions)
            {
                if(this.id<7){
                    pion.deplacerPionCase(Table.ListeCases[14],ajoutPosition);
                }
                else{
                    pion.deplacerPionCase(Table.ListeCases[15], ajoutPosition);
                }
                z += 1.5f;
            }
        }

    }
}
