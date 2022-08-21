using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    [Header("Nom du joueur")]
    //nom du joueur
    [SerializeField] private string nom;

    [Header("Resultat du joueur")]
    //resultat
    [SerializeField] private bool perdre, gagner;

    [Header("Tour du joueur")]
    //tour de jour
    public bool mon_tour;

    [Header("score du match")]
    //score du match
    [SerializeField] private int score_joueur;

    [Header("Numero du joueur")]
    //numero du joueur
    [SerializeField] int numero;

    private void Start()
    {
        gagner = false;
        perdre = false;
        score_joueur = 0;
    }

    //retourne le nom du joueur
    public string recupere_le_nom()
    {
        return nom;
    }

    //saisir le nom du joueur
    public void donner_le_nom(string nom_saisi)
    {
        nom = nom_saisi;
    }

    //retourne le numero du joueur
    public int recupere_numero()
    {
        return numero;
    }

    public bool victoire()
    {
        gagner = true;
        score_joueur += 1;
        return gagner;
    }

    public bool recupere_resultat()
    {
        return gagner;
    }

    public bool defaite()
    {
        perdre = true;
        return perdre;
    }

    public int recupere_score_joueur()
    {
        return score_joueur;
    }

    public void restart()
    {
        gagner = false;
        perdre = false;
    }
}
