//------------------------------------
using UnityEngine;
using System.Collections;
//------------------------------------
public class CameraFacing : MonoBehaviour 
{
	//------------------------------------
	//Reference to local transform
	private Transform ThisTransform = null;
	//------------------------------------
	// Use this for initialization
	void Awake ()
	{
		ThisTransform = GetComponent<Transform> ();
	}
	//------------------------------------
	// Update is called once per frame
	void LateUpdate () 
	{
		ThisTransform.LookAt (Camera.main.transform);
	}
	//------------------------------------
}
