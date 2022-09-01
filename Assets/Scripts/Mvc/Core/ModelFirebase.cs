using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;

namespace Mvc.Core
{
    public class ModelFirebase : MonoBehaviour, IModel
    {
        //[SerializeField] protected static string table;
        [SerializeField] protected string msgSuccess;
        [SerializeField] protected string msgFailed;
        [SerializeField] protected DatabaseReference refe;

        public string MsgSuccess { get => msgSuccess; set => msgSuccess = value; }
        public string MsgFailed { get => msgFailed; set => msgFailed = value; }
        public string table()
        {
            List<string> classeJoueur = new List<string>() { "joueurOn", "joueurConnecte", "joueurNonConnecte" };
            List<string> classeMatch = new List<string>() { "matchEnLigne" };

            if (classeJoueur.Contains(this.name))
            {
                return "songo_joueur_online";
            }
            else if (classeMatch.Contains(this.name))
            {
                return "songo_match_en_ligne";
            }
            return null;
        }

        //public static Table { get => table; set => table = value; }

        public void insert()
        {

        }
        public void update(string cheminAttribut, Dictionary<string, object> cleValeur)
        {
            Debug.Log("Le chemin est : " + cheminAttribut);
            refe = FirebaseDatabase.DefaultInstance.RootReference;
            Dictionary<string, object> childUpdates = new Dictionary<string, object>();
            childUpdates[cheminAttribut] = cleValeur;
            refe.UpdateChildrenAsync(childUpdates).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log(this.msgSuccess);
                    refe = null;
                }
                else
                {
                    Debug.Log(this.msgFailed);
                    refe = null;
                }
            });

        }
        public void delete(string id)
        {

        }
        public void selectAll()
        {

        }
        public void selectById(string id)
        {

        }
        
        public void select()
        {

        }
        public void selectWhere(Query requete){

        }
    }
}