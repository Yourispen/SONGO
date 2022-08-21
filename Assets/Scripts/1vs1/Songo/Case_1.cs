using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_1 : MonoBehaviour
{
    public Dictionary<string, List<int>> liste_des_matchs= new Dictionary<string, List<int>>();
    public List<int> match1 = new List<int> { 1, 2, 3 };
    public List<int> match2 = new List<int> { 1 };

    private void Start()
    {
        liste_des_matchs["liste des matchs"] = match1;
        liste_des_matchs["liste des matchs"] = match2;
    }
    private void Update()
    {

    }
}
