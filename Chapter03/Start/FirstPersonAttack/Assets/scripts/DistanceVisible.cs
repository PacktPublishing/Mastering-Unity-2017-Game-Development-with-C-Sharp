using UnityEngine;
using System.Collections;

public class DistanceVisible : MonoBehaviour 
{
	//Reference to local sphere, marking distance
	private SphereCollider ThisSphere = null;

	//Object to show hide
	public GameObject ShowHideObject = null;

	void OnTriggerEnter(Collider Col)
	{
		if (!Col.CompareTag ("Player"))
			return;

		if (ShowHideObject != null)
			ShowHideObject.SetActive (true);
	}

	void OnTriggerExit(Collider Col)
	{
		if (!Col.CompareTag ("Player"))
			return;

		if (ShowHideObject != null)
			ShowHideObject.SetActive (false);
	}
}
