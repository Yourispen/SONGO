using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : MonoBehaviour
{
    private string nom;

    string recupere_le_nom()
    {
        return nom;
    }
    void donner_le_nom(string nom_saisi)
    {
        nom = nom_saisi;
    }

}
