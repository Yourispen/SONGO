using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mvc.Entities;

public class Clavier : MonoBehaviour
{
    TouchScreenKeyboard clavier;
    ResultatMatch typeMatch = 0;

    // Update is called once per frame
    void Update()
    {

    }

    public void ouvrir_clavier()
    {
        clavier = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NamePhonePad);


    }
}
