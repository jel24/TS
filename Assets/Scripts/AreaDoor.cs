using UnityEngine;
using System.Collections;

public class AreaDoor : MonoBehaviour {

	public AreaDoor target;
	public Camera camera;

	void OnTriggerEnter (Collider collider)
	{

		if (collider.tag == "Player") { // player moving through door
			Player player = collider.GetComponent<Player> (); // player entering or leaving

			if (player.entering) { // move into current room
				player.transform.Translate (Vector3.forward * 5);
				player.entering = false;
			} else { // move to another room
				player.entering = true;
				player.transform.position = target.transform.position;
				player.SwitchCamera(target.camera);
			}


		}

	}

}
