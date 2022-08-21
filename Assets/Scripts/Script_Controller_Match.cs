using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Controller_Match : MonoBehaviour
{
    #region Variables

    [Header("temps de dépot des pions et transition des couleurs")]
    //temps mis après le dépot d'un pion
    protected float temps_depot_caisse = 0.02f;
    protected float temps_depot_case = 0.2f;

    [Header("pions du match")]
    //liste des pions du match
    [SerializeField] protected List<GameObject> pions_match = new List<GameObject>();

    [Header("scripts")]
    public Joueur joueur_1;
    public Joueur joueur_2;

    [Header("caisse")]
    [SerializeField] protected GameObject caisse;
    [SerializeField] protected Vector3 hauteur = new Vector3(0, 1f, 0);

    //numero de la case de départ
    public int numero_case_depart;

    //temps pour le tactile
    protected float temps_actuel, temps_depart;

    //tour de chaque joueur
    //public int tour;

    //deplacement en cours d'une case
    public bool en_deplacement;

    //mouvement des cases
    public bool peut_jouer;
    public bool peut_compter;
    public bool match_fini;

    [SerializeField] protected GameObject pion_prefab;
    protected GameObject pion_instance;
    [SerializeField] protected GameObject _case;


    #endregion

    #region Fonctions voids

    //rejouer le tour si ce n'est pas jouable
    public void rejoueur_le_tour(int numero_joueur)
    {
        if (numero_joueur == 1)
        {
            joueur_1.mon_tour = true;
        }
        else
        {
            joueur_2.mon_tour = true;
        }
        en_deplacement = false;
        numero_case_depart=0;
    }

    #endregion

    #region Fonctions Coroutines

    //générer les pions pour le match
    public IEnumerator generer_pions()
    {
        pions_match.Clear();

        for (int i = 1; i <= 70; i++)
        {
            pion_instance = Instantiate(pion_prefab, caisse.transform.position + new Vector3(Random.Range(-1.9f, 1.9f), -0.5f, Random.Range(-1.9f, 1.9f)), Quaternion.identity);
            pion_instance.name = "pion" + i.ToString();
            pions_match.Add(pion_instance);
            yield return new WaitForSeconds(temps_depot_caisse);
        }
        StartCoroutine("partager_pions");
    }

    #endregion

    #region Fonctions ints



    #endregion

    #region Fonctions bools



    #endregion

}
