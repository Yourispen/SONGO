using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

public class TestIp : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Nom du pays : " + recupAddIpFonction());
        //StartCoroutine(recupAddIp());
    }
    public string recupAddIpFonction()
    {
        string nomPays = "";
        try
        {
            WebRequest request = WebRequest.Create("https://www.geodatatool.com/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                while (!stream.EndOfStream)
                {
                    if (stream.ReadLine().Contains("gif"))
                    {
                        nomPays = stream.ReadLine();
                        nomPays = Regex.Replace(nomPays, @"\s", "");
                    }
                }
            }
            return nomPays;
        }
        catch (System.Exception)
        {

            Debug.Log("Pas de connexion internet");
            return nomPays;
        }

    }
}
