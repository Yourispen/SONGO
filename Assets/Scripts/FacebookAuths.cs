using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Facebook.Unity;
using Firebase.Auth;
using System;
using Mvc.Core;

public class FacebookAuths : MonoBehaviour
{
    //string accessToken = "EAAKgZACq3EOIBAAwbuNrYIm74ydcD3kEZADMNQKXh8RZB9hu5CE9iOPB8aHDTLqFGXDwsv4WZAvpBeK2mbEhFHpWVbzzTrMCUK3g4q6NdQrJds3OgSSF07UMXURYlmTGSntJzxamUossr2jspdLrwr1QJdzacjgcHacDvWxkqfIt0aXN22f2";
    private FirebaseAuth auth;
    private Model model;// = new Model();
    private FirebaseUser user;

    public Model Model { get => model; set => model = value; }
    public FirebaseUser User { get => user; set => user = value; }

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

    public void setProfilPlayer()
    {
        Debug.Log("Ce joueur n'existe pas");
        //string email = "", name, uid;
        if (user != null)
        {
            PlayerPrefs.SetString("userName", user.DisplayName);
            //name = user.DisplayName;
            PlayerPrefs.SetString("email", user.Email);
            //email = user.Email;
            //System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            //uid = user.UserId;
            PlayerPrefs.SetString("id", user.UserId);
            PlayerPrefs.SetString("dateInscription", DateTime.Now.ToShortDateString());
            PlayerPrefs.SetString("heureInscription", DateTime.Now.ToShortTimeString());
            //model.insert();

        }
        //Debug.Log("email : "+email);
    }
    public void getProfilPlayer(List<string> datas)
    {
        if (datas.Count != 0)
        {
            //PlayerPrefs.SetString("userName", user.DisplayName);
            Utilisateur user = JsonUtility.FromJson<Utilisateur>(datas[0]);
            Debug.Log(user.Email);
            PlayerPrefs.SetString("userName", user.UserName);
            PlayerPrefs.SetString("email", user.Email);
            PlayerPrefs.SetString("idUser", user.IdUser);
            PlayerPrefs.SetString("dateInscription", user.DateInscription);
            PlayerPrefs.SetString("heureInscription", user.HeureInscription);
            PlayerPrefs.SetString("id", this.user.UserId);
            Debug.Log("Ce joueur existe d�j�");
        }
        else
        {
            setProfilPlayer();
        }
    }

}
