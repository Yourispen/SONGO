using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilisateur
{
    [SerializeField] private string idUser;
    [SerializeField] private string userName;
    [SerializeField] private string dateInscription;
    [SerializeField] private string heureInscription;
    [SerializeField] private string email;
    public Utilisateur(string idUser, string userName, string dateInscription = "", string heureInscription = "", string email="")
    {
        this.idUser = idUser;
        this.userName = userName;
        this.dateInscription = dateInscription;
        this.heureInscription = heureInscription;
        this.email = email;
    }
    public Utilisateur()
    {

    }

    public string UserName { get => userName; set => userName = value; }
    public string DateInscription { get => dateInscription; set => dateInscription = value; }
    public string HeureInscription { get => heureInscription; set => heureInscription = value; }
    public string Email { get => email; set => email = value; }
    public string IdUser { get => idUser; set => idUser = value; }
}
