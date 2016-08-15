using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyHUD : MonoBehaviour {

	private NetworkManager networkManager;

	// Use this for initialization
	void Start () {
		networkManager = this.GetComponent<NetworkManager>();
	}
	
	public void MyStartHost ()
	{

		networkManager.StartHost();
		Debug.Log("Hosting started at " + Time.timeSinceLevelLoad);

	}
}
