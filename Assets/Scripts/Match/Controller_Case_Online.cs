using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Case_Online : Script_Controller_Case
{
    #region Variables

    [Header("scripts")]
    //pour avoir accès aux variables et aux fonctions publiques de la classe Controller_Match_1vs1
    public Controller_Match_Online controlleur_match;

    public Controller_Scene_Match controlleur_scene;
    //faire clignoter la case
    public Couleur_Change_Online couleur_change;

    //case suivante
    public Controller_Case_Online case_suivante;
    //case précédante
    public Controller_Case_Online case_precedante;
    //case de début de l'adversaire
    public Controller_Case_Online case_debut_adversaire;
    //grande case du joueur 2
    public Controller_Case_Online grande_case_adversaire;
    //grande case du joueur 2
    public Controller_Case_Online grande_case_joueur;

    public GameManager gameManager;

    int numero_joueur;

    #endregion

    #region Fonctions Principales Unity
    private void Start()
    {
        numero_joueur = gameManager.numero_joueur;
        couleur_de_initiale = gameObject.GetComponent<Renderer>().material;
        compteur_pions = 0;
    }

    private void Update()
    {
        if (controlleur_match.en_deplacement || controlleur_scene.en_pause)
            return;
        if (GameObject.Find("Adversaire") != null)
        {
            adversaire_joue();
        }
        if (Input.GetKeyDown(bouton))
        {
            if (joueur.recupere_numero() == numero_joueur && joueur.mon_tour)
            {
                GameObject.Find("Joueur").GetComponent<PlayerScript>().jouer(numero_case);
                //fixe la case de départ
                controlleur_match.numero_case_depart = numero_case;
                jouer();
            }
        }
        

        if (Input.touchCount >= 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //recupère la position du doigt
                position_debut_doigt = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10);
                //convertis la position du doigt
                position_debut_doigt_convertie = Camera.main.ScreenToWorldPoint(position_debut_doigt);
                toucher_case = toucher(position_debut_doigt_convertie);

                //initialise le temps du debut du toucher
                temps_depart = Time.timeSinceLevelLoad;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                //si je ne touche pas la case
                if (!toucher_case || controlleur_match.en_deplacement)
                    return;
                //prends le temps actuel du toucher
                temps_actuel = Time.timeSinceLevelLoad;
                //si je maintiens le doigt sur l'ecran
                if (temps_actuel >= temps_depart + 0.3f)
                {
                    controlleur_match.peut_jouer = false;
                    if (controlleur_match.peut_compter)
                    {
                        if (compteur_pions <= pions_case.Count)
                        {
                            controlleur_scene.afficher_nombre_de_pions(compteur_pions.ToString());
                            compteur_pions += 1;
                        }

                    }
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //remet la couleur initiale
                gameObject.GetComponent<Renderer>().material = couleur_de_initiale;

                compteur_pions = 0;

                //reinitialise le comptage de pions
                controlleur_scene.afficher_nombre_de_pions("");

                if (controlleur_match.match_fini)
                    return;
                if (numero_case != 15 && numero_case != 16)
                {

                    //si je suis la case touchée et il n'y a pas de pions en déplacement de pions et c'est mon tour
                    if (toucher_case && !controlleur_match.en_deplacement && joueur.mon_tour)
                    {
                        toucher_case = false;
                        if (controlleur_match.peut_jouer)
                        {
                            //recupère la position du doigt
                            position_fin_doigt = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10);
                            //convertis la position du doigt
                            position_fin_doigt_convertie = Camera.main.ScreenToWorldPoint(position_fin_doigt);

                            if (joueur.recupere_numero() == 1)
                            {
                                if ((position_debut_doigt_convertie.z + vecteur) < position_fin_doigt_convertie.z)
                                {
                                    if (joueur.recupere_numero() == numero_joueur && joueur.mon_tour)
                                    {
                                        GameObject.Find("Joueur").GetComponent<PlayerScript>().jouer(numero_case);
                                        //fixe la case de départ
                                        controlleur_match.numero_case_depart = numero_case;
                                        jouer();
                                    }

                                }
                            }
                            else if (joueur.recupere_numero() == 2)
                            {
                                if ((position_debut_doigt_convertie.z + vecteur) > position_fin_doigt_convertie.z)
                                {
                                    if (joueur.recupere_numero() == numero_joueur && joueur.mon_tour)
                                    {
                                        GameObject.Find("Joueur").GetComponent<PlayerScript>().jouer(numero_case);
                                        //fixe la case de départ
                                        controlleur_match.numero_case_depart = numero_case;
                                        jouer();
                                    }
                                }
                            }
                        }
                        else
                        {
                            controlleur_match.peut_jouer = true;
                        }
                    }
                }
                else if (numero_case == 15 || numero_case == 16)
                {
                    controlleur_match.peut_jouer = true;
                }

            }
        }
    }

    #endregion

    #region Fonctions voids

    private void jouer()
    {
        if (!controlleur_match.peut_jouer)
            return;
        
        //recupère le numéro du joueur actuel
        int numero_joueur = joueur.recupere_numero();
        int numero_adversaire = adversaire.recupere_numero();
        //si le nombre de pions du joueur adverse est nul et si je ne peux pas transmettre des pions à l'adversaire
        if (controlleur_match.nombre_de_pions_null(numero_adversaire, 14) && !controlleur_match.peut_transmettre_des_pions(numero_joueur))
        {
            //victoire du joueur
            controlleur_match.fin_du_match(joueur, true);
            return;
        }
        //si le nombre de pions du joueur adverse est nul et si je peux transmettre des pions à l'adversaire
        if (controlleur_match.nombre_de_pions_null(numero_adversaire, 14) && controlleur_match.peut_transmettre_des_pions(numero_joueur))
        {
            //la case actuelle ne peut pas transmettre des pions à l'adversaire
            if (pions_case.Count <= (7 - numero_case))
            {
                //alors on rejoue le tour
                controlleur_match.rejoueur_le_tour(numero_joueur);
                return;
            }
        }
        //si le nombre de pions du joueur actuelle est nul et si je ne peux pas transmettre des pions à l'adversaire
        if (numero_case == 7 && (controlleur_match.nombre_de_pions_null(numero_joueur, 6) && numero_joueur == 1 || controlleur_match.nombre_de_pions_null(numero_joueur, 13) && numero_joueur == 2))
        {
            if (pions_case.Count == 1)
            {
                controlleur_match.en_deplacement = true;
                //fait clignoter la case
                couleur_change.GetComponent<Couleur_Change_Online>().enabled = true;
                //faire une copie
                List<GameObject> _pions_case = new List<GameObject>(pions_case);
                //c'est la grande case du joueur actuelle qui mange
                grande_case_joueur.manger_pions(_pions_case);
                pions_case.Clear();
                //tour suivant
                controlleur_match.tour_suivant(numero_joueur);
                return;
            }
        }
        //la case actuelle ne contient pas de pions
        if (pions_case.Count == 0)
        {
            //alors on rejoue le tour
            controlleur_match.rejoueur_le_tour(numero_joueur);
            return;
        }
        if (pions_case.Count == 1)
        {
            if (numero_case == 7)
            {
                //alors on rejoue le tour
                controlleur_match.rejoueur_le_tour(numero_joueur);
                return;
            }
            else
            {
                controlleur_match.en_deplacement = true;
                //fait clignoter la case
                couleur_change.GetComponent<Couleur_Change_Online>().enabled = true;
                //faire une copie
                List<GameObject> _pions_case = new List<GameObject>(pions_case);

                case_suivante.suivante(_pions_case, 0);

                pions_case.Clear();
                return;
            }
        }
        
        StartCoroutine("mettre_dans_la_caisse");
    }

    //ajoute le pion et appelle la case suivante
    public void suivante(List<GameObject> _pions, int compteur_pion)
    {
        //copier la liste
        List<GameObject> pions = new List<GameObject>(_pions);

        //si la case actuelle est la case de départ
        if (compteur_pion >= 13 && joueur.mon_tour)
        {
            if (pions.Count == 1)
            {
                //manger
                grande_case_joueur.manger_pions(pions);
                //tour suivant
                controlleur_match.tour_suivant(joueur.recupere_numero());
            }
            else if (pions.Count == 0)
            {
                case_precedante.precedant();
            }
            else if (pions.Count > 1)
            {
                StartCoroutine(case_suivante_avec_une_pause(case_debut_adversaire, pions, compteur_pion, true));
            }

        }
        else
        {
            //si la liste des pions contient encore des pions
            if (pions.Count > 0)
            {
                StartCoroutine(case_suivante_avec_une_pause(case_suivante, pions, compteur_pion + 1, false));

            }
            //si la liste des pions ne contient plus de pions
            else
            {
                //si c'est le tour du joueur actuel
                if (joueur.mon_tour)
                {
                    //si c'est la case 1, alors active la case 14
                    if (numero_case == 1)
                    {
                        case_precedante.precedant();
                    }
                    else
                    {
                        //tour suivant
                        controlleur_match.tour_suivant(joueur.recupere_numero());
                    }
                }
                //si c'est le tour de l'adversaire
                else
                {
                    if (numero_case == 2)
                    {
                        //tour suivant
                        controlleur_match.tour_suivant(adversaire.recupere_numero());
                    }
                    else
                    {
                        case_precedante.precedant();
                    }
                }

            }
        }

    }

    //enlève les pions qui sont dans cette case, puis les mettre dans la grande case et enfin appelle la case précécante
    public void precedant()
    {
        //si c'est le tour de l'adversaire
        if (adversaire.mon_tour)
        {
            //si la c'est la case 7 ou la case 14
            if (numero_case == 7)
            {
                //si c'est possible de manger
                if (controlleur_match.peut_manger(joueur.recupere_numero()))
                {
                    if (pions_case.Count > 1 && pions_case.Count < 5)
                    {
                        List<GameObject> _pions_case = new List<GameObject>(pions_case);
                        StartCoroutine(case_precedante_avec_une_pause(_pions_case));
                        pions_case.Clear();
                    }
                    else
                    {
                        //tour suivant
                        controlleur_match.tour_suivant(adversaire.recupere_numero());
                    }
                }
                else
                {
                    //tour suivant
                    controlleur_match.tour_suivant(adversaire.recupere_numero());
                }
            }
            else
            {
                //si c'est possible de manger
                if (pions_case.Count > 1 && pions_case.Count < 5)
                {
                    List<GameObject> _pions_case = new List<GameObject>(pions_case);
                    StartCoroutine(case_precedante_avec_une_pause(_pions_case));
                    pions_case.Clear();
                }
                else
                {
                    //tour suivant
                    controlleur_match.tour_suivant(adversaire.recupere_numero());
                }
            }
        }
        //si ce n'est pas le tour de l'adversaire
        else
        {
            controlleur_match.tour_suivant(joueur.recupere_numero());
        }
    }

    public void adversaire_joue()
    {
        if ((numero_joueur == 2 && joueur.recupere_numero() == 1 && joueur.mon_tour) || (numero_joueur == 1 && joueur.recupere_numero() == 2 && joueur.mon_tour))
        {
            if (GameObject.Find("Adversaire").GetComponent<PlayerScript>().case_de_depart == numero_case)
            {
                controlleur_match.numero_case_depart = numero_case;
                jouer();
            }
        }
    }

    #endregion

    #region Fonctions Coroutines

    //mettre dans la caisse avant de déplacer les pions
    public IEnumerator mettre_dans_la_caisse()
    {
        controlleur_match.en_deplacement = true;
        //fait clignoter la case
        couleur_change.GetComponent<Couleur_Change_Online>().enabled = true;

        foreach (GameObject pion in pions_case)
        {
            pion.transform.position = caisse.transform.position + new Vector3(Random.Range(-1.9f, 1.9f), -0.5f, Random.Range(-1.9f, 1.9f));
            yield return new WaitForSeconds(temps_depot_caisse);

        }
        yield return new WaitForSeconds(temps_depot_case);

        //faire une copie
        List<GameObject> _pions_case = new List<GameObject>(pions_case);
        //appelle la case suivante
        case_suivante.suivante(_pions_case, 0);
        //vide la liste des pions
        pions_case.Clear();
    }

    //appelle la case suivante après une pause
    public IEnumerator case_suivante_avec_une_pause(Controller_Case_Online _case_suivante, List<GameObject> _pions, int compteur_pion, bool case_debut)
    {
        //faire une copie de la liste
        List<GameObject> pions = new List<GameObject>(_pions);

        //si c'est la case suivante n'est pas une case de début
        if (!case_debut)
        {
            //la case actuelle recupère un pion, ensuite on retranche le pion de la case de départ, enfin case suivante.
            ajouter_pion(pions[0]);
            pions.Remove(pions[0]);
        }
        yield return new WaitForSeconds(temps_depot_case);
        _case_suivante.suivante(pions, compteur_pion);
    }

    public IEnumerator case_precedante_avec_une_pause(List<GameObject> pions)
    {
        yield return new WaitForSeconds(temps_depot_caisse);
        grande_case_adversaire.manger_pions(pions);
        yield return new WaitForSeconds(temps_depot_case);
        case_precedante.precedant();
    }

    #endregion

}
