using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Scene_Match : Script_Controller_Scene_Match
{
    #region Variables

    [SerializeField] private TMPro.TMP_ColorGradient gradient_rouge;
    [SerializeField] private TMPro.TMP_ColorGradient gradient_vert;

    [Header("Cameras des joueurs")]
    [SerializeField] private GameObject camera_P1;
    [SerializeField] private GameObject camera_P2;

    [SerializeField] private GameObject Background_Profil_P2;
    [SerializeField] private GameObject Background_Profil_P1;

    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;

    [SerializeField] private TMPro.TMP_Text Text_Tour_P1;
    [SerializeField] private TMPro.TMP_Text Text_Tour_P2;

    [SerializeField] private GameObject Background_Connexion;
    [SerializeField] private TMPro.TMP_Text Text_Connexion;

    [SerializeField] private GameObject Timer_Prefab;
    private GameObject Timer;

    public Controller_Match_Online controlleur_match;

    public Deplacement deplacement_P1;
    public Deplacement deplacement_P2;

    public GameObject chargement;

    public GameManager gameManager;

    [SerializeField] private int numero_joueur;

    //si le joueur quitte le match proprement
    public bool quitter;
    #endregion

    #region Fonctions Principale Unity

    // Start is called before the first frame update
    void Start()
    {
        numero_joueur = gameManager.numero_joueur;

        afficher_noms();
        afficher_score();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        en_pause = false;

        quitter = false;

        Debug.Log("Je suis le joueur numero " + numero_joueur);
        if (numero_joueur == 2)
        {
            camera_P2.SetActive(true);
            camera_P1.SetActive(false);
            Background_Profil_P1.transform.position = P2.transform.position;
            Background_Profil_P2.transform.position = P1.transform.position;
            Background_Profil_P2.SetActive(true);
            Background_Profil_P1.SetActive(true);
            Counter_Pieces_P2.SetActive(true);
            Tour_P2.SetActive(true);
        }
        else if(numero_joueur == 1)
        {
            camera_P1.SetActive(true);
            camera_P2.SetActive(false);
            Background_Profil_P2.SetActive(true);
            Background_Profil_P1.SetActive(true);
            Counter_Pieces_P1.SetActive(true);
            Tour_P1.SetActive(true);
        }
    }
    #endregion

    #region Boutons

    public void bouton_pause()
    {
        //active le Menu_Pause
        MenuPause.SetActive(true);
        en_pause = true;
        //désactive le bouton_Pause
        ButtonPause.SetActive(false);
    }

    public void bouton_continue()
    {
        //active le bouton_Pause
        ButtonPause.SetActive(true);
        //désactive le Menu_Pause
        MenuPause.SetActive(false);
        en_pause = false;
    }

    public void bouton_resign()
    {
        MenuPause.SetActive(false);
        if (GameObject.Find("Joueur") != null)
            GameObject.Find("Joueur").GetComponent<PlayerScript>().fin_du_match(true);
        string msg ="Vous avez abandonné(e).";
        afficher_message(msg, true);
        if (numero_joueur == 1)
        {
            controlleur_match.fin_du_match(joueur_2, true);
        }
        else
        {
            controlleur_match.fin_du_match(joueur_1, true);
        }
            
    }

    public void bouton_replay()
    {
        Counter_Pieces_P1.SetActive(false);
        //désactive les objets pour afficher la fin du match
        Text_Victoire_Mini.enabled = false;
        Fin_Match.SetActive(false);
        Background_Victoire.SetActive(false);
        ButtonPause.SetActive(true);
        controlleur_match.restart();
    }

    public void bouton_quit()
    {
        quitter = true;
        gameManager.quitter_le_match();
    }

    public void bouton_table_jeu()
    {
        Background_Connexion.SetActive(false);
        en_pause = false;
        Text_Victoire_Mini.GetComponent<TMPro.TMP_Text>().colorGradientPreset = Text_Victoire.GetComponent<TMPro.TMP_Text>().colorGradientPreset;
        string texte_victoire= Text_Victoire.GetComponent<TMPro.TMP_Text>().text;
        if (texte_victoire.Contains("Félicitations"))
            Text_Victoire_Mini.GetComponent<TMPro.TMP_Text>().text = "Vous avez gagné !!";
        else
            Text_Victoire_Mini.GetComponent<TMPro.TMP_Text>().text = "Vous avez perdu !!";
        Text_Victoire_Mini.enabled = true;
        Background_Victoire_Mini.SetActive(true);
        Background_Victoire.SetActive(false);
        controlleur_match.peut_compter = true;
    }

    #endregion

    #region Fonctions voids

    public void afficher_nombre_de_pions(string nombre)
    {
        if (numero_joueur == 1)
            Text_Compteur_P1.GetComponent<TMPro.TMP_Text>().text = nombre;
        else if (numero_joueur == 2)
            Text_Compteur_P2.GetComponent<TMPro.TMP_Text>().text = nombre;
    }


    //afficher les infos pour le tour du joueur
    public void afficher_tour()
    {
        if (joueur_1.mon_tour)
        {
            if (numero_joueur == 1)
            {
                //Handheld.Vibrate();
                string texte= " à toi de jouer ";
                deplacement_P1.texte_saisi= texte;
                //Text_Tour_P1.GetComponent<TMPro.TMP_Text>().text = texte;
            }
            else if (numero_joueur == 2)
            {
                string texte = " à lui de jouer ";
                deplacement_P2.texte_saisi = texte;
                //Text_Tour_P1.GetComponent<TMPro.TMP_Text>().text = texte;
            }
        }
        else if (joueur_2.mon_tour)
        {
            if (numero_joueur == 2)
            {
                //Handheld.Vibrate();
                string texte = " à toi de jouer ";
                deplacement_P2.texte_saisi = texte;
                //Text_Tour_P2.GetComponent<TMPro.TMP_Text>().text = " à toi de jouer ";
            }
            else if (numero_joueur == 1)
            {
                string texte = " à lui de jouer ";
                deplacement_P1.texte_saisi = texte;
                //Text_Tour_P2.GetComponent<TMPro.TMP_Text>().text = " à lui de jouer ";
            }
        }
    }

    public void afficher_message(string message, bool active)
    {
        if (numero_joueur == 1)
        {
            Tour_P1.SetActive(!active);
        }
        else if (numero_joueur == 2)
        {
            Tour_P2.SetActive(!active);
        }
        Text_Connexion.GetComponent<TMPro.TMP_Text>().text = message;
        Background_Connexion.SetActive(active);
    }

    public void instancier_timer()
    {
        if (GameObject.Find(Timer_Prefab.name) == null)
        {
            Timer = Instantiate(Timer_Prefab);
            Timer.name = Timer_Prefab.name;
        }
    }

    public void detruit_timer()
    {
        if (GameObject.Find(Timer_Prefab.name) != null)
            Destroy(Timer.gameObject);
    }

    //affiche la fin du match
    public void afficher_fin_match()
    {
        afficher_score();
        Tour_P2.SetActive(false);
        Tour_P1.SetActive(false);
        MenuPause.SetActive(false);
        Counter_Pieces_P2.SetActive(joueur_2.mon_tour);
        Counter_Pieces_P1.SetActive(joueur_1.mon_tour);

        ButtonPause.SetActive(false);
        //active les objets pour afficher la fin du match
        Fin_Match.SetActive(true);
        Background_Victoire.SetActive(true);

        if (joueur_1.recupere_resultat())
        {
            if (numero_joueur == 1)
            {
                Text_Victoire.GetComponent<TMPro.TMP_Text>().colorGradientPreset = gradient_vert;
                Text_Victoire.GetComponent<TMPro.TMP_Text>().text = "Félicitations, vous avez gagné !!";
            }
            else if (numero_joueur == 2)
            {
                Text_Victoire.GetComponent<TMPro.TMP_Text>().colorGradientPreset = gradient_rouge;
                Text_Victoire.GetComponent<TMPro.TMP_Text>().text = "Désolé, vous avez perdu !!";
            }
        }
        else if (joueur_2.recupere_resultat())
        {
            if (numero_joueur == 2)
            {
                Text_Victoire.GetComponent<TMPro.TMP_Text>().colorGradientPreset = gradient_vert;
                Text_Victoire.GetComponent<TMPro.TMP_Text>().text = "Félicitations, vous avez gagné !!";
            }
            else if (numero_joueur == 1)
            {
                Text_Victoire.GetComponent<TMPro.TMP_Text>().colorGradientPreset = gradient_rouge;
                Text_Victoire.GetComponent<TMPro.TMP_Text>().text = "Désolé, vous avez perdu !!";
            }
        }
        else
        {
            Text_Victoire.GetComponent<TMPro.TMP_Text>().colorGradientPreset = gradient_vert;
            Text_Victoire.GetComponent<TMPro.TMP_Text>().text = "Match nul !!";
        }
    }
    

    #endregion
}
