using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Weapon : NetworkBehaviour {

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

	void OnTriggerEnter(Collider defender){

		if (defender.GetComponent<Player>() && owner.GetComponent<Animator>().GetBool("attack")){
			Player player = defender.GetComponent<Player>();
			if (player != owner && internalCooldown == 0 && player.IsAlive()){

				owner.writeMessageLocal("You hit a target.\n");
				player.writeMessageLocal("You took damage.\n");
				audioSource.Play();
				internalCooldown += weaponCooldown;
				player.TakeDamage(damage);

			}
		}

	}
}
