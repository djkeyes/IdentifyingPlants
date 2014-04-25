using UnityEngine;
using System.Collections;

public class SearchManager : MonoBehaviour
{
	public GameObject guitar;
	public GameObject violin;
	public GameObject marimba;
	public GameObject trumpet;
	public GameObject piano;
	public GameObject numSearchesLabel;

	private SearchInView guitarSearch;
	private SearchInView violinSearch;
	private SearchInView marimbaSearch;
	private SearchInView trumpetSearch;
	private SearchInView pianoSearch;
	private int numSearches;

	// Use this for initialization
	void Start ()
	{
		guitarSearch = (SearchInView) guitar.GetComponent ("SearchInView");
		violinSearch = (SearchInView) violin.GetComponent ("SearchInView");
		marimbaSearch = (SearchInView) marimba.GetComponent ("SearchInView");
		pianoSearch = (SearchInView) piano.GetComponent ("SearchInView");
		trumpetSearch = (SearchInView) trumpet.GetComponent ("SearchInView");
	}

	// Update is called once per frame
	void Update ()
	{
		numSearchesLabel.guiText.text = "Searches (" + numSearches + " / 3)";
	}

	public bool HasAvailableSearches(){
		return numSearches < 3;
	}

	public void AddSearch(PlantAttribute criteria){
		SearchInView search = GetOnCriteria (criteria);
		if(search.isAvailable){
			search.StartSearch();
			numSearches++;
		}
	}

	public void RemoveSearch(PlantAttribute criteria){
		SearchInView search = GetOnCriteria (criteria);
		if(!search.isAvailable){
			search.StopSearch();
			numSearches--;
		}
	}

	private SearchInView GetOnCriteria(PlantAttribute criteria){
		switch(criteria){
		case PlantAttribute.building_material:
			return guitarSearch;
		case PlantAttribute.firewood:
			return marimbaSearch;
		case PlantAttribute.medicine:
			return pianoSearch;
		case PlantAttribute.poison:
			return trumpetSearch;
		case PlantAttribute.food:
		default:
			return violinSearch;
		}
	}

	public bool DoingSearch(PlantAttribute criteria){
		SearchInView search = GetOnCriteria (criteria);
		return !search.isAvailable;
	}
}

