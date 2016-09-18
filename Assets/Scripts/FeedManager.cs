using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeedManager : MonoBehaviour {

	private string[] lines;
	private Text feed;


	// Use this for initialization
	void Start () {
		lines = new string[5];
		lines[0] = "Welcome to Twisted Streets!";
		feed = GameObject.FindGameObjectWithTag("Feed").GetComponent<Text>();
		Refresh();
	}
	
	public void Write (string text)
	{

		for (int i = 4; i > 0; i--) {

			lines[i] = lines [i-1];
			Debug.Log("Line " + i + " is now " + lines[i]);

		}

		lines[0] = text;

		Refresh();

	}

	private void Refresh ()
	{
		feed.text = "";

		for (int i = 0; i < 5; i++) {

			feed.text += lines[i] + "\n";

		}

	}

}
