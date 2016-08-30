using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

	public bool entering = false;
	public Camera activeCam;

	public int maxHealth;

	private int curHealth;
	private Animator animator;
	private Canvas canvas;
	private Text textArea;

	// Use this for initialization
	void Start () {
		activeCam = FindObjectOfType<Camera>();
		curHealth = maxHealth;
		animator = GetComponent<Animator>();
		canvas = FindObjectOfType<Canvas>();
		textArea = canvas.GetComponentInChildren<Text>();
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

		if (curHealth <= 0) { // death

			textArea.text += "You have died.";
			curHealth = maxHealth;
			animator.SetBool("dead", true);

		}


	}
}
