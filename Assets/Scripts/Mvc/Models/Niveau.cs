using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class Niveau : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private string nomNiveau;

        public static Dictionary<int, string> dictNiveau = new Dictionary<int, string>
        {
            [1] = "Débutant",
            [2] = "Débutant 1"
        };

        public Niveau()
        {
        }

        public Niveau(int id, string nomNiveau)
        {
            Id = id;
            NomNiveau = nomNiveau;
        }

        //[SerializeField] private string libelle;

        public int Id { get => id; set => id = value; }
        public string NomNiveau
        {
            get
            {
                nomNiveau = dictNiveau[id];
                return nomNiveau;
            }
            set => nomNiveau = value;
        }

        public static string getNomNiveau()
        {
            return dictNiveau[PlayerPrefs.GetInt("idNiveau")];
        }

    }
}
