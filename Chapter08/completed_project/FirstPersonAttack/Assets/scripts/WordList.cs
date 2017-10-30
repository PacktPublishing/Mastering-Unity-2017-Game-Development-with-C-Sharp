//------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//------------------------------------
public class WordList : MonoBehaviour
{
	//------------------------------------
	//Text asset featuring word list
	public TextAsset FileWordList = null;
	public string[] Words;

	//Members for Singleton
	public static WordList ThisInstance
	{
		get
		{
			//Get or create singleton instance
			if (m_WL == null)
			{
				GameObject GO = new GameObject ("WordList");
				ThisInstance = GO.AddComponent<WordList> ();
			}

			return m_WL;
		}
		set 
		{
			//If not null then we already have instance
			if (m_WL != null) 
			{
				//If different, then remove duplicate immediately
				if(m_WL.GetInstanceID() != value.GetInstanceID())
					DestroyImmediate (value.gameObject);

				return;
			}

			//If new, then create new sinleton instance
			m_WL = value;
			DontDestroyOnLoad (m_WL.gameObject);
		}
	}
	private static WordList m_WL = null;
	//------------------------------------
	// Use this for initialization
	void Awake () 
	{
		//Set singleton instance
		ThisInstance = this;

		//Now load word list, if available
		if (FileWordList == null)
			FileWordList = (TextAsset) Resources.Load("WordList");
		
		Words = FileWordList.text.Split (new[] { "\r\n" }, System.StringSplitOptions.None);
	
	}
	//------------------------------------
	//Returns a random word from the word list
	public string GetRandomWord()
	{
		return Words[Random.Range(0, Words.Length)].ToLower();
	}
	//------------------------------------
	//Compares two strings and returns the extent of a match
	//EG: s1="hello" and s2="helicopter" the result = "hel"
	public static string CompareWords(string s1, string s2)
	{
		//Build resulting string
		string Result = string.Empty;

		//Get shortest length
		int ShortestLength = Mathf.Min(s1.Length, s2.Length);

		//Check for string match
		for (int i = 0; i < ShortestLength; i++)
		{
			if (s1 [i] != s2 [i])
				return Result;

			Result += s1[i];
		}

		//Output result
		return Result;
	}
	//------------------------------------
}
//------------------------------------