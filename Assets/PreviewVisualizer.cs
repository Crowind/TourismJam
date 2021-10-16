using System;
using UnityEngine;
using UnityEngine.UI;

public class PreviewVisualizer : MonoBehaviour {

	public Destination destination;
	public Button openDetails;

	public DetailsScreen detailsScreen;
	
	private void Awake() {
	
		openDetails.onClick.AddListener(() => {

			detailsScreen.Init(destination);
			detailsScreen.gameObject.SetActive(true);

		});
		
	}

	public void Lock() {

		openDetails.interactable = false;

	}

	public void Unlock() {

		openDetails.interactable = destination.capacity > 0;
	}
}