using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class Caisse : MonoBehaviour
    {
        [SerializeField] private List<Pion> listePions = new List<Pion>();
        [SerializeField] private Table table;

        public List<Pion> ListePions { get => listePions; }
        public Table Table { get => table; set => table = value; }

        public void deplacerTousLesPions()
        {
            foreach (var pion in listePions)
            {
                pion.StartCoroutine("deplacerPionCase()");
            }
        }

    }
}