using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private string libelle;
        [SerializeField] private Case cases;

        public int Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public Case Cases { get => cases; set => cases = value; }
    }
}