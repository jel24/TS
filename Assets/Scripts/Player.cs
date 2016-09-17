using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

	public bool entering = false;
	public Camera activeCam;

	public int maxHealth;

	private Image healthBar;
	private int curHealth;
	private Animator animator;
	private Canvas canvas;
	private Text VPCounter;
	private Text textArea;
	private int victoryPoints;

	private bool alive = true;

	// Use this for initialization
	void Start () {
		activeCam = FindObjectOfType<Camera>();
		curHealth = maxHealth;
		animator = GetComponent<Animator>();
		canvas = FindObjectOfType<Canvas>();
		textArea = GameObject.FindGameObjectWithTag("Feed").GetComponent<Text>();
		healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
		VPCounter = GameObject.FindGameObjectWithTag("VPCounter").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SwitchCamera (Camera newCam)
	{

		if (isLocalPlayer) {

			Debug.Log("Current camera is " + activeCam);
			Debug.Log("Switching cameras to " + newCam.name);
			newCam.gameObject.SetActive(true);
			activeCam.gameObject.SetActive(false);
			activeCam = newCam;
		}

	}

	public void StopAttacking ()
	{
		animator.SetBool("attack", false);
	}

	public void StopDodging ()
	{
		animator.SetBool("dodge", false);
	}

	public void TakeDamage (int damage)
	{
		curHealth -= damage;

		UpdateHealthBar();

		if (curHealth <= 0) { // death

			writeMessageLocal("You have died.\n");
			curHealth = maxHealth;
			animator.SetBool("dead", true);
			this.alive = false;

		}
	}

	private void UpdateHealthBar ()
	{
		if (isLocalPlayer) {
			Rect barDimensions = healthBar.GetComponent<RectTransform>().rect;
			writeMessageLocal("Updating bar size to " + barDimensions.width * (curHealth * 1f / maxHealth * 1f) + "\n");
			healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2( 188 * (curHealth * 1f / maxHealth * 1f), barDimensions.height);
		}
	}

	public bool IsAlive ()
	{
		return alive;
	}

	public void writeMessage (string text)
	{
		textArea.text += text;
	}

	public void writeMessageLocal (string text)
	{
		if (isLocalPlayer) {

			textArea.text += text;

		}
	}

	public void ScoreVP (int value)
	{
		victoryPoints += value;
		if (isLocalPlayer) {
			VPCounter.text = victoryPoints.ToString();
		}

	}
}
