using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Controller_Scene_Match : MonoBehaviour
{
    #region Variables

    [Header("Pause Menu")]
    [SerializeField] protected GameObject ButtonPause;
    [SerializeField] protected GameObject MenuPause;
    public bool en_pause;

    [Header("Compteurs de pions")]
    [SerializeField] protected GameObject Counter_Pieces_P1;
    [SerializeField] protected GameObject Counter_Pieces_P2;
    [SerializeField] protected TMPro.TMP_Text Text_Compteur_P1;
    [SerializeField] protected TMPro.TMP_Text Text_Compteur_P2;

    [Header("Tour de chaque joueur")]
    [SerializeField] protected GameObject Tour_P1;
    [SerializeField] protected GameObject Tour_P2;

    [Header("Profils des joueurs")]
    [SerializeField] protected GameObject Profil_P1;
    [SerializeField] protected GameObject Profil_P2;
    [SerializeField] protected TMPro.TMP_Text Name_P2;
    [SerializeField] protected TMPro.TMP_Text Name_P1;

    [Header("Fin du match")]
    [SerializeField] protected GameObject Fin_Match;
    [SerializeField] protected GameObject Background_Victoire;
    [SerializeField] protected TMPro.TMP_Text Text_Victoire;
    [SerializeField] protected GameObject Background_Victoire_Mini;
    [SerializeField] protected TMPro.TMP_Text Text_Victoire_Mini;

    [Header("scripts")]
    public Joueur joueur_1;
    public Joueur joueur_2;

    [Header("Score du Match")]
    [SerializeField] protected TMPro.TMP_Text Score_P1;
    [SerializeField] protected TMPro.TMP_Text Score_P2;

    #endregion

    #region Fonctions voids

    public void afficher_noms()
    {
        Name_P1.GetComponent<TMPro.TMP_Text>().text = joueur_1.recupere_le_nom();
        Name_P2.GetComponent<TMPro.TMP_Text>().text = joueur_2.recupere_le_nom();
    }

    public void afficher_score()
    {
        Score_P1.GetComponent<TMPro.TMP_Text>().text = joueur_1.recupere_le_nom() + " : " + joueur_1.recupere_score_joueur().ToString();
        Score_P2.GetComponent<TMPro.TMP_Text>().text = joueur_2.recupere_le_nom() + " : " + joueur_2.recupere_score_joueur().ToString();
    }

    #endregion

    #region Boutons

    #endregion

}
