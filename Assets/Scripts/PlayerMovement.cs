using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	public float speed;
	public float turnSpeed;
	public Fireball fireball;

	private Player player;
	private Animator animator;
	private NetworkManager man;
	private GameManager gameManager;


	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		player = this.GetComponent<Player>();
		man = GameObject.FindObjectOfType<NetworkManager>();

	}

	// Update is called once per frame
	void Update ()
	{
		if (gameManager == null) {
			gameManager = FindObjectOfType<GameManager> ();
		} else {
			if (isLocalPlayer && player.HasSpawned () && player.IsAlive()) {

				float inputZ = CrossPlatformInputManager.GetAxis ("Vertical");
				float inputX = CrossPlatformInputManager.GetAxis ("Horizontal");

				/* MOVEMENT */

				if (Mathf.Abs (inputZ) > 0 || Mathf.Abs (inputX) > 0) { // walking


					if (!animator.GetBool ("attack") && !animator.GetBool("dodge")) {
						Vector3 targetPoint = transform.position + new Vector3 (inputX, 0f, inputZ);
						transform.LookAt (targetPoint);
						float cameraRotation = player.activeCam.transform.rotation.eulerAngles.y;
						float labelRotation = player.GetPlayerLabel().transform.rotation.eulerAngles.y;

						Canvas label = player.GetPlayerLabel();
						Vector3 newRot = new Vector3 (0f, 45f, 0f);

						label.transform.rotation = Quaternion.Euler(45f, 0f, 0f);


						// print ("Camera is rotated at " + cameraRotation + " degrees. Adjusting.");
						transform.RotateAround (transform.position, Vector3.up, cameraRotation);
						transform.Translate (Vector3.forward * Time.deltaTime * speed);

					}
					animator.SetBool ("walking", true);
				} else { // stopped
					animator.SetBool ("walking", false);
				}

				if (CrossPlatformInputManager.GetButton ("Fire1")) { // attack
					if (!Acting()) {
						animator.SetBool ("attack", true);
					}
				}

				if (CrossPlatformInputManager.GetButtonDown ("Fire2")) { // dodge

					if (!Acting()) {
						animator.SetBool ("dodge", true);
					}

				}

				if (CrossPlatformInputManager.GetButtonDown ("Fire3")) { // disable hud

					bool onOff = man.gameObject.activeSelf;
					player.writeMessageLocal("Your network HUD is on: " + !onOff);
					man.gameObject.SetActive(!onOff);
				}

				if (CrossPlatformInputManager.GetButtonDown ("Jump")) { // fireball


					Vector3 playerPos = player.transform.position;

					Vector3 fireballPos = new Vector3(playerPos.x, playerPos.y + 1f, playerPos.z);
					Quaternion playerRot = player.transform.rotation;

					Object ball = Instantiate(fireball, fireballPos, playerRot);
					Fireball projectile = ball as Fireball;
					projectile.owner = player;


					NetworkServer.Spawn(ball as GameObject);
				}

				if (animator.GetBool ("dodge")) {
					transform.Translate (Vector3.forward * Time.deltaTime * speed * 4f);
				}
		}
		}



	}

	public override void OnStartLocalPlayer ()
	{

	}

	private bool Acting ()
	{
		return animator.GetBool("dodge") || animator.GetBool("attack");
	}
}

