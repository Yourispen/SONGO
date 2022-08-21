using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mvc.Core;

namespace Mvc.Models
{
    public class ConnexionCompte : ModelFirebase
    {
        [SerializeField] TypeConnexionCompte typeConnexionCompte;

        [SerializeField] private GameObject fbAuthPrefab;
        [SerializeField] private FacebookAuth fbAuth;
        [SerializeField] private GameObject joueurOnPrefab;
        [SerializeField] private JoueurOn joueurOn;

        void OnEnable()
        {
            joueurOn = Fonctions.instancierObjet(joueurOnPrefab).GetComponent<JoueurOn>();
            joueurOn.ConnexionCompte=this;
            fbAuth = Fonctions.instancierObjet(fbAuthPrefab).GetComponent<FacebookAuth>();
            fbAuth.ConnexionCompte=this;
        }
        public ConnexionCompte()
        {
        }

        //[SerializeField] private FacebookAuth fbAuth;

        public TypeConnexionCompte TypeConnexionCompte { get => typeConnexionCompte; set => typeConnexionCompte = value; }
        public JoueurOn JoueurOn { get => joueurOn; set => joueurOn = value; }

        //public TypeConnexionCompte TypeConnexionCompte1 { get => typeConnexionCompte; set => typeConnexionCompte = value; }

        public void connexionFacebook()
        {
            fbAuth.loginBtnForFB();
        }
        public void connexionGoogle()
        {

        }
    }
}
