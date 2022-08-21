using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Couleur_Change : MonoBehaviour
{
    #region Variables
    //les couleurs qui vont alterner
    [Header("les différentes couleurs de la case")]
    [SerializeField] protected Material couleur1;
    [SerializeField] protected Material couleur2;
    [SerializeField] protected Material couleur_initiale;

    //pour le temps entre deux changement de couleur
    protected float temps_actuel, temps_depart;
    #endregion

    #region
    public void clignoter_case()
    {
        gameObject.GetComponent<Renderer>().material = couleur1;
        couleur1 = couleur2;
        couleur2 = gameObject.GetComponent<Renderer>().material;
    }
    #endregion
}
