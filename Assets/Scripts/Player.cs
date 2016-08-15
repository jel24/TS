using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	public bool entering = false;
	private Camera activeCam;

	// Use this for initialization
	void Start () {
		activeCam = FindObjectOfType<Camera>();
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
}
