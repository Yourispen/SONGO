using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plan_sur_la_case : MonoBehaviour
{
    private Material couleur_initiale;
    [SerializeField] private Material couleur_1;
    [SerializeField] private Material couleur_2;
    public Joueur joueur_1;
    public Joueur joueur_2;
    public GameObject _case;

    Scene scene;
    private void Start()
    {
        couleur_initiale= _case.gameObject.GetComponent<Renderer>().material;
        scene = SceneManager.GetActiveScene();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (joueur_1.mon_tour)
        {
            StartCoroutine(changer_couleur(couleur_1));
        }
        else if (joueur_2.mon_tour)
        {
            StartCoroutine(changer_couleur(couleur_2));
        }

    }
    IEnumerator changer_couleur(Material couleur)
    {
        _case.gameObject.GetComponent<Renderer>().material = couleur;
        yield return new WaitForSeconds(0.2f);
        _case.gameObject.GetComponent<Renderer>().material = couleur_initiale;
    }
}
