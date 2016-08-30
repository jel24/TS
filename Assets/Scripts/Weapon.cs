using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Weapon : NetworkBehaviour {

	public Player owner;
	public int damage;


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

	void OnTriggerEnter(Collider defender){

		if (defender.GetComponent<Player>() && owner.GetComponent<Animator>().GetBool("attack")){
			Player player = defender.GetComponent<Player>();
			if (!player.isLocalPlayer && internalCooldown == 0){

				print("You hit a target.");
				audioSource.Play();
				internalCooldown += 35;
				player.TakeDamage(damage);

			}
		}

	}
}
