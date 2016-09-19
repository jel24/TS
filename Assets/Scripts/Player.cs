﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

	public bool entering = false;
	public Camera activeCam;
	public string name;

	public int maxHealth;

	private Image healthBar;
	private int curHealth;
	private Animator animator;
	private Canvas canvas;
	private Text VPCounter;
	private FeedManager feed;
	private int victoryPoints;
	private Vector3 respawnLoc;
	private Camera respawnCam;

	private bool alive = true;

	// Use this for initialization
	void Start () {
		activeCam = FindObjectOfType<Camera>();
		curHealth = maxHealth;
		animator = GetComponent<Animator>();
		canvas = FindObjectOfType<Canvas>();
		feed = GameObject.FindObjectOfType<FeedManager>();
		healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
		VPCounter = GameObject.FindGameObjectWithTag("VPCounter").GetComponent<Text>();
		respawnLoc = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
		respawnCam = activeCam;
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
		activeCam = respawnCam;
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
}
