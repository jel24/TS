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

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		player = this.GetComponent<Player>();
		man = GameObject.FindObjectOfType<NetworkManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isLocalPlayer) {

			float inputZ = CrossPlatformInputManager.GetAxis ("Vertical");
			float inputX = CrossPlatformInputManager.GetAxis ("Horizontal");

			/* MOVEMENT */

			if (Mathf.Abs (inputZ) > 0 || Mathf.Abs (inputX) > 0) { // walking


				if (!animator.GetBool("attack")) {
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

			if (CrossPlatformInputManager.GetButton("Fire1")){ // attack
					if (animator.GetBool("attack") == false){
						animator.SetBool("attack", true);
					}
			}

			if (CrossPlatformInputManager.GetButtonDown("Fire2")){ // dodge

					bool onOff = man.gameObject.activeSelf;
					player.writeMessageLocal("Your network HUD is on: " + !onOff);
					man.gameObject.SetActive(!onOff);
			}


		}

	}

	public override void OnStartLocalPlayer ()
	{

	}

}

