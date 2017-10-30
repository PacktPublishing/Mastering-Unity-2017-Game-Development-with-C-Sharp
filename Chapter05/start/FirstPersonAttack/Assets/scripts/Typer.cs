//------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//------------------------------------
public class Typer : MonoBehaviour
{
	//Reference to typed word
	public static string TypedWord = string.Empty;

	//Reference to all enemies in level
	private AIEnemy[] Enemies;

	//Text object for showing type
	private Text TyperText = null;

	//Reference to audio source component
	private AudioSource ThisAS = null;

	//Typing changed event
	public UnityEvent OnTypingChanged;

	//Time elapsed since last reset
	public static float ElapsedTime = 0.0f;

	//Record words per second
	public static float RecordLettersPerSecond = 2f;
	//------------------------------------
	//Collection of combat sounds
	public AudioClip[] CombatSounds;
	// Use this for initialization
	void Awake () 
	{
		//Get all enemies
		Enemies = Object.FindObjectsOfType<AIEnemy>();

		//Get audio source
		ThisAS = GetComponent<AudioSource>();
		ThisAS.clip = CombatSounds[0];

		TyperText = GetComponentInChildren<Text>();
	}
	//------------------------------------
	// Update is called once per frame
	void Update ()
	{
		//Update time
		ElapsedTime += Time.deltaTime;

		//Update types string
		if (Input.inputString.Length > 0) 
		{
			TypedWord += Input.inputString.ToLower();
			UpdateTyping ();
		}
	}
	//------------------------------------
	//Update enemy type event
	private void UpdateTyping()
	{
		//Invoke typing events for enemies
		foreach (AIEnemy E in Enemies)
		{
			E.OnTypingChanged.Invoke ();
		}

		//Update GUI Typer
		OnTypingChanged.Invoke();

		//Check for entry reset (are there no possible matches?)
		foreach (AIEnemy E in Enemies)
		{
			if (TypedWord.Length-E.MatchedWord.Length <= 0)
				return;
		}

		Reset ();
	}
	//------------------------------------
	public void Reset()
	{
		//Reset typing
		TypedWord = string.Empty;

		//Reset time
		ElapsedTime = 0.0f;

		//Invoke typing events for enemies
		foreach (AIEnemy E in Enemies)
		{
			E.OnTypingChanged.Invoke ();
		}
	}
	//------------------------------------
	// Update is called once per frame
	public void UpdateTyperText() 
	{
		TyperText.text = Input.inputString;
		ThisAS.clip = CombatSounds [Random.Range (0, CombatSounds.Length)];
	}
	//------------------------------------
}
//------------------------------------