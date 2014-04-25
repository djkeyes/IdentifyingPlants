using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SearchInView : MonoBehaviour {
	public AudioClip[] clips;
	private AudioSource audioSource;
	public GameObject label;
	public GameObject image;
	private DetectInView dv;
	public PlantAttribute searchCritera;
	private int lastAmount;
	private bool changed;
	public bool isAvailable;
	private bool passive;

	// Use this for initialization
	void Start () {
		dv = (DetectInView) GameObject.FindGameObjectWithTag ("MainCamera").GetComponent ("DetectInView");
		audioSource = gameObject.AddComponent<AudioSource> ();
		isAvailable = true;
		label.guiText.color = Color.gray;
		image.guiTexture.texture = null;
		passive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAvailable) {
			PlantClassification closest = dv.ClosestPlant(searchCritera);
			if(closest){
				image.guiTexture.texture = closest.plantImage;
			} else {
				image.guiTexture.texture = null;
			}
			int amount = dv.NumberOf (searchCritera);
			if(IsChanged(amount) || changed){
				if(audioSource.isPlaying){
					audioSource.Stop();
				}	
				audioSource.volume = dv.ClosestVolume(searchCritera);
				SetClip(amount);
				audioSource.Play();
				changed = false;
			} else {
				if(!audioSource.isPlaying){
					audioSource.Play();
				}
			}
			lastAmount = amount;
		}		
	}

	private bool IsChanged(int amount){
		if (amount < 1 && lastAmount < 1) {
			return false;
		} else if (amount < 3 && lastAmount < 3 && amount > 0 && lastAmount > 0) {
			return false;
		} else if (amount < 5 && lastAmount < 5 && amount > 2 && lastAmount > 2) {
			return false;
		} else if(amount > 4 && lastAmount > 4){
			return false;
		} else {
			return true;
		}
	}

	private void SetClip(int amount){
		if (amount < 1) {
			if(passive){
				audioSource.clip = null;
			} else {
				audioSource.clip = clips [0];
			}
		} else if (amount < 3) {
			audioSource.clip = clips [1];
		} else if (amount < 5) {
			audioSource.clip = clips [2];
		} else {
			audioSource.clip = clips [3];
		}
	}

	public void StopSearch(){
		isAvailable = true;
		audioSource.Stop ();
		lastAmount = 0;
		changed = true;
		image.guiTexture.texture = null;
		label.guiText.color = Color.gray;
	}

	public PlantAttribute GetCriteria(){
		return searchCritera;
	}

	public void StartSearch(bool isPassive){
		passive = isPassive;
		isAvailable = false;
		audioSource.Stop ();
		lastAmount = 0;
		if(passive){
			label.guiText.color = Color.blue;
		} else {
			label.guiText.color = Color.green;
		}
		changed = true;
	}

	public bool GetPassive(){
		return passive;
	}
}
