using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

	public bool entering = false;
	public Camera activeCam;

	public int maxHealth;

	private GameObject healthBar;
	private int curHealth;
	private Animator animator;
	private Canvas canvas;
	private Text textArea;

	private bool alive = true;

	// Use this for initialization
	void Start () {
		activeCam = FindObjectOfType<Camera>();
		curHealth = maxHealth;
		animator = GetComponent<Animator>();
		canvas = FindObjectOfType<Canvas>();
		textArea = canvas.GetComponentInChildren<Text>();
		healthBar = GameObject.FindGameObjectWithTag("HealthBar");
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

		Rect barDimensions = healthBar.GetComponent<RectTransform>().rect;

		healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2( barDimensions.width * (curHealth / maxHealth * 1f), barDimensions.height);

		if (curHealth <= 0) { // death

			writeMessageLocal("You have died.\n");
			curHealth = maxHealth;
			animator.SetBool("dead", true);
			this.alive = false;

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
}
