using Firebase;
//using Firebase.Analytics;
using Firebase.Database;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Firebase.Extensions;
using UnityEditor;

namespace Mvc.Core
{
    public class DatabaseRealtime : MonoBehaviour
    {
        [SerializeField] private List<string> liste_ids = new List<string>();
        [SerializeField] private string id;
        [SerializeField] private List<string> resultat;
        [SerializeField] private Dictionary<string, string> listePlayers;

        [SerializeField]
        private StatutDatabase statut;

        Model model;

        public List<string> Resultat { get => resultat; }
        public StatutDatabase Statut { get => statut; set => statut = value; }

        public static DatabaseReference reference() {
            DatabaseReference refe = FirebaseDatabase.DefaultInstance.RootReference;
            return refe;
        }
        void Start()
        {
            statut = 0;
            // Get the root reference location of the database.
            //reference = FirebaseDatabase.DefaultInstance.RootReference;

        }

        public int recuperer_donnees()
        {
            //.Child(ids)
            //var data = reference.Child("users").GetValueAsync();
            Debug.Log(300);
            return 200;
        }

        public int[] saisir_donnees()
        {
            return new int[1];
        }

        public List<string> executeSelect(string table, string id = "")
        {

            //var i = StartCoroutine(test(table));
            //Task<DataSnapshot> task= select(table);
            //DataSnapshot snapshot = task.Result;
            /*foreach (var id in snapshot.Children)
            {
                string _id = id.Key.ToString();
                //Debug.Log(_id);
                resultat.Add(_id);
            }*/
            Debug.Log(select(table).IsCompleted);
            print("Ok");
            return resultat;
        }

        IEnumerator test(string table, string id = "")
        {
            //resultat = new List<string>();
            DatabaseReference refe = FirebaseDatabase.DefaultInstance.RootReference;
            Task task;
            //int a = Progress.Start("Running one task");
            //refe.Child(table).Child("0000001").GetValueAsync()
            task = refe.Child(table).Child("0000001").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("les donn�es ont �t� compl�tement r�cup�r�s dans la base de donn�es.");
                    DataSnapshot snapshot = task.Result;
                    //listePlayers = snapshot
                    //Debug.Log(snapshot.GetRawJsonValue());
                    //listePlayers= (Dictionary<string, string>)JsonUtility.FromJson<System.Object>(snapshot.GetRawJsonValue());
                    //Debug.Log(listePlayers.Keys);
                    ///result.Add(snapshot.GetRawJsonValue());
                    snapshot.GetType();
                    foreach (var id in snapshot.Children)
                    {
                        string _id = id.Key.ToString();
                        //Debug.Log(_id);
                        resultat.Add(_id);
                    }
                    statut = StatutDatabase.Succes;
                }
                else
                {
                    Debug.Log("les donn�es n'ont pas �t� r�cup�r�s dans la base de donn�es.");
                    return;
                }
            });
            yield return new WaitUntil(()=>task.IsCompleted);
            Debug.Log(resultat.Count);

        }

        async Task<DataSnapshot> select(string table, string id = "")
        {
            DatabaseReference refe = FirebaseDatabase.DefaultInstance.RootReference;
            //return await refe.Child(table).Child("0000001").GetValueAsync();
            return await refe.Child(table).Child("0000001").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("les donn�es ont �t� compl�tement r�cup�r�s dans la base de donn�es.");
                    //DataSnapshot snapshot = task.Result;
                    //listePlayers = snapshot
                    //Debug.Log(snapshot.GetRawJsonValue());
                    //listePlayers= (Dictionary<string, string>)JsonUtility.FromJson<System.Object>(snapshot.GetRawJsonValue());
                    //Debug.Log(listePlayers.Keys);
                    ///result.Add(snapshot.GetRawJsonValue());
                    /*snapshot.GetType();
                    foreach (var id in snapshot.Children)
                    {
                        string _id = id.Key.ToString();
                        //Debug.Log(_id);
                        resultat.Add(_id);
                    }
                    statut = StatutDatabase.Succes;*/
                    return task.Result;
                }
                else
                {
                    Debug.Log("les donn�es n'ont pas �t� r�cup�r�s dans la base de donn�es.");
                    return task.Result;
                }
            });
            //Debug.Log(resultat.Count);
        }

        public void executeSelected(string table, string id = "")
        {
            resultat = new List<string>();
            DatabaseReference refe = FirebaseDatabase.DefaultInstance.RootReference;
            var data = FirebaseDatabase.DefaultInstance.GetReference(table);
            Debug.Log(data);
            Task task;
            //refe.Child(table).EqualTo("");
            task=refe.Child(table).Child("0000001").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                 if (task.IsCompleted)
                 {
                     Debug.Log("les donn�es ont �t� compl�tement r�cup�r�s dans la base de donn�es.");
                     DataSnapshot snapshot = task.Result;
                    //listePlayers = snapshot
                     //Debug.Log(snapshot.GetRawJsonValue());
                     //listePlayers= (Dictionary<string, string>)JsonUtility.FromJson<System.Object>(snapshot.GetRawJsonValue());
                     //Debug.Log(listePlayers.Keys);
                     ///result.Add(snapshot.GetRawJsonValue());
                     snapshot.GetType();
                     foreach (var id in snapshot.Children)
                     {
                         string _id = id.Key.ToString();
                         //Debug.Log(_id);
                         resultat.Add(_id);
                     }
                     statut = StatutDatabase.Succes;
                 }
                 else
                 {
                     Debug.Log("les donn�es n'ont pas �t� r�cup�r�s dans la base de donn�es.");
                 }
            });
             Debug.Log(resultat.Count);

            if (id.CompareTo("") == 0)
            {
                /* refe.Child(table).GetValueAsync().ContinueWith( task =>
                 {
                     if (task.IsCompleted)
                     {
                         Debug.Log("les donn�es ont �t� compl�tement r�cup�r�s dans la base de donn�es.");
                         DataSnapshot snapshot = task.Result;
                         //Debug.Log(snapshot.GetRawJsonValue());
                         //listePlayers= (Dictionary<string, string>)JsonUtility.FromJson<System.Object>(snapshot.GetRawJsonValue());
                         //Debug.Log(listePlayers.Keys);
                         ///result.Add(snapshot.GetRawJsonValue());
                         snapshot.GetType();
                         foreach (var id in snapshot.Children)
                         {
                             string _id = id.Key.ToString();
                             //Debug.Log(_id);
                             resultat.Add(_id);
                         }
                         statut = StatutDatabase.Succes;
                     }
                     else
                     {
                         Debug.Log("les donn�es n'ont pas �t� r�cup�r�s dans la base de donn�es.");
                     }

                 });*/
            }
            else
            {
                refe.Child(table).Child(id).GetValueAsync().ContinueWith(task =>
               {
                   if (task.IsCompleted)
                   {
                       Debug.Log("les donn�es ont �t� compl�tement r�cup�r�s dans la base de donn�es.");
                       DataSnapshot snapshot = task.Result;
                       string data = snapshot.GetRawJsonValue();
                       if (data != null)
                       {
                           resultat.Add(data);
                       }
                       //Utilisateur user = JsonUtility.FromJson<Utilisateur>(snapshot.GetRawJsonValue());
                       //Debug.Log(user.Email);

                   }
                   else
                   {
                       Debug.Log("les donn�es n'ont pas �t� r�cup�r�s dans la base de donn�es.");
                   }
                   Debug.Log("OK");
                   statut = StatutDatabase.Succes;

               });
            }

        }

        public void executeUpdate()
        {
            string idUser = generer_id();
            //Debug.Log(_id);
            PlayerPrefs.SetString("idUser",idUser);

            Utilisateur new_user = new Utilisateur(idUser, PlayerPrefs.GetString("userName"), PlayerPrefs.GetString("dateInscription"), PlayerPrefs.GetString("heureInscription"), PlayerPrefs.GetString("email"));
            string user_json = JsonUtility.ToJson(new_user);
            reference().Child("user").Child(PlayerPrefs.GetString("id")).SetRawJsonValueAsync(user_json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("les donn�es ont �t� compl�tement ajout�s dans la base de donn�es.");
                }
                else
                {
                    Debug.Log("les donn�es n'ont pas �t� ajout�s dans la base de donn�es.");
                }
            });
        }

        private string generer_id()
        {
            string _id = "";
            for (int i = 1; i <= 9; i++)
            {
                _id += UnityEngine.Random.Range(0, 10).ToString();
                if (i % 3 == 0 && i < 9)
                    _id += "-";
            }
            return _id;
        }
        public void getData()
        {

        }
        public void setData()
        {

        }
    }
}
