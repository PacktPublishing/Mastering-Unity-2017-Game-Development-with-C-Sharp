//------------------------------------
using UnityEngine;
using System.Collections;
//------------------------------------
public class UIHealth : MonoBehaviour
{
	private Health PlayerHealth = null;
	private RectTransform ThisTransform = null;

	// Use this for initialization
	void Awake () 
	{
		GameObject GO = GameObject.FindGameObjectWithTag ("Player");
		PlayerHealth = GO.GetComponent<Health> ();
		ThisTransform = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	public void UpdateHealth () 
	{
		//Update player health
		ThisTransform.sizeDelta = new Vector2(PlayerHealth.Value, ThisTransform.sizeDelta.y);
	}
}
//------------------------------------