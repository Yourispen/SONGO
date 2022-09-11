using UnityEngine;

namespace Mvc.Core
{
    public class Clignote : MonoBehaviour
    {
        [SerializeField] private float tempsActuel, tempsDepart;
        [SerializeField] private Material couleur1;
        [SerializeField] private Material couleur2;
        [SerializeField] private Material couleurInitiale;
        void Start()
        {
            //couleurInitiale = this.gameObject.GetComponent<Renderer>().material;
            tempsDepart = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            tempsActuel = Time.time;
            if (tempsActuel >= tempsDepart + 0.2f)
            {
                tempsDepart = tempsActuel;
                clignoterCase();
            }
        }

        public void clignoterCase()
        {
            this.gameObject.GetComponent<Renderer>().material = couleur1;
            couleur1 = couleur2;
            couleur2 = gameObject.GetComponent<Renderer>().material;
        }
        public void initialiseCouleur()
        {
            this.gameObject.GetComponent<Renderer>().material = couleurInitiale;
        }
        void OnDisable()
        {
            initialiseCouleur();
        }

    }

}