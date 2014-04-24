using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SearchInView : MonoBehaviour {
	
	public AudioClip[] clips;
	public AudioClip medicineClip;
	public AudioSource medicineSource;
	// Use this for initialization
	void Start () {
		medicineSource = gameObject.AddComponent<AudioSource> ();
		clips = new AudioClip[4];
	}
	
	// Update is called once per frame
	void Update () {
		DetectInView dv = (DetectInView) GameObject.FindGameObjectWithTag ("MainCamera").GetComponent ("DetectInView");
		int medicinePlants = dv.NumberOf (PlantClassification.PlantAttribute.medicine);
		//Debug.Log ("Number of medicine plants: " + medicinePlants);
		if (medicinePlants > 0) {
			medicineSource.clip = clips [0];
		} else if (medicinePlants > 2) {
			medicineSource.clip = clips [1];
		} else if (medicinePlants > 4) {
			medicineSource.clip = clips [2];
		} else {
			medicineSource.clip = clips[3];
		}
		
	}
	
	
	
}
