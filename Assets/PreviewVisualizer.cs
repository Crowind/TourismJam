using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewVisualizer : MonoBehaviour {

	public Destination destination;
	public Button openDetails;

	public DetailsScreen detailsScreen;
	public Image preview;
	public bool added;
	public TextMeshProUGUI capacity;
	
	private void Awake() {

		preview.sprite = destination.preview;
		openDetails.onClick.AddListener(() => {

			detailsScreen.Init(destination);
			detailsScreen.gameObject.SetActive(true);

		});
		
	}

	public void Lock(bool isBeingAdded) {//TODO Capacity

		openDetails.interactable = false;

		added |= isBeingAdded;

	}

	public void Unlock(bool isBeingRemoved) {

		added &= !isBeingRemoved;
		
		openDetails.interactable = destination.capacity > 0 && !added;
	}
}