using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mvc.Entities
{
    public class SongoJoueurOnline
    {
        [SerializeField] private string surnom;
        [SerializeField] private string dateInscription;
        [SerializeField] private string heureInscription;
        [SerializeField] private string email;
        [SerializeField] private int idConnexionCompte;
        [SerializeField] private int idNiveau;
        [SerializeField] private string role;
        public static string chemin = "/songo_joueur_online/";

        public string Surnom { get => surnom; set => surnom = value; }
        public string DateInscription { get => dateInscription; set => dateInscription = value; }
        public string HeureInscription { get => heureInscription; set => heureInscription = value; }
        public string Email { get => email; set => email = value; }
        public int IdConnexionCompte { get => idConnexionCompte; set => idConnexionCompte = value; }
        public int IdNiveau { get => idNiveau; set => idNiveau = value; }
        public string Role { get => role; set => role = value; }

        public SongoJoueurOnline(string surnom, string dateInscription, string heureInscription, string email, int idConnexionCompte, int idNiveau,string role)
        {
            this.surnom = surnom;
            this.dateInscription = dateInscription;
            this.heureInscription = heureInscription;
            this.email = email;
            this.idConnexionCompte = idConnexionCompte;
            this.idNiveau = idNiveau;
            this.role=role;
        }

        public override string ToString()
        {
            return "Surnom : " + surnom +
            " email : " + email +
            " dateInscription : " + dateInscription +
            " heureInscription : " + heureInscription +
            " idConnexionCompte : " + idConnexionCompte;
        }
    }
}
