using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Models
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private bool enPause;

        [SerializeField] private Match match = GameObject.Find("Match").GetComponent<Match>();

        public bool EnPause { get => enPause; set => enPause = value; }
    }
}
