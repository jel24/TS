using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public Camera spawnCamera;

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawIcon(transform.position, "startPos.png", true);
		Gizmos.DrawWireCube(transform.position, new Vector3(1,2,1));
	}
	
}
