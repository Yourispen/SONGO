using UnityEngine;
using Mvc.Models;
using Photon.Pun;

namespace Mvc.Core
{
    public class Swipe : MonoBehaviour
    {

        [SerializeField] private Vector2 startPosition, stopPosition;
        [SerializeField] private float tempsActuel;
        [SerializeField] private float tempsDepart;
        [SerializeField] private Mvc.Models.Joueur joueur;
        [SerializeField] private float dragDistance = 70f;
        [SerializeField] private GameObject objetTouche;
        [SerializeField] private Case caseTouche;
        [SerializeField] private bool toucheJouer;
        [SerializeField] private int compteurPion;
        RaycastHit hit;

        public Models.Joueur Joueur { get => joueur; set => joueur = value; }
        public Case CaseTouche { get => caseTouche; set => caseTouche = value; }
        public GameObject ObjetTouche { get => objetTouche; set => objetTouche = value; }

        void Start()
        {
            //joueur=this.gameObject.GetComponent<>
            toucheJouer = false;
        }

        void Update()
        {
            if (joueur.Match.EtatDuMatch == EtatMatch.Debut || joueur.Match.PauseMenu.EnPause)
            {
                if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
                {
                    if(PhotonNetwork.IsConnected){
                        if(PhotonNetwork.CurrentRoom.PlayerCount<2)
                            return;
                    }
                    else{
                        return;
                    }
                }
            }
            {
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        tempsDepart = Time.timeSinceLevelLoad;
                        compteurPion = 0;

                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        if (Physics.Raycast(ray, out hit))
                        {
                            objetTouche = hit.collider.gameObject;
                            if (objetTouche.name.CompareTo("Table") != 0 && objetTouche.name.CompareTo("Sol") != 0)
                            {
                                toucheJouer = true;
                                caseTouche = objetTouche.GetComponentInParent<Case>();
                                caseTouche.changerCouleurCase(joueur.CouleurTouche);
                            }
                        }

                        startPosition = touch.position;
                        stopPosition = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Stationary)
                    {
                        tempsActuel = Time.timeSinceLevelLoad;
                        if (tempsActuel >= tempsDepart + 0.3f)
                        {
                            //Debug.Log("Maintient");
                            if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
                            {
                                StartCoroutine(caseTouche.compterLesPionsCase(compteurPion, PlayerPrefs.GetInt("numPositionMatchEnCours") == 1 ? joueur.Match.OutilsJoueur.TextPlaqueCompteur1 : joueur.Match.OutilsJoueur.TextPlaqueCompteur2));
                            }
                            else
                            {
                                StartCoroutine(caseTouche.compterLesPionsCase(compteurPion, joueur.gameObject.name.CompareTo("joueur1") == 0 ? joueur.Match.OutilsJoueur.TextPlaqueCompteur1 : joueur.Match.OutilsJoueur.TextPlaqueCompteur2));
                            }
                            compteurPion += 1;
                        }
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        stopPosition = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        Fonctions.changerTexte(joueur.Match.OutilsJoueur.TextPlaqueCompteur1);
                        Fonctions.changerTexte(joueur.Match.OutilsJoueur.TextPlaqueCompteur2);
                        if (toucheJouer)
                        {
                            caseTouche.changerCouleurCase(caseTouche.CouleurInitiale);
                        }
                        stopPosition = touch.position;
                        if (Mathf.Abs(stopPosition.x - startPosition.x) > dragDistance || Mathf.Abs(stopPosition.y - startPosition.y) > dragDistance)
                        {
                            if (Mathf.Abs(stopPosition.x - startPosition.x) > Mathf.Abs(stopPosition.y - startPosition.y))
                            {
                                if (stopPosition.x > startPosition.x)
                                {
                                    //Debug.Log("Swipe à droite");
                                    if (toucheJouer)
                                    {
                                        if (Fonctions.sceneActuelle("SceneMatch1vs1"))
                                        {
                                            if (joueur.Tour == Tour.MonTour && joueur.gameObject.name.CompareTo("joueur2") == 0 && caseTouche.Id >= 7 && caseTouche.Id < 14)
                                            {
                                                joueur.jouerMatch(caseTouche);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //Debug.Log("Swipe à gauche");
                                    if (toucheJouer)
                                    {
                                        if (Fonctions.sceneActuelle("SceneMatchEnLigne"))
                                        {
                                            if (joueur.Tour == Tour.MonTour && joueur.gameObject.name.CompareTo("joueur2") == 0 && caseTouche.Id >= 7 && caseTouche.Id < 14)
                                            {
                                                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 2)
                                                    ((JoueurOn)joueur).PhotonView.RPC("jouerMatch", RpcTarget.AllBuffered, caseTouche.Id);
                                            }
                                            else if (joueur.Tour == Tour.MonTour && joueur.gameObject.name.CompareTo("joueur1") == 0 && caseTouche.Id < 7)
                                            {
                                                if (PlayerPrefs.GetInt("numPositionMatchEnCours") == 1)
                                                    ((JoueurOn)joueur).PhotonView.RPC("jouerMatch", RpcTarget.AllBuffered, caseTouche.Id);
                                            }


                                        }
                                        else if (Fonctions.sceneActuelle("SceneMatch1vs1"))
                                        {
                                            if (joueur.Tour == Tour.MonTour && joueur.gameObject.name.CompareTo("joueur1") == 0 && caseTouche.Id < 7)
                                            {
                                                joueur.jouerMatch(caseTouche);
                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if (stopPosition.y > startPosition.y)
                                {
                                    //Debug.Log("Swipe en haut");
                                }
                                else
                                {
                                    //Debug.Log("Swipe en bas");
                                }
                            }
                        }
                        else
                        {
                            //Debug.Log("Tap");

                        }
                        toucheJouer = false;
                    }
                }

                if (Input.GetKeyDown("q"))
                {
                    Debug.Log("J'ai joué");
                    ((JoueurOn)joueur).PhotonView.RPC("jouerMatch", RpcTarget.AllBuffered, 0);
                }
                if (Input.GetKeyDown("w"))
                {
                    Debug.Log("J'ai joué");
                    ((JoueurOn)joueur).PhotonView.RPC("jouerMatch", RpcTarget.AllBuffered, 7);
                }
            }
        }
    }
}
