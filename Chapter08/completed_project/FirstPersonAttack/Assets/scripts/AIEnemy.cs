//------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
//------------------------------------
public class AIEnemy : MonoBehaviour
{
	//------------------------------------
	public enum AISTATE {IDLE = 0, CHASE = 1, ATTACK = 2, DEAD=3};
	public AISTATE ActiveState
	{
		get{ return mActiveState; }
		set
		{ 
			StopAllCoroutines ();
			mActiveState = value;

			switch(mActiveState)
			{
			case AISTATE.IDLE:
					StartCoroutine (StateIdle());
				break;

				case AISTATE.CHASE:
					StartCoroutine (StateChase());
				break;

				case AISTATE.ATTACK:
					StartCoroutine (StateAttack());
				break;

				case AISTATE.DEAD:
					StartCoroutine (StateDead());
				break;
			}
				
			OnStateChanged.Invoke ();
		}
	}
		
	[SerializeField]
	private AISTATE mActiveState = AISTATE.IDLE;
	//------------------------------------
	//Events called on FSM changes
	public UnityEvent OnStateChanged;
	public UnityEvent OnIdleEnter;
	public UnityEvent OnChaseEnter;
	public UnityEvent OnAttackEnter;
	public UnityEvent OnTypingChanged;
	public UnityEvent OnTypingMatched;
	//------------------------------------
	//Component references
	private Animator ThisAnimator = null;
	private UnityEngine.AI.NavMeshAgent ThisAgent = null;
	private Transform ThisTransform = null;

	//Reference to player transform
	private Transform PlayerTransform = null;

	//Points for enemy
	public int ScorePoints = 10;

	//Reference to Score Text
	private UIScore ScoreText = null;

	//Player health component
	private Health PlayerHealth = null;

	//Word associated
	public string AssocWord = string.Empty;

	//Extent of word match with associated word
	public string MatchedWord = string.Empty;

	//Amount of damage to deal on attack
	public int AttackDamage = 10;

	//Text component
	private Text NameTextComp = null;

	//Active enemy count (how many enemies wandering at one time?)
	public static int ActiveEnemies = 0;

	//Sound to play on hit
	public AudioSource HitSound = null;
	//------------------------------------
	void Awake()
	{
		ThisAnimator = GetComponent<Animator> ();
		ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		PlayerTransform = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		PlayerHealth = PlayerTransform.GetComponent<Health>();
		//Find and get associated UI Text
		NameTextComp = GetComponentInChildren<Text> ();
		ThisTransform = GetComponent<Transform> ();
		HitSound = GetComponent<AudioSource> ();
		ScoreText = GameObject.FindGameObjectWithTag ("ScoreText").GetComponent<UIScore> ();

		//Hide text
		NameTextComp.gameObject.SetActive(false);
	}
	//------------------------------------
	void Start()
	{
		//Set active state
		ActiveState = mActiveState;

		//Get random word
		AssocWord = WordList.ThisInstance.GetRandomWord();

		UpdateText();
	}
	//------------------------------------
	public IEnumerator StateIdle()
	{
		//Run idle animation
		ThisAnimator.SetInteger("AnimState", (int) ActiveState);

		//While in idle state
		while(ActiveState == AISTATE.IDLE)
		{
	
			yield return null;
		}
	}
	//------------------------------------
	public IEnumerator StateChase()
	{
		++ActiveEnemies;

		//Run chase animation
		ThisAnimator.SetInteger("AnimState", (int) ActiveState);

		//Set destination
		ThisAgent.SetDestination (PlayerTransform.position);

		//Wait until path is calculated
		while (!ThisAgent.hasPath)
			yield return null;

		//While in idle state
		while(ActiveState == AISTATE.CHASE)
		{
			if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance)
			{
				ThisAgent.Stop ();
				yield return null;
				ActiveState = AISTATE.ATTACK;
				yield break;
			}

			yield return null;
		}
	}
	//------------------------------------
	public IEnumerator StateAttack()
	{
		//Run attack animation
		ThisAnimator.SetInteger("AnimState", (int) ActiveState);

		//While in idle state
		while(ActiveState == AISTATE.ATTACK)
		{
			//Look at player
			Vector3 PlanarPosition = new Vector3(PlayerTransform.position.x, ThisTransform.position.y, PlayerTransform.position.z);
			ThisTransform.LookAt(PlanarPosition, ThisTransform.up);

			//Get distance between enemy and player
			float Distance = Vector3.Distance(PlayerTransform.position, ThisTransform.position);

			if (Distance > ThisAgent.stoppingDistance*2f)
			{
				ThisAgent.Stop ();
				yield return null;
				ActiveState = AISTATE.CHASE;
				yield break;
			}

			yield return null;
		}
	}
	//------------------------------------
	public IEnumerator StateDead()
	{
		//Run dead animation
		ThisAnimator.SetInteger("AnimState", (int) ActiveState);

		//While in idle state
		while(ActiveState == AISTATE.DEAD)
		{

			yield return null;
		}
	}
	//------------------------------------
	public void UpdateTypedWord()
	{
		//If not chasing or attacking, then ignore
		if(ActiveState != AISTATE.CHASE && ActiveState != AISTATE.ATTACK) return;

		MatchedWord = WordList.CompareWords (Typer.TypedWord, AssocWord);

		//Check for typing match
		if (MatchedWord.Length != AssocWord.Length)
			return;

		if (MatchedWord.Equals (AssocWord))
			OnTypingMatched.Invoke (); //Match found. Invoke matched event
	}
	//------------------------------------
	//Deal damage to the player
	public void DealDamage()
	{
		PlayerHealth.Value -= AttackDamage;
		HitSound.Play ();
	}
	//------------------------------------
	// Update is called once per frame
	public void UpdateText ()
	{
		//Build UI String
		NameTextComp.text = "<color=red>" + MatchedWord + "</color>" + AssocWord.Substring(MatchedWord.Length,AssocWord.Length-MatchedWord.Length);
	}
	//------------------------------------
	public void Die()
	{
		//Update Game Score
		GameManager.ThisInstance.Score += ScorePoints;
		ScoreText.OnScoreChange.Invoke ();

		//Calcluate Bonus, if achieved
		float LettersPerSecond = AssocWord.Length / Typer.ElapsedTime;

		//If we beat best times, then get bonus
		if (LettersPerSecond < Typer.RecordLettersPerSecond) 
		{
			//Bonus achieved
			++GameManager.ThisInstance.BonusLevel;
		}

		ActiveState = AISTATE.DEAD;
		--ActiveEnemies;

		//Reset matched word
		MatchedWord = string.Empty;

		//Update Navigator
		Navigator.ThisInstance.EnemyDie.Invoke();
	}
	//------------------------------------
	public void WakeUp()
	{
		ActiveState = AISTATE.CHASE;
	}
	//------------------------------------
}
//------------------------------------