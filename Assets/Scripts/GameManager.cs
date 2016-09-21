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
	public GameObject GameHud;
	public GameObject LobbyHud;
	public Camera respawnCam;

	void Start () {
		SpawnPoint[] spawnObjects = GameObject.FindObjectsOfType<SpawnPoint>();
		foreach (SpawnPoint obj in spawnObjects){
			spawnPoints.Add(obj.transform.position);
			cameras.Add(obj.spawnCamera);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartGame ()
	{
		int rand = Random.Range(0, 6);
		LobbyHud.gameObject.SetActive(false);
		GameHud.gameObject.SetActive(true);
		Player[] playerObjects = FindObjectsOfType<Player>();
		foreach (Player obj in playerObjects){
			players.Add(obj.GetComponent<Player>());
		}
		Debug.Log("Started a " + players.Count + " player game.");
		foreach (Player p in players) {
			p.SetName (GetRandomName ());
			p.SetRespawnCam(respawnCam);
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
