using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;
using Mvc.Controllers;
using UnityEngine.UI;
using TMPro;

namespace Mvc.Models
{
    public class AttenteMenu : MonoBehaviour

    {
        [SerializeField] private GameObject menuAttente;
        [SerializeField] private TMPro.TMP_Text codeMatch;
        [SerializeField] private Image plaqueNom1;
        [SerializeField] private TMPro.TMP_Text textPlaqueNom1;
        [SerializeField] private Image plaqueNom2;
        [SerializeField] private TMPro.TMP_Text textPlaqueNom2;
        [SerializeField] private TMPro.TMP_Text textAttente;
        [SerializeField] PhotonManager photonManager;
        [SerializeField] Button boutonRetour;

        public GameObject AttenteJoueur { get => menuAttente; set => menuAttente = value; }
        public Image PlaqueNom1 { get => plaqueNom1; set => plaqueNom1 = value; }
        public Image PlaqueNom2 { get => plaqueNom2; set => plaqueNom2 = value; }
        public TMP_Text CodeMatch { get => codeMatch; set => codeMatch = value; }
        public TMP_Text TextPlaqueNom1 { get => textPlaqueNom1; set => textPlaqueNom1 = value; }
        public TMP_Text TextPlaqueNom2 { get => textPlaqueNom2; set => textPlaqueNom2 = value; }
        public PhotonManager PhotonManager { get => photonManager; set => photonManager = value; }
        public Button BoutonRetour { get => boutonRetour; set => boutonRetour = value; }
        public TMP_Text TextAttente { get => textAttente; set => textAttente = value; }

        void Awake()
        {
            string texteCode;
            string code = PlayerPrefs.GetString("codeMatchEnCours");
            if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
            {
                texteCode = "Code du match : <color=blue><size=100>" + code + "</size></color>\nPartagez le code avec votre ami";
                Fonctions.changerTexte(textPlaqueNom1, PlayerPrefs.GetString("surnom"));
                Fonctions.activerObjet(plaqueNom1.gameObject);
            }
            else
            {
                texteCode = "Vous rejoignez le match : <color=blue><size=100>" + code + "</size></color>\n";
                Fonctions.changerTexte(textPlaqueNom2, PlayerPrefs.GetString("surnom"));
                Fonctions.activerObjet(plaqueNom2.gameObject);
            }
            Fonctions.changerTexte(codeMatch, texteCode);
            Fonctions.activerObjet(menuAttente);
        }
        void Start()
        {
            // Debug.Log("instancier Joueur");
            //photonManager.instancierUnJoueur();
        }

        public void boutonRetourAttenteJoueur()
        {
            photonManager.QuitterMatch = true;
            PhotonManager.connectePhoton = false;
            photonManager.quitterLobby();
        }
    }
}