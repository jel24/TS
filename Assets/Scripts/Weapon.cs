using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Weapon : MonoBehaviour {

	public Player owner;
	public int damage;
	public int weaponCooldown;

	private int internalCooldown = 0;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (internalCooldown > 0) {
			internalCooldown--;
		}
	}

	void OnTriggerEnter (Collider defender)
	{

		if (defender.GetComponent<Player> () && owner.GetComponent<Animator> ().GetBool ("attack")) {
			Player defendingPlayer = defender.GetComponent<Player> ();
			if (defendingPlayer != owner && internalCooldown == 0 && defendingPlayer.IsAlive ()) {


				// defendingPlayer.writeMessageLocal ("You took damage.\n");
				audioSource.Play ();
				internalCooldown += weaponCooldown;
				defendingPlayer.TakeDamage (damage);
				if (!defendingPlayer.IsAlive ()) {
					owner.ScoreVP(2);
					defendingPlayer.ScoreVP(-2);
					owner.writeMessageLocal ("Kill confirmed. +2 VP\n");
				}

			}
		}

	}
}
