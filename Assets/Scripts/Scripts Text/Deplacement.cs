using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deplacement : MonoBehaviour
{
	private TMPro.TMP_Text texte_actuel;
	public string texte_saisi;
	private float temps_actuel, temps_depart;

	[Header("Paramètre")]
	[SerializeField] private bool affichage_multiple=true;
	void Awake()
	{
		texte_actuel = GetComponent<TMPro.TMP_Text>();
		texte_saisi = texte_actuel.text;
		texte_actuel.text = "";

		// TODO: add optional delay when to start
		StartCoroutine("PlayText");
	}
	private void Start()
    {
		temps_depart = Time.timeSinceLevelLoad;
	}

    private void Update()
    {
		if (!affichage_multiple)
			return;
		temps_actuel = Time.timeSinceLevelLoad;
		if (temps_actuel >= temps_depart + 5f)
		{
			texte_actuel.text = "";
			StartCoroutine("PlayText");
			temps_depart = temps_actuel;
		}
    }
   
    IEnumerator PlayText()
	{
		string texte = texte_saisi;
		foreach (char c in texte_saisi)
		{
			if (texte_saisi != texte)
			{
				temps_depart = 0;
				yield break;
			}
			texte_actuel.text += c;
			yield return new WaitForSeconds(0.125f);
		}
	}

}
