using UnityEngine;
using TMPro;

[RequireComponent (typeof (TextMeshProUGUI))]
public class TextTyperTMP : MonoBehaviour 
{	
	[Header("Settings")]
	[SerializeField] private float typeSpeed		= 0.1f;
	[SerializeField] private float startDelay 		= 0.5f;
	[SerializeField] private float volumeVariation 	= 0.1f;
	[SerializeField] private bool startOnStart		= false;

	[Header("Components")]
	[SerializeField] private AudioSource mainAudioSource;

	private bool typing;
	private int counter;
	private string textToType;
	private TextMeshProUGUI textComponent;

	private void Awake()
	{
		textComponent = GetComponent<TextMeshProUGUI>();

		if(!mainAudioSource)
		{
			Debug.Log("No AudioSource has been set. Set one if you wish you use audio features.");
		}
		
		counter = 0;
		textToType = textComponent.text;
		textComponent.text = "";
	}

	private void Start()
	{
		if(startOnStart)
		{
            StartTyping();
		}
	}

	public void StartTyping()
	{	
		if(!typing)
		{
			InvokeRepeating("Type", startDelay, typeSpeed);
		}
		else
		{
			Debug.LogWarning(gameObject.name + " : Is already typing!");
		}
	}

	public void StopTyping()
	{
        counter = 0;
        typing = false;
		CancelInvoke("Type");
	}

    public void UpdateText(string newText)
    {   
        StopTyping();
        textComponent.text = "";
        textToType = newText;
        StartTyping();
    }

	public void QuickSkip()
	{
		if(typing)
		{
			StopTyping();
			textComponent.text = textToType;
		}
	}

	private void Type()
	{	
		typing = true;
		textComponent.text = textComponent.text + textToType[counter];
		counter++;

		if(mainAudioSource)
		{	
			mainAudioSource.Play();
			RandomiseVolume();
		}

		if(counter == textToType.Length)
		{	
			typing = false;
			CancelInvoke("Type");
		}
	}

	private void RandomiseVolume()
	{
		mainAudioSource.volume = Random.Range(1 - volumeVariation, volumeVariation + 1);
	}

    public bool IsTyping() { return typing; }
}

/*
Copyright 2019 George Blackwell

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the 
Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR 
ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH 
THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */