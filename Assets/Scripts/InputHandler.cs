using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {
	private DetectInView dv;
	private Hashtable recentNames;
	private bool namesMode;
	public GameObject modeLabel;
	private string MODE_NONE = "Mode : None";
	private string MODE_NAMES = "Mode : Names";
	private string MODE_DETAILS = "Mode : Details";


	// Use this for initialization
	void Start () {
		dv = (DetectInView) GameObject.FindGameObjectWithTag ("MainCamera").GetComponent ("DetectInView");
		recentNames = new Hashtable ();
		namesMode = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("2") && namesMode){
			namesMode = false;
			audio.Stop();
		} else if (!audio.isPlaying) {
			modeLabel.guiText.text = MODE_NONE;
			if(namesMode){
				modeLabel.guiText.text = MODE_NAMES;
				PlantClassification[] plants = dv.GetPlants();
				bool foundNewName = false;
				for(int i = 0; i < plants.Length; i++){
					if(!recentNames.Contains(plants[i].name)){
						audio.clip = plants[i].nameSound;
						audio.Play();
						recentNames.Add(plants[i].name, plants[i].name);
						foundNewName = true;
						break;
					}
				}
				if(!foundNewName){
					recentNames.Clear();
				}
			} else {
				if (Input.GetKeyDown ("1")) {
					audio.clip = dv.ClosestPlant ().detailsSound;
					audio.Play ();
					modeLabel.guiText.text = MODE_DETAILS;
				} else if(Input.GetKeyDown("2")){
					namesMode = true;
				}
			}
		}
	}
}
