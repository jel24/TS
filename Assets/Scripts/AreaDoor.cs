using UnityEngine;
using System.Collections;

public class AreaDoor : MonoBehaviour {

	public AreaDoor target;
	public Camera camera;

	void OnTriggerEnter (Collider collider)
	{

		if (collider.tag == "Player") { // player moving through door
			Player player = collider.GetComponent<Player> (); // player entering or leaving

			player.transform.position = target.transform.position;
			player.transform.Translate (Vector3.forward * 5);
			player.SwitchCamera(target.camera);


		}

	}

}
