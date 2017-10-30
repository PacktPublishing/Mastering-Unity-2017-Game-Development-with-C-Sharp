using UnityEngine;
using System.Collections;

public class UIBonus : MonoBehaviour 
{
	public GameObject[] BonusObjects;

	// Use this for initialization
	void Awake () 
	{
		BonusObjects = GameObject.FindGameObjectsWithTag ("BonusObject");
	}
	
	// Update is called once per frame
	void Update ()
	{
		///Set bonus level
		//Hide/Show all bonus objects
		for (int i = 0; i < BonusObjects.Length; i++) 
		{
			if (i < GameManager.ThisInstance.BonusLevel)
				BonusObjects [i].SetActive (true);
			else
				BonusObjects [i].SetActive (false);
		}
	}
}
