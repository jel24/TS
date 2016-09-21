using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThroneManager : MonoBehaviour {

	private List<Player> playerGroup;

	private Player controller;

	void Start ()
	{
		playerGroup = new List<Player>();
		UpdatePoints();
	}

	void Update ()
	{
		if (playerGroup.Count == 1 && playerGroup[0] != controller) {

			controller = playerGroup[0];
			controller.writeMessage("Someone has claimed the throne.");

		}
	}

	private void OnTriggerEnter (Collider collider)
	{

		Player player = collider.GetComponent<Player> ();
		if (player != null) {
			playerGroup.Add (player);
			if (playerGroup.Count > 1) {
				player.writeMessageLocal ("Another currently controls the throne.");
			}
			player.writeMessageLocal ("You have laid claim to the throne, " + player.GetName () + ".");
		}


	}

	private void OnTriggerExit (Collider collider)
	{

		Player player = collider.GetComponent<Player> ();
		if (player != null) {
			playerGroup.Remove (player);
			if (player == controller) {
				controller = null;
			}
		}

	}

	private void UpdatePoints ()
	{
		if (controller != null) {
			controller.ScoreVP(1);

		}
		Invoke("UpdatePoints", 15);
	}
}
