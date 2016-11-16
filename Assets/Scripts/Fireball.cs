using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public float velocity;
	public Player owner;
	public AudioClip explosionSound;
	public int damage;

	private AudioSource audioSource;
	private float birth;
	private bool init;
	private bool active;

	void Start() {
		audioSource = this.GetComponent<AudioSource>();
		birth = Time.time;
		init = true;
		active = false;
		Invoke("Activate", .25f);
	}

	private void Activate ()
	{
		active = true;
	}

	// Update is called once per frame
	void Update ()
	{
		this.transform.Translate (Vector3.forward * Time.deltaTime * velocity);
		if (Time.time - birth > 3) {
			Explode();
		}
	}

	public void OnTriggerEnter (Collider c)
	{
		Debug.Log("Triggered!");
		if (c.GetComponent<Player>()) {
			Debug.Log("Collided!");
			Explode();
		}

	}

	private void Explode ()
	{
		if (active) {
			audioSource.clip = explosionSound;
			audioSource.Play ();
			active = false;
			velocity = 0;

			Collider[] hitTargets = Physics.OverlapSphere (transform.position, 1f);

			for (int i = 0; i < hitTargets.Length; i++) {
				Player player = hitTargets [i].GetComponent<Player> ();
				if (player != null) {
					hitTargets [i].GetComponent<Player> ().TakeDamage (damage);
					player.writeMessage ("Fireball exploded and hit " + player.name);
				}
			}

			Invoke("CleanUp", 1f);
			GetComponent<ParticleSystem>().Emit(100);
		}

	}

	private void CleanUp ()
	{
		Destroy (this.gameObject);
	}
}
