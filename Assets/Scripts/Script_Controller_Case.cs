using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Controller_Case : MonoBehaviour
{
    #region Variables
    [Header("pions de la case")]
    //la liste des pions de la case
    [SerializeField] protected List<GameObject> pions_case = new List<GameObject>();
    protected int compteur_pions;

    [Header("informations de la case")]
    [SerializeField] protected int numero_case;
    //coté 2 ou 1
    //[SerializeField] private int cote_case;

    [Header("caisse")]
    //la caisse pour mettre les pions
    [SerializeField] protected GameObject caisse;

    [Header("scripts")]
    //scripts du joueur
    public Joueur joueur;
    //script adversaire
    public Joueur adversaire;


    [Header("temps de dépot des pions et transition des couleurs")]
    //temps mis après le dépot d'un pion
    protected float temps_depot_caisse = 0.125f;
    protected float temps_depot_case = 0.5f;
    protected float temps_transition_couleur = 0.125f;

    //coordonnées de la case
    [Header("coordonnées")]
    [SerializeField] protected float x_min;
    [SerializeField] protected float x_max;
    [SerializeField] protected float z_min;
    [SerializeField] protected float z_max;
    [SerializeField] protected float vecteur;

    //coordonnées pour le tactile
    protected Vector3 position_debut_doigt;
    protected Vector3 position_debut_doigt_convertie;
    protected Vector3 position_fin_doigt;
    protected Vector3 position_fin_doigt_convertie;
    protected bool toucher_case;

    //temps pour le tactile
    protected float temps_actuel, temps_depart;

    [Header("bouton du clavier")]
    [SerializeField] protected string bouton;

    [Header("couleurs des cases")]
    [SerializeField] protected Material couleur_de_transition_joueur;
    [SerializeField] protected Material couleur_de_toucher_joueur;
    [SerializeField] protected Material couleur_de_transition_adversaire;
    [SerializeField] protected Material couleur_de_toucher_adversaire;
    protected Material couleur_de_initiale;

    #endregion

    #region Fonctions voids

    //ajoute un pion dans la case
    public void ajouter_pion(GameObject pion)
    {
        //ajoute le pion dans la liste, ensuite déplace le pion dans la case actuelle.
        pions_case.Add(pion);
        deplace_pion(pion);
    }

    //enlève un pion dans la case
    public void enlever_pion(GameObject pion)
    {
        pions_case.Remove(pion);
    }


    //mange les pions pour le l'adversaire
    public void manger_pions(List<GameObject> pions)
    {
        float z = -3f;
        while (pions.Count > 0)
        {
            pions[0].transform.position = gameObject.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0, z);
            pions_case.Add(pions[0]);
            pions.RemoveAt(0);
            z += 1.5f;
        }
    }

   
    //déplace un pion dans une case
    public void deplace_pion(GameObject pion)
    {

        pion.transform.position = gameObject.transform.position;

    }

    public void recupere_pion(GameObject pion)
    {
        pions_case.Add(pion);
    }

    public void restart()
    {
        couleur_de_initiale = gameObject.GetComponent<Renderer>().material;
        pions_case.Clear();
    }

    #endregion

    #region Fonctions Coroutines

    public IEnumerator allumer_objet()
    {
        if (joueur.mon_tour)
            gameObject.GetComponent<Renderer>().material = couleur_de_transition_joueur;
        if (adversaire.mon_tour)
            gameObject.GetComponent<Renderer>().material = couleur_de_transition_adversaire;
        yield return new WaitForSeconds(temps_transition_couleur);
        gameObject.GetComponent<Renderer>().material = couleur_de_initiale;
    }

    #endregion

    #region Fonctions ints

    //retourne le nombre de pions de la case
    public int nombre_de_pions()
    {
        return pions_case.Count;
    }

    #endregion

    #region Fonctions bools

    public bool toucher(Vector3 position)
    {
        float touchX = position.x;
        float touchZ = position.z;
        if (touchX > x_min && touchX < x_max && touchZ > z_min && touchZ < z_max)
        {

            if (joueur.mon_tour)
            {
                gameObject.GetComponent<Renderer>().material = couleur_de_toucher_joueur;
            }
            else if (adversaire.mon_tour)
            {
                gameObject.GetComponent<Renderer>().material = couleur_de_toucher_adversaire;
            }
            return true;
        }
        return false;

    }

    #endregion
}
