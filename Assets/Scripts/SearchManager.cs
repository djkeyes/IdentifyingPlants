using UnityEngine;
using System.Collections;

public class SearchManager : MonoBehaviour
{
	public GameObject guitar;
	public GameObject violin;
	public GameObject marimba;
	
	private SearchInView guitarSearch;
	private SearchInView violinSearch;
	private SearchInView marimbaSearch;


	// Use this for initialization
	void Start ()
	{
		guitarSearch = (SearchInView) guitar.GetComponent ("SearchInView");
		violinSearch = (SearchInView) violin.GetComponent ("SearchInView");
		marimbaSearch = (SearchInView) marimba.GetComponent ("SearchInView");

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public bool HasAvailableSearches(){
		return guitarSearch.isAvailable || violinSearch.isAvailable || marimbaSearch.isAvailable;
	}

	public bool AddSearch(PlantAttribute criteria){
		if(!DoingSearch(criteria)){
			SearchInView available = GetAvailableSearch();
			if(available){
				available.SetCritera(criteria);
				return true;
			}
		}
		return false;
	}

	public bool RemoveSearch(PlantAttribute criteria){
		SearchInView current = DoingSearch (criteria);
		if(current){
			current.Stop();
			return true;
		}
		return false;
	}

	private SearchInView GetAvailableSearch(){
		if(guitarSearch.isAvailable){
			return guitarSearch;
		} else if(marimbaSearch.isAvailable){
			return marimbaSearch;
		} else if(violinSearch.isAvailable){
			return violinSearch;
		} else {
			return null;
		}
	}

	public SearchInView DoingSearch(PlantAttribute criteria){
		if (!guitarSearch.isAvailable && guitarSearch.GetCriteria ().Equals (criteria)) {
			return guitarSearch;
		} else if (!violinSearch.isAvailable && violinSearch.GetCriteria ().Equals (criteria)) {
			return violinSearch;
		} else if (!marimbaSearch.isAvailable && marimbaSearch.GetCriteria ().Equals (criteria)) {
			return marimbaSearch;
		} else {
			return null;
		}
	}
}

