//------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//------------------------------------
public class GameTrigger : MonoBehaviour 
{
	public UnityEvent OnTriggerEntered;
	//------------------------------------
	// Update is called once per frame
	void OnTriggerEnter (Collider Other) 
	{
		//If not player then exit
		if(!Other.CompareTag("Player")) return;

		OnTriggerEntered.Invoke ();
	}
	//------------------------------------
}
//------------------------------------