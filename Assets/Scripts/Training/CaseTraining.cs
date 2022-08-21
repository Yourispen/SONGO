using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseTraining : MonoBehaviour
{
    private int nombrePions;

    CaseTraining()
    {
        nombrePions = 0;
    }

    public int getNombrePions()
    {
        return nombrePions;
    }

    public void setNombrePions(string nombrePions)
    {
        this.nombrePions = int.Parse(nombrePions);
    }

}
