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
	private GameObject GameHud;
	private GameObject LobbyHud;
	private Camera respawnCam;

	private bool gameStarted;


	void Start ()
	{
		Setup();

		LobbyHud.gameObject.SetActive(false);
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
		GameHud = GameObject.Find ("Game Hud");
		LobbyHud = GameObject.Find ("Lobby Hud");
		SpawnPoint[] spawnObjects = GameObject.FindObjectsOfType<SpawnPoint> ();
		foreach (SpawnPoint obj in spawnObjects) {
			spawnPoints.Add (obj.transform.position);
			cameras.Add (obj.spawnCamera);
			if (obj.spawnCamera.name == "Hospital Camera") {
				respawnCam = obj.spawnCamera;
			}
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
