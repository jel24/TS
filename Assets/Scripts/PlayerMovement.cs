using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	public float speed;
	public float turnSpeed;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isLocalPlayer) {

			float inputY = CrossPlatformInputManager.GetAxis ("Vertical");

			if (inputY > 0) { //forward
				transform.Translate (Vector3.forward * Time.deltaTime * speed * inputY);
			} else if (inputY < 0) { //backward
				transform.Translate (Vector3.forward * Time.deltaTime * speed/2 * inputY);
			}


			float inputX = CrossPlatformInputManager.GetAxis ("Horizontal");
			Vector3 newRotation = new Vector3 (0f, turnSpeed * inputX * Time.deltaTime, 0f);
			transform.Rotate (newRotation);

			if (inputY == 0) {

				animator.SetBool ("walking", false);

			} else {
				animator.SetBool ("walking", true);
			}

		}

	}

	public override void OnStartLocalPlayer ()
	{

	}
}

