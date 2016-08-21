using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	public bool entering = false;
	public Camera activeCam;

	public int maxHealth;

	private int curHealth;
	private Animator animator;

	// Use this for initialization
	void Start () {
		activeCam = FindObjectOfType<Camera>();
		curHealth = maxHealth;
		animator = GetComponent<Animator>();
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

	public void TakeDamage (int damage)
	{

		curHealth -= damage;

		if (curHealth <= 0) { // death

			print("You have died.");
			curHealth = maxHealth;
			animator.SetBool("dead", true);

		}

	}
}
