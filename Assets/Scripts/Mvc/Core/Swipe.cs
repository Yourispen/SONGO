using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Models;

namespace Mvc.Core
{
    public class Swipe : MonoBehaviour
    {

        [SerializeField] private Vector2 startPosition, stopPosition;
        //[SerializeField] private Joueur joueur;
        [SerializeField] private float tempsActuel;
        [SerializeField] private float tempsDepart;
        [SerializeField] private Mvc.Models.Joueur joueur;
        [SerializeField] private float dragDistance = 100f;

        public Models.Joueur Joueur { get => joueur; set => joueur = value; }

        void Start()
        {
            //joueur=this.gameObject.GetComponent<>
        }

        void Update()
        {
            if(Joueur.Tour==Tour.Aucun)
                return;
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    tempsDepart = Time.timeSinceLevelLoad;
                    startPosition = touch.position;
                    stopPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Stationary)
                {
                    tempsActuel = Time.timeSinceLevelLoad;
                    if (tempsActuel >= tempsDepart + 0.3f)
                    {
                        Debug.Log("Maintient");
                    }
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    stopPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    stopPosition = touch.position;
                    if (Mathf.Abs(stopPosition.x - startPosition.x) > dragDistance || Mathf.Abs(stopPosition.y - startPosition.y) > dragDistance)
                    {
                        if (Mathf.Abs(stopPosition.x - startPosition.x) > Mathf.Abs(stopPosition.y - startPosition.y))
                        {
                            if (stopPosition.x > startPosition.x)
                            {
                                Debug.Log("Swipe à droite");
                            }
                            else
                            {
                                Debug.Log("Swipe à gauche");
                            }
                        }
                        else
                        {
                            if (stopPosition.y > startPosition.y)
                            {
                                Debug.Log("Swipe en haut");
                            }
                            else
                            {
                                Debug.Log("Swipe en bas");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Tap");
                        
                    }
                }
            }
        }
    }
}
