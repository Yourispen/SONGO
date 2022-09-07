using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mvc.Core;
using TMPro;

namespace Mvc.Entities
{
    public class ScoreMatch : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text textScoreMatch;
        [SerializeField] private Match match;

        public Match Match { get => match; set => match = value; }
        public TMP_Text TextScoreMatch { get => textScoreMatch; set => textScoreMatch = value; }

        public void afficherScoreMatch()
        {
            string score = match.Joueur1.Surnom + " : " + match.Joueur1.NombreVictoire + "\n" +
            match.Joueur2.Surnom + " : " + match.Joueur2.NombreVictoire;
            Fonctions.changerTexte(textScoreMatch, score);
        }
    }
}
