using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	
	private List<Vector3> spawnPoints = new List<Vector3>();
	private List<Camera> cameras = new List<Camera>();
	private List<Player> players = new List<Player>();
	private string[] names = {"Eadwig", "Salasmund", "Dannis", "Lideon", "Sully", "Randy", 
										"Veniris", "Bandor", "Lyrra", "Rena", "Wanda", "Xekan", "Peato",
										"Bandobald"};
	private GameObject GameHud;
	private GameObject LobbyHud;
	private Camera respawnCam;

	private void Setup ()
	{
		//GameHud = GameObject.Find ("Game Hud");
		//LobbyHud = GameObject.Find ("Lobby Hud");
		SpawnPoint[] spawnObjects = GameObject.FindObjectsOfType<SpawnPoint> ();
		foreach (SpawnPoint obj in spawnObjects) {
			spawnPoints.Add (obj.transform.position);
			cameras.Add (obj.spawnCamera);
			if (obj.spawnCamera.name == "Hospital Camera") {
				respawnCam = obj.spawnCamera;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartGame ()
	{
		Setup();

		//LobbyHud.gameObject.SetActive(false);
		//GameHud.gameObject.SetActive(true);
		Player[] playerObjects = FindObjectsOfType<Player> ();
		foreach (Player obj in playerObjects) {
			players.Add (obj.GetComponent<Player> ());
		}
		Debug.Log ("Started a " + players.Count + " player game.");
		foreach (Player p in players) {
			int rand = Random.Range (0, 6);
			p.SetName (GetRandomName ());
			p.SetRespawnCam (respawnCam);
			print (rand);
			foreach (Camera c in cameras) {
				print (c.name);
			}
			p.SwitchCamera(cameras[rand]);
			p.transform.position = spawnPoints[rand];
			p.StartGame();
		}
	}

	private string GetRandomName ()
	{

		return names[Random.Range(0, names.Length)];

	}
}
