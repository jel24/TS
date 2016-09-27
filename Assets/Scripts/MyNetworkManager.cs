using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

	private bool waiting = false;
	public GameManager gameManager;

	public void MyStartHost ()
	{

		Debug.Log(Time.timeSinceLevelLoad + " -- starting host.");
		StartHost();
		StartClient();

	}

	public override void OnStartHost ()
	{
		Debug.Log(Time.timeSinceLevelLoad + " -- host started.");

	}

	public override void OnStartClient (NetworkClient myClient)
	{
		Debug.Log(Time.timeSinceLevelLoad + " -- client start requested.");
		waiting = true;
		MyWaiting();
	}

	public override void OnClientConnect (NetworkConnection conn)
	{
		waiting = false;
		Debug.Log(Time.timeSinceLevelLoad + " -- client connected to " + conn.address);
		

	}

	private void MyWaiting ()
	{
		if (waiting) {
			print(".");
			Invoke("MyWaiting", 1f);
		}
	}

	public void StartGame ()
	{
		GameManager game = Instantiate(gameManager);
		NetworkServer.Spawn(game.gameObject);
		game = GameObject.FindObjectOfType<GameManager>();
		game.StartGame();
	}
}
