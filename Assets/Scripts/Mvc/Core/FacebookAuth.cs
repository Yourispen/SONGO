using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;
using System;
using Mvc.Controllers;
using Mvc.Models;

namespace Mvc.Core
{
    public class FacebookAuth : MonoBehaviour
    {
        //string accessToken = "EAAKgZACq3EOIBAMXaLX9BKBuMSVIdSBZBlHSZATtp8nfV7EZCcG5VVK8MImFyjxPn24iQhGFM5B8ZB71AxhjIBWQ3sSEXuuZBHu94Wh515BFN1DstYJwYC0GX6ET6X1sfCqj0Oo9tQW3qZBQ2PfZAK7K5HWfHiZCc9ZAxZC3NNObJm0yeZC0NavyZCfWC";
        private FirebaseAuth auth;
        private FirebaseUser user = null;

        [SerializeField] private GameObject joueurOnControllerPrefab;
        [SerializeField] private JoueurOnController joueurOnController;
        [SerializeField] private ConnexionCompte connexionCompte;

        public static string msgConnexion = "Vous êtes connecté avec Facebook";

        public FirebaseUser User { get => user; set => user = value; }
        public ConnexionCompte ConnexionCompte { get => connexionCompte; set => connexionCompte = value; }

        // Start function from Unity's MonoBehavior
        void Start()
        {


            if (!FB.IsInitialized)
            {
                FB.Init(initCallback, onHideUnity);
            }
            else
            {
                // Already initialized
                FB.ActivateApp();
            }
        }
        void Update()
        {
            if (user != null)
            {
                setDataJoueur();
            }
        }

        private void initCallback()
        {
            if (FB.IsInitialized)
            {
                // Signal an app activation App Event
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            }
            else
            {
                Debug.Log("Something went wrong to Initialize the Facebook SDK");
            }
        }

        private void onHideUnity(bool isGameScreenShown)
        {
            if (!isGameScreenShown)
            {
                // Pause the game - we will need to hide
                Time.timeScale = 0;
            }
            else
            {
                // Resume the game - we're getting focus again
                Time.timeScale = 1;
            }
        }
        public void loginBtnForFB()
        {
            // Permission option list      https://developers.facebook.com/docs/facebook-login/permissions/
            var perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
        private void AuthCallback(ILoginResult result)
        {
            if (FB.IsLoggedIn)
            {
                // AccessToken class will have session details
                var aToken = AccessToken.CurrentAccessToken;
                // Print current access token's User ID
                Debug.Log(aToken.UserId);
                // Print current access token's granted permissions
                foreach (string perm in aToken.Permissions)
                {
                    Debug.Log(perm);
                }
                loginviaFirebaseFacebook(aToken.TokenString);

            }
            else
            {
                Debug.Log("User cancelled login");
            }
        }

        private void loginviaFirebaseFacebook(string accessToken)
        {
            auth = FirebaseAuth.DefaultInstance;
            Credential credential = FacebookAuthProvider.GetCredential(accessToken.ToString());
            auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                user = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    user.DisplayName, user.UserId);

            });
        }

        public void setDataJoueur()
        {
            //user = null;
            ///PlayerPrefs.SetString("userName", user.DisplayName);
            //name = user.DisplayName;
            //PlayerPrefs.SetString("email", user.Email);
            //email = user.Email;
            //System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            //uid = user.UserId;
            PlayerPrefs.SetString("id", user.UserId);
            //Debug.Log(PlayerPrefs.GetString("id")); return;
            PlayerPrefs.SetString("email", user.Email);
            PlayerPrefs.SetString("dateInscription", DateTime.Now.ToString("yyyy'-'MM'-'dd"));
            PlayerPrefs.SetString("heureInscription", DateTime.Now.ToString("HH:mm"));
            PlayerPrefs.SetInt("idConnexionCompte", 1);
            PlayerPrefs.SetInt("idNiveau", 1);
            user = null;
            joueurOnController = Fonctions.instancierObjet(joueurOnControllerPrefab).GetComponent<JoueurOnController>();
            joueurOnController.SceneController=connexionCompte.ConnexionCompteController.SceneController;
            joueurOnController.recupereJoueurConnecte();

        }

    }
}