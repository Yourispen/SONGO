using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScript : MonoBehaviour
{
    private PhotonView photonView;

    public int case_de_depart;
    //public int numero_de_celui_qui_joue;
   
    public bool abandon;

    [SerializeField] private Controller_Match_Online controller_match;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int numero_joueur_debut;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        abandon = false;

        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (GameObject.Find("Controller_Match_Online") != null)
        {
            controller_match = GameObject.Find("Controller_Match_Online").GetComponent<Controller_Match_Online>();
        }
        numero_joueur_debut = gameManager.numero_joueur;
        if (photonView.IsMine && numero_joueur_debut == 1)
            gameObject.name = "Joueur";
        else if(!photonView.IsMine && numero_joueur_debut == 2)
            gameObject.name = "Adversaire";
        else if (photonView.IsMine && numero_joueur_debut == 2)
            gameObject.name = "Joueur";
        else if(!photonView.IsMine && numero_joueur_debut == 1)
            gameObject.name = "Adversaire";
    }

    // Update is called once per frame
    void Update()
    {
       /* if (numero_joueur_debut == 1)
        {

            if (!photonView.IsMine)
            {
                if (controller_match.joueur_1.mon_tour)
                {
                    tour_suivant(controller_match.numero_case_depart, 1);
                }
            }
            //Debug.Log(case_de_depart);

            if (photonView.IsMine)
            {
                if (controller_match.joueur_2.mon_tour)
                {
                    controller_match.numero_de_celui_qui_joue = joueur;
                    controller_match.numero_case_depart = case_de_depart;
                }
            }

        }
        else if (numero_joueur_debut == 2)
        {

            if (!photonView.IsMine)
            {
                if (controller_match.joueur_2.mon_tour)
                {
                    tour_suivant(controller_match.numero_case_depart, 2);
                }
            }
            //Debug.Log(case_de_depart);

            else
            {
                if (controller_match.joueur_1.mon_tour)
                {
                    controller_match.numero_de_celui_qui_joue = joueur;
                    controller_match.numero_case_depart = case_de_depart;
                }
            }

        }*/
    }

    [PunRPC]
    void RPC_jouer(int numero_case)
    {
        //numero_de_celui_qui_joue = numero_joueur;
        case_de_depart = numero_case;
    }

    [PunRPC]
    void RPC_fin_du_match(bool perdu)
    {
        abandon = perdu;
    }

    public void fin_du_match(bool perdu)
    {
        photonView.RPC("RPC_fin_du_match", RpcTarget.AllBuffered, perdu);
    }

    public void jouer(int case_de_depart)
    {
        photonView.RPC("RPC_jouer", RpcTarget.AllBuffered, case_de_depart);
    }
}
