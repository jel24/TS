using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	public float speed;
	public float turnSpeed;

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
			if (isLocalPlayer && player.HasSpawned ()) {

				float inputZ = CrossPlatformInputManager.GetAxis ("Vertical");
				float inputX = CrossPlatformInputManager.GetAxis ("Horizontal");

				/* MOVEMENT */

				if (Mathf.Abs (inputZ) > 0 || Mathf.Abs (inputX) > 0) { // walking


					if (!animator.GetBool ("attack") && !animator.GetBool("dodge")) {
						Vector3 targetPoint = transform.position + new Vector3 (inputX, 0f, inputZ);
						transform.LookAt (targetPoint);
						float cameraRotation = player.activeCam.transform.rotation.eulerAngles.y;
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

