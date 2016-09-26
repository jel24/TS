using UnityEngine;
using System.Collections;

public class AreaDoor : MonoBehaviour {

	public AreaDoor target;
	public Camera camera;

	void OnTriggerEnter (Collider collider)
	{

		if (collider.tag == "Player") { // player moving through door
			
			Player player = collider.GetComponent<Player> (); // player entering or leaving

			if (player.entering == false) { //entering
				player.entering = true;

				player.transform.position = target.transform.position;

			} else if (player.entering == true) { //exiting

				player.transform.Translate (Vector3.forward * 5);
				player.entering = false;
				player.SwitchCamera (camera);
			}




		}

	}

}
