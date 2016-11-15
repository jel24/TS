using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

	public bool entering = false;
	public Camera activeCam;
	public string name;
	public Fireball fireball;
	public int maxHealth;

	private Image healthBar;
	private int curHealth;
	private Animator animator;
	private Canvas canvas;
	private Text VPCounter;
	private FeedManager feed;
	private Text locationText;
	private int victoryPoints;
	private Vector3 respawnLoc;
	private Camera respawnCam;
	private GameManager gameManager;
	private bool playerSpawned = false;
	private string location;
	private bool alive = true;
	private ResourceManager resources;
	private Canvas playerLabel;

	// Use this for initialization
	void Start () {
		activeCam = FindObjectOfType<Camera>();
		curHealth = maxHealth;
		animator = GetComponent<Animator>();
		respawnLoc = GameObject.FindGameObjectWithTag("RespawnPoint").GetComponent<Transform>().position;
		locationText = GameObject.Find("Location Text").GetComponent<Text>();
		resources = GetComponent<ResourceManager>();
		playerLabel = GetComponentInChildren<Canvas>();
	}

	public void SetRespawnCam (Camera cam)
	{
		respawnCam = cam;
	}

	public void StartGameHud ()
	{
		canvas = FindObjectOfType<Canvas>();
		feed = GameObject.FindObjectOfType<FeedManager>();
		healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
		VPCounter = GameObject.FindGameObjectWithTag("VPCounter").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (gameManager == null) {
			gameManager = FindObjectOfType<GameManager> ();
			if (gameManager) {

			}
		} else {

			if (gameManager.HasGameStarted () && !playerSpawned) {
				print("Game Started");
				StartGame();
			}
		}

	}

	public void SwitchCamera (Camera newCam)
	{

		if (isLocalPlayer) {

			//Debug.Log("Current camera is " + activeCam);
			//Debug.Log("Switching cameras to " + newCam.name);
			location = newCam.transform.parent.gameObject.name;
			locationText.text = location;
			newCam.gameObject.SetActive(true);
			activeCam.gameObject.SetActive(false);
			activeCam = newCam;
		}

	}

	public void StopAttacking ()
	{
		animator.SetBool("attack", false);
	}

	public void StopDodging ()
	{
		animator.SetBool("dodge", false);
	}

	public void TakeDamage (int damage)
	{
		curHealth -= damage;

		UpdateHealthBar();

		if (curHealth <= 0) { // death

			Die();

		}
	}

	private void UpdateHealthBar ()
	{
		if (isLocalPlayer) {
			Rect barDimensions = healthBar.GetComponent<RectTransform>().rect;
			// writeMessageLocal("Updating bar size to " + barDimensions.width * (curHealth * 1f / maxHealth * 1f));
			healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2( 188 * (curHealth * 1f / maxHealth * 1f), barDimensions.height);
		}
	}

	private void Die ()
	{

		if (isLocalPlayer) {

			writeMessageLocal("You will respawn in 20 seconds.");
			writeMessageLocal("You have died.");
			curHealth = maxHealth;
			animator.SetBool("dead", true);
			this.alive = false;
			Invoke("Respawn", 20);
		}

	}

	private void Respawn ()
	{
		activeCam.gameObject.SetActive(false);
		activeCam = gameManager.statics.cameras[16];
		activeCam.gameObject.SetActive(true);
		this.transform.position = respawnLoc;
		this.alive = true;
		animator.SetBool("dead", false);
		UpdateHealthBar();
	}

	public bool IsAlive ()
	{
		return alive;
	}

	public void writeMessage (string text)
	{
		feed.Write(text);
	}

	public void writeMessageLocal (string text)
	{
		if (isLocalPlayer) {

			feed.Write(text);

		}
	}

	public void ScoreVP (int value)
	{
		victoryPoints += value;

		if (victoryPoints <= 0) {
			victoryPoints = 0;
		}

		if (isLocalPlayer) {
			VPCounter.text = victoryPoints.ToString();
		}

	}

	public void SetName (string text)
	{
		this.name = text;
	}

	public string GetName ()
	{
		return name;
	}

	public void StartGame ()
	{
		int rand = Random.Range (0, 5);
		SetName (gameManager.GetRandomName ());
		SetRespawnCam (respawnCam);
		print (rand);
		SwitchCamera(gameManager.GetCameras()[rand]);
		transform.position = gameManager.GetSpawnPoints()[rand];
		playerSpawned = true;
		StartGameHud();
	}

	public bool HasSpawned(){
		return playerSpawned;
	}

	public bool IsDodging ()
	{
		return animator.GetBool("dodge");
	}

	public ResourceManager GetResourceManager ()
	{
		return resources;
	}

	public Canvas GetPlayerLabel ()
	{
		return playerLabel;
	}

	[Command]
	public void CmdSpawnFireball ()
	{
		Vector3 playerPos = transform.position;

		Vector3 fireballPos = new Vector3 (playerPos.x, playerPos.y + 1.5f, playerPos.z);
		Quaternion playerRot = transform.rotation;

		Object ball = Instantiate (fireball, fireballPos, playerRot);
		Fireball projectile = ball as Fireball;
		projectile.owner = this;

		Debug.Log (ball.name);
		NetworkServer.Spawn(projectile.gameObject);

	}
}
