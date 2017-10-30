using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//------------------------------------
public class KeyPressEvent : MonoBehaviour 
{
	public KeyCode Key = KeyCode.A;

	public UnityEvent OnKeyPress;

	void Update () 
	{
		if (Input.GetKeyDown (Key)) 
		{
			OnKeyPress.Invoke ();
		}
	}
}
//------------------------------------