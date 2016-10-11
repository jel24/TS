using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] songs;

	private int track;
	private AudioSource source;
	private AudioClip currentSong;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		track = Random.Range(0, songs.Length);
		source.clip = songs[track];
		print("Playing song " + track + ".");
		source.Play();
		Invoke("songSwitch", songs[track].length);
	}

	private void songSwitch ()
	{
		int newTrack = track;
		while (newTrack == track) {
			newTrack = Random.Range(0, songs.Length);
		}
		source.clip = songs[newTrack];
		track = newTrack;
		print("Playing song " + track + ".");
		source.Play();
		Invoke("songSwitch", songs[track].length);
	}

}
