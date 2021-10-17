using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewVisualizer : MonoBehaviour {

	public Destination destination;
	public Button openDetails;

	public DetailsScreen detailsScreen;
	public Image preview;
	public bool added;
	public int taken;
	public TextMeshProUGUI capacity;
	
	private void Awake() {

		preview.sprite = destination.preview;
		openDetails.onClick.AddListener(() => {

			detailsScreen.Init(destination);
			detailsScreen.gameObject.SetActive(true);

		});
		
	}

	public void Lock(bool isBeingAdded) {

		openDetails.interactable = false;

		added |= isBeingAdded;

		if (isBeingAdded) {

			Dictionary<Destination, int> dictionary = GameController.instance.days[GameController.instance.day].destinations;

			if (dictionary.ContainsKey(destination)) {
				dictionary[destination] += 1;
			}
			else {
				dictionary.Add(destination,1);
			}
			taken = dictionary[destination];
		}

		UpdateCapacity();

	}

	public void Unlock(bool isBeingRemoved) {

		added &= !isBeingRemoved;
		
		openDetails.interactable = destination.capacity > 0 && !added;
		
		if (isBeingRemoved) {

			Dictionary<Destination, int> dictionary = GameController.instance.days[GameController.instance.day].destinations;
			dictionary[destination] -= 1;
			taken = dictionary[destination];
		}
		
		UpdateCapacity();
	}

	public void Init() {
		added = false;
		Unlock(false);
		taken = 0;
		UpdateCapacity();
	}

	private void UpdateCapacity() {
		capacity.text = (destination.capacity - taken) + "/" + destination.capacity;
	}
}