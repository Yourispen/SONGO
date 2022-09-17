using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
using UnityEngine.UI;
using TMPro;

namespace Mvc.Entities
{
    public class FinMatchMenu : MonoBehaviour
    {
        [SerializeField] private float delaiPourRejouer;
        [SerializeField] private Match match;
        [SerializeField] private GameObject menuFinMatch;
        [SerializeField] private GameObject backgroundVictoire;
        [SerializeField] private GameObject backgroundVictoireMini;
        [SerializeField] private TMPro.TMP_Text textVictoire;
        [SerializeField] private TMPro.TMP_Text textVictoireMini;
        [SerializeField] private TMP_ColorGradient couleurDefaite;
        [SerializeField] private TMP_ColorGradient couleurVictoire;
        [SerializeField] private TMP_ColorGradient couleurMatchNul;
        [SerializeField] private Button buttonRejouer;

        public Match Match { get => match; set => match = value; }
        public TMP_Text TextVictoire { get => textVictoire; set => textVictoire = value; }
        public TMP_Text TextVictoireMini { get => textVictoireMini; set => textVictoireMini = value; }
        public float DelaiPourRejouer { get => delaiPourRejouer; set => delaiPourRejouer = value; }
        public GameObject MenuFinMatch { get => menuFinMatch; set => menuFinMatch = value; }
        public GameObject BackgroundVictoire { get => backgroundVictoire; set => backgroundVictoire = value; }
        public GameObject BackgroundVictoireMini { get => backgroundVictoireMini; set => backgroundVictoireMini = value; }
        public TMP_ColorGradient CouleurDefaite { get => couleurDefaite; set => couleurDefaite = value; }
        public TMP_ColorGradient CouleurVictoire { get => couleurVictoire; set => couleurVictoire = value; }
        public TMP_ColorGradient CouleurMatchNul { get => couleurMatchNul; set => couleurMatchNul = value; }
        public Button ButtonRejouer { get => buttonRejouer; set => buttonRejouer = value; }

        //Name_P1.GetComponent<TMPro.TMP_Text>().text
        public void boutonQuitter()
        {
            if (Fonctions.sceneActuelle("SceneMatch1vs1"))
            {
                Fonctions.changerDeScene("ScenePlay");
            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                ((MatchEnLigne)match).MatchEnLigneController.SceneController.PhotonManager.QuitterMatch = true;
                ((MatchEnLigne)match).MatchEnLigneController.SceneController.PhotonManager.quitterLobby();
            }
            else if (Fonctions.sceneActuelle("SceneMatchEntrainement"))
            {
                Fonctions.changerDeScene("SceneMenuPrincipal");
            }
        }
        public void boutonRejouer()
        {
            Fonctions.activerObjet(backgroundVictoire);
            Fonctions.changerTexte(textVictoire);
            Fonctions.changerTexte(textVictoireMini);
            Fonctions.desactiverObjet(backgroundVictoireMini);
            Fonctions.activerObjet(match.PauseMenu.BoutonPausePrefab);
            if (Fonctions.sceneActuelle("SceneMatch1vs1"))
            {
                ((MatchHorsLigne)match).rejouerMatch();
            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                ((MatchEnLigne)match).rejouerMatch();
            }
            else if (Fonctions.sceneActuelle("SceneMatchEntrainement"))
            {
                ((MatchEntrainement)match).rejouerMatch();
            }
        }
        public void boutonVoirTable()
        {
            match.PauseMenu.EnPause = false;
            Fonctions.activerObjet(backgroundVictoireMini);
            if (match.Joueur1 != null)
            {
                match.Joueur1.Tour = Tour.SonTour;
                match.Joueur1.Swipe.enabled = false;
            }
            if (match.Joueur2 != null)
            {
                match.Joueur2.Tour = Tour.SonTour;
                match.Joueur2.Swipe.enabled = false;
            }
            if (Fonctions.sceneActuelle("SceneMatch1vs1") || Fonctions.sceneActuelle("SceneMatchEntrainement"))
            {
                Fonctions.changerTexte(textVictoireMini, textVictoire.text);
                Fonctions.desactiverObjet(backgroundVictoire);
                match.Joueur1.Tour = Tour.MonTour;
                match.Joueur1.Swipe.enabled = true;
                match.OutilsJoueur.activerCompteurJoueur(1);
            }
            else if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
            {
                string texte;
                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                {
                    if (match.Joueur1 != null)
                    {
                        match.Joueur1.Tour = Tour.MonTour;
                        match.Joueur1.Swipe.enabled = true;
                    }
                    if (match.ResultatDuMatch == ResultatMatch.V1)
                    {
                        texte = "Vous avez gagné !!!";
                        textVictoireMini.colorGradientPreset = couleurVictoire;
                    }
                    else if (match.ResultatDuMatch == ResultatMatch.V2)
                    {
                        texte = "Vous avez perdu !!!";
                        textVictoireMini.colorGradientPreset = couleurDefaite;
                    }
                    else
                    {
                        texte = "Match nul !!!";
                        textVictoireMini.colorGradientPreset = couleurMatchNul;
                    }
                    match.OutilsJoueur.activerCompteurJoueur(1);

                }
                else
                {
                    if (match.Joueur1 != null)
                    {
                        match.Joueur2.Tour = Tour.MonTour;
                        match.Joueur2.Swipe.enabled = true;
                    }
                    if (match.ResultatDuMatch == ResultatMatch.V1)
                    {
                        texte = "Vous avez perdu !!!";
                        textVictoireMini.colorGradientPreset = couleurDefaite;
                    }
                    else if (match.ResultatDuMatch == ResultatMatch.V2)
                    {
                        texte = "Vous avez gagné !!!";
                        textVictoireMini.colorGradientPreset = couleurVictoire;
                    }
                    else
                    {
                        texte = "Match nul !!!";
                        textVictoireMini.colorGradientPreset = couleurMatchNul;
                    }
                    match.OutilsJoueur.activerCompteurJoueur(1);

                }
                Fonctions.changerTexte(textVictoireMini, texte);
                Fonctions.desactiverObjet(backgroundVictoire);
            }

        }
    }
}
