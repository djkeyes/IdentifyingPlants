using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SearchInView : MonoBehaviour {
	public AudioClip[] clips;
	private AudioSource audioSource;
	public GameObject label;
	private DetectInView dv;
	private PlantAttribute searchCritera;
	private int lastAmount;
	private bool changed;
	public bool isAvailable;

	// Use this for initialization
	void Start () {
		dv = (DetectInView) GameObject.FindGameObjectWithTag ("MainCamera").GetComponent ("DetectInView");
		audioSource = gameObject.AddComponent<AudioSource> ();
		isAvailable = true;
		label.guiText.text = "None";
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAvailable) {
			int amount = dv.NumberOf (searchCritera);
			if(IsChanged(amount) || changed){
				if(audioSource.isPlaying || changed){
					audioSource.Stop();	
					SetClip(amount);
				}	
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
			audioSource.clip = clips [0];
		} else if (amount < 3) {
			audioSource.clip = clips [1];
		} else if (amount < 5) {
			audioSource.clip = clips [2];
		} else {
			audioSource.clip = clips [3];
		}
	}

	public void Stop(){
		isAvailable = true;
		audioSource.Stop ();
		lastAmount = 0;
		changed = true;
		label.guiText.text = "None";
	}

	public PlantAttribute GetCriteria(){
		return searchCritera;
	}

	public void SetCritera(PlantAttribute criteria){
		isAvailable = false;
		searchCritera = criteria;
		audioSource.Stop ();
		lastAmount = 0;
		label.guiText.text = criteria.ToString();
		changed = true;
	}
}
