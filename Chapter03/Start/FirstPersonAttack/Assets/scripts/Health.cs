//------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//------------------------------------
public class Health : MonoBehaviour
{
	//------------------------------------
	public float Value
	{
		get{return fValue;}
		set
		{
			fValue = value;
			OnHealthChanged.Invoke();
		
			if (fValue <= 0f)
				OnHealthExpired.Invoke();
		} 
	}

	[SerializeField]
	private float fValue = 100f;
	//------------------------------------
	//Events called on health change
	public UnityEvent OnHealthExpired;
	public UnityEvent OnHealthChanged;
	//------------------------------------
}	
