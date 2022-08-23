using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public class Case : MonoBehaviour
    {
        [SerializeField] private string libelle;
        [SerializeField] private Material couleurInitiale;
        [SerializeField] private Material couleurMouvement;
        [SerializeField] private bool peutJouer;
        [SerializeField] private Table table;
        [SerializeField] private List<Pion> listePions = new List<Pion>();
        [SerializeField] private Cube cube;
        [SerializeField] private int id;
        public static float tempsDepotCaisse = 0;//0.02f;
        public static float tempsDepotCase = 0.3f;
        public static float tempsAttente = 0.5f;
        [SerializeField] private List<Material> listeCouleurs = new List<Material>();


        public string Libelle { get => libelle; set => libelle = value; }
        public Material CouleurInitiale { get => couleurInitiale; set => couleurInitiale = value; }
        public Material CouleurMouvement { get => couleurMouvement; set => couleurMouvement = value; }
        public bool PeutJouer { get => peutJouer; set => peutJouer = value; }
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
            libelle = gameObject.name;
            couleurInitiale = this.GetComponent<Renderer>().material;
        }

        public IEnumerator deplacerLesPionsCase()
        {
            this.gameObject.GetComponent<Clignote>().enabled = true;
            Vector3 ajoutPosition = new Vector3(0, 0, 0);
            int idCaseSuivante = id + 1;
            if (listePions.Count == 1)
            {
                if (id == 6 || id == 13)
                {
                    idCaseSuivante = id == 6 ? 14 : 15;
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
                yield return new WaitForSeconds(tempsAttente);
                bool tourComplet = false;
                int nbPionsDeplace = 0;
                int idCaseTourComplet = id;
                foreach (var pion in listePions)
                {
                    idCaseSuivante = idCaseSuivante == 14 ? 0 : idCaseSuivante;
                    tourComplet = idCaseSuivante == idCaseTourComplet ? true : false;
                    if (tourComplet)
                    {
                        if (id < 7)
                        {
                            idCaseTourComplet = 0;
                            if (listePions.Count - nbPionsDeplace == 1)
                            {
                                listePions[0].deplacerPionCase(Table.ListeCases[14], ajoutPosition);
                                idCaseSuivante = 14;
                                break;
                            }
                            idCaseSuivante = 7;

                        }
                        else if (id >= 7)
                        {
                            idCaseTourComplet = 7;
                            if (listePions.Count - nbPionsDeplace == 1)
                            {
                                listePions[0].deplacerPionCase(Table.ListeCases[15], ajoutPosition);
                                idCaseSuivante = 15;
                                break;
                            }
                            idCaseSuivante = 0;
                        }
                    }
                    pion.deplacerPionCase(Table.ListeCases[idCaseSuivante], ajoutPosition);
                    nbPionsDeplace += 1;
                    idCaseSuivante += 1;
                    yield return new WaitForSeconds(tempsDepotCase);
                }
                yield return new WaitForSeconds(tempsAttente);
            }
            listePions.Clear();
            StartCoroutine(table.mangerLesPions(this, Table.ListeCases[idCaseSuivante - 1]));
        }

        public void deplacerLesPionsGrandeCase()
        {
            float z = -3f;
            Vector3 ajoutPosition = new Vector3(Random.Range(-0.2f, 0.2f), 0, z);
            foreach (var pion in listePions)
            {
                if (this.id < 7)
                {
                    pion.deplacerPionCase(Table.ListeCases[15], ajoutPosition);
                }
                else
                {
                    pion.deplacerPionCase(Table.ListeCases[14], ajoutPosition);
                }
                z += 1.5f;
                ajoutPosition = new Vector3(Random.Range(-0.2f, 0.2f), 0, z);
            }
            listePions.Clear();
        }

        public IEnumerator compterLesPionsCase(int pionCompte, TMPro.TMP_Text textPlaqueCompteur)
        {
            if (pionCompte <= listePions.Count)
            {
                Fonctions.changerTexte(textPlaqueCompteur, pionCompte.ToString());
                yield return new WaitForSeconds(tempsDepotCase);
            }
        }
        public void changerCouleurCase(Material couleur)
        {
            gameObject.GetComponent<Renderer>().material = couleur;
        }

    }
}
