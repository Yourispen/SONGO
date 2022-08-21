using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Core
{
    public class Swipe : MonoBehaviour
    {

        [SerializeField] private Vector2 startPosition, stopPosition;
        float dragDistance = 100f;

        void Update()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    startPosition = touch.position;
                    stopPosition = touch.position;
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
