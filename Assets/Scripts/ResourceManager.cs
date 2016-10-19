using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

	private Hashtable resources;
	private Text counter;


	// Use this for initialization
	void Start () {
		resources = new Hashtable();
		resources.Add("gold", 1);
		resources.Add("wood", 2);
		resources.Add("water", 3);
		resources.Add("holywater", 4);
		resources.Add("mushroom", 5);
		resources.Add("iron", 6);
		counter = GameObject.Find("Resource Text").GetComponent<Text>();
		UpdateCounter();
	}
	
	public void AddResource (string resource, int num)
	{
		int currentValue = (int) resources [resource];
		int newValue = currentValue + num;
		if (newValue < 0) {
			newValue = 0;
		}
		resources[resource] = newValue;
		UpdateCounter();
		print("Updating resouces");
	}

	public void SubtractResource (string resource, int num)
	{
		int currentValue = (int) resources[resource];
		int newValue = currentValue - num;
		if (newValue < 0) {
			newValue = 0;
		}
		resources[resource] = newValue;
		UpdateCounter();
	}

	private void UpdateCounter(){
		print(resources["gold"]);
		counter.text = (resources["gold"] + "\n" +
			resources["wood"] + "\n" +
			resources["water"] + "\n" +
			resources["holywater"] + "\n" +
			resources["mushroom"] + "\n" +
			resources["iron"]);
	}
}
