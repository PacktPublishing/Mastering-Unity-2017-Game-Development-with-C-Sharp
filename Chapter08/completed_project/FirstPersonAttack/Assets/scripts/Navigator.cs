//------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//------------------------------------
public class Navigator : MonoBehaviour
{
	//------------------------------------
	public int CurrentNode = 0;
	private Animator ThisAnimator = null;
	private int AnimStateHash = Animator.StringToHash("NavState");

	//Reference to NPC die event
	public UnityEvent EnemyDie;

	//Reference to navigator button
	private Button NavigatorButton = null;

	//Reference to singleton instance
	public static Navigator ThisInstance
	{
		get
		{
			//Get or create singleton instance
			if (mThisInstance == null)
			{
				GameObject GO = new GameObject ("Navigator");
				mThisInstance = GO.AddComponent<Navigator> ();
			}

			return mThisInstance;
		}

		set 
		{
			//If not null then we already have instance
			if (mThisInstance != null) 
			{
				//If different, then remove duplicate immediately
				if(mThisInstance.GetInstanceID() != value.GetInstanceID())
					DestroyImmediate (value.gameObject);

				return;
			}

			//If new, then create new sinleton instance
			mThisInstance = value;
		}
	}

	private static Navigator mThisInstance = null;
	//------------------------------------
	void Awake()
	{
		ThisInstance = this;
		ThisAnimator = GetComponent<Animator> ();
		NavigatorButton = GameObject.FindGameObjectWithTag ("NavigatorButton").GetComponent<Button>();
		NavigatorButton.gameObject.SetActive (false);
	}
	//------------------------------------
	public void Next()
	{
		//Reset typing
		Typer.TypedWord = string.Empty;

		++CurrentNode;
		ThisAnimator.SetInteger (AnimStateHash, CurrentNode);
	}
	//------------------------------------
	public void Prev()
	{
		//Reset typing
		Typer.TypedWord = string.Empty;

		--CurrentNode;
		ThisAnimator.SetInteger (AnimStateHash, CurrentNode);
	}
	//------------------------------------
	//Show button if there are no remaining enemies
	public void ShowMoveButton()
	{
		Debug.Log (AIEnemy.ActiveEnemies);


		//Should we show move button?
		if (AIEnemy.ActiveEnemies > 0) return;

		NavigatorButton.gameObject.SetActive (true);
	}
	//------------------------------------
}
//------------------------------------