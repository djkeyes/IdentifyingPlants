using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {
	private enum Mode {
		none, names, details
	}
	private DetectInView dv;
	private Hashtable recentNames;
	private Mode mode;
	public GameObject modeLabel;
	public GameObject plantTexture;


	// Use this for initialization
	void Start () {
		dv = (DetectInView) GameObject.FindGameObjectWithTag ("MainCamera").GetComponent ("DetectInView");
		recentNames = new Hashtable ();
		mode = Mode.none;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (GetModeInput(Mode.details)) && mode.Equals (Mode.details)) {
			mode = Mode.none;
			audio.Stop();
		} else if(Input.GetKeyDown(GetModeInput(Mode.names)) && mode.Equals(Mode.names)){
			mode = Mode.none;
			audio.Stop();
		} else if (!audio.isPlaying) {
			if(mode.Equals(Mode.names)){
				PlantClassification[] plants = dv.GetPlants();
				plantTexture.guiTexture.texture = null;
				for(int i = 0; i < plants.Length; i++){
					if(!recentNames.Contains(plants[i].name)){
						audio.clip = plants[i].nameSound;	
						plantTexture.guiTexture.texture = plants[i].plantImage;
						audio.Play();
						recentNames.Add(plants[i].name, plants[i].name);
						break;
					}
				}
			} else {
				SwitchMode ();
			}
		}
		modeLabel.guiText.text = GetModeText();
	}

	private void SwitchMode ()
	{
		if (Input.GetKeyDown (GetModeInput (Mode.details))) {
			PlantClassification plant = dv.ClosestPlant ();
			if (plant) {
				audio.clip = plant.detailsSound;
				plantTexture.guiTexture.texture = plant.plantImage;
				audio.Play ();
				mode = Mode.details;
			}
		}
		else if (Input.GetKeyDown (GetModeInput (Mode.names))) {
			mode = Mode.names;
		}
	}

	private string GetModeInput(Mode mode){
		switch(mode){
		case Mode.details:
			return "1";
		case Mode.names:
			return "2";
		case Mode.none:
		default:
			return "";
		}
	}

	private string GetModeText(){
		switch(mode){
		case Mode.details:
			return "Mode : details";
		case Mode.names:
			return "Mode : names";
		case Mode.none:
		default:
			recentNames.Clear();
			plantTexture.guiTexture.texture = null;
			return "Mode : none";
		}
	}
}
