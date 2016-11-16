using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	
	private List<Vector3> spawnPoints = new List<Vector3>();
	private List<Camera> cameras = new List<Camera>();
	private List<Player> players = new List<Player>();
	private string[] names = {"Eadwig", "Salasmund", "Dannis", "Lideon", "Sully", "Randy", 
										"Veniris", "Bandor", "Lyrra", "Rena", "Wanda", "Xekan", "Peato",
										"Bandobald"};
	public StaticsManager statics;
	private GameObject GameHud;
	private GameObject LobbyHud;
	private Camera respawnCam;

	private bool gameStarted;


	void Start ()
	{
		Setup ();

		if (LobbyHud) {
			LobbyHud.gameObject.SetActive(false);
		}

		GameHud.gameObject.SetActive(true);
		Player[] playerObjects = FindObjectsOfType<Player> ();
		foreach (Player obj in playerObjects) {
			players.Add (obj.GetComponent<Player> ());
		}
		Debug.Log ("Started a " + players.Count + " player game.");
		gameStarted = true;
		NetworkServer.Spawn(this.gameObject);
	}

	private void Setup ()
	{

		statics = FindObjectOfType<StaticsManager> ();
		GameHud = statics.gameHud;
		if (statics.lobbyHud) {
			LobbyHud = statics.lobbyHud;
		}


		SpawnPoint[] spawnObjects = GameObject.FindObjectsOfType<SpawnPoint> ();
		foreach (SpawnPoint obj in spawnObjects) {
			spawnPoints.Add (obj.transform.position);
			cameras.Add (obj.spawnCamera);
		}
	}

	public string GetRandomName ()
	{
		return names [Random.Range (0, names.Length)];
	}

	public bool HasGameStarted ()
	{
		return gameStarted;
	}

	public List<Camera> GetCameras(){
		return cameras;
	}

	public List<Player> GetPlayers(){
		return players;
	}

	public List<Vector3> GetSpawnPoints(){
		return spawnPoints;
	}


}
