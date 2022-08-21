using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlleur_scene_training : Script_Controller_Scene_Match
{
    #region Variables

    [Header("Enregistrements")]
    [SerializeField] private GameObject Registrations;
    [SerializeField] private TMPro.TMP_Text TextError;

    public List<CaseTraining> nombrePionsCases = new List<CaseTraining>();

    public Controller_Match_1vs1 controlleur_match;

    [SerializeField]
    private string scene_precedante = "Setting";


    #endregion

    #region Fonctions Principale Unity

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //le Menu_Pause est désactivé
        en_pause = false;
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

    public void bouton_back()
    {
        //va à la scène Offline
        SceneManager.LoadScene(scene_precedante);
    }

    public void bouton_resign()
    {
        
    }

    public void bouton_quit()
    {
        //va à la scène Offline
        SceneManager.LoadScene(scene_precedante);
    }

    public void bouton_replay()
    {
        Counter_Pieces_P1.SetActive(false);
        //désactive les objets pour afficher la fin du match
        Text_Victoire_Mini.enabled = false;
        Fin_Match.SetActive(false);
        Background_Victoire.SetActive(false);
        Background_Victoire_Mini.SetActive(false);
        ButtonPause.SetActive(true);
        controlleur_match.restart();
    }

    public void bouton_start()
    {

    }

    public void bouton_table_jeu()
    {
        Text_Victoire_Mini.GetComponent<TMPro.TMP_Text>().text = Text_Victoire.GetComponent<TMPro.TMP_Text>().text;
        Text_Victoire_Mini.enabled = true;
        Background_Victoire_Mini.SetActive(true);
        Background_Victoire.SetActive(false);
        controlleur_match.peut_compter = true;
    }

    #endregion
}
