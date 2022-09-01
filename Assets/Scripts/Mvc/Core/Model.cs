using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace Mvc.Core
{
    public class Model : MonoBehaviour
    {
        [SerializeField] protected string table;
        [SerializeField] protected string msgSuccess;
        [SerializeField] protected string msgFailed;

        [SerializeField] protected List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        public const string WEB_URL = "http://localhost/songo_db/";

        /*[SerializeField] private DatabaseRealtime databaseRealtime;//= GameObject.Find("DatabaseRealtime").GetComponent<DatabaseRealtime>();
        //DatabaseRealtime databaseRealtime = new DatabaseRealtime();

        public DatabaseRealtime DatabaseRealtime { get => databaseRealtime; set => databaseRealtime = value; }
        */
        public string Table { get => table; set => table = value; }
        public string MsgSuccess { get => msgSuccess; set => msgSuccess = value; }
        public string MsgFailed { get => msgFailed; set => msgFailed = value; }
        public List<Dictionary<string, string>> Data { get => data; set => data = value; }

        public void delete(string id)
        {
            WWWForm form = new WWWForm();
            form.AddField("table", this.table);
            form.AddField("action", "supprimer");
            form.AddField("id", id);
            StartCoroutine(request(form));
        }

        public void insert()
        {

        }
        public void selectAll()
        {
            WWWForm form = new WWWForm();
            form.AddField("table", this.table);
            form.AddField("action", "lister");
            form.AddField("sous-action", "all");
            //form.AddField("id", PlayerPrefs.GetString("id"));
            StartCoroutine(request(form));
        }

        public void selectById(string id)
        {
            WWWForm form = new WWWForm();
            form.AddField("table", this.table);
            form.AddField("action", "lister");
            form.AddField("sous-action", "one");
            form.AddField("id", id);
            StartCoroutine(request(form));
        }

        public void selectWhere(string[] data)
        {
            
        }

        public void update(string nomColonne, string id,string valeur)
        {
            WWWForm form = new WWWForm();
            form.AddField("table", this.table);
            form.AddField("action", "modifier");
            form.AddField("nomColonne", nomColonne);
            form.AddField("valeur", PlayerPrefs.GetString(nomColonne));
            form.AddField("id", id);
            StartCoroutine(request(form));
        }

        public IEnumerator request(WWWForm form)
        {
            using (UnityWebRequest request = UnityWebRequest.Post(WEB_URL, form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(this.msgFailed + request.ToString());
                }
                else
                {
                    Debug.Log(this.msgSuccess);

                    string json = request.downloadHandler.text;
                    if (json.CompareTo("succes") != 0)
                    {
                        Debug.Log(json);
                        Data = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
                        Debug.Log(Data.Count);
                    }
                }
            }
        }
    }
}
