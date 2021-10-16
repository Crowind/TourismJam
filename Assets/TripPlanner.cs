using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripPlanner : MonoBehaviour {


	public List<PreviewVisualizer> previewVisualizers;

	public Button confirmButton;

	public Image recapImage1;
	public Image recapImage2;
	public Image recapImage3;
	
	
	private List<Destination> destinations = new List<Destination>(3);

	private void Awake() {
		
		confirmButton.interactable = false;
		gameObject.SetActive(false);
	}

	public void AddDestination(Destination destination) {

		if (destinations.Count >= 3) {
			return;
		}
		destinations.Add(destination);

		previewVisualizers.Find(visualizer => visualizer.destination = destination)?.Lock();
		
		if (destinations.Count == 3) {

			foreach (PreviewVisualizer previewVisualizer in previewVisualizers) {

				previewVisualizer.Lock();
			}
		}
		UpdatePreviewTrip();
	}

	public void RemoveDestination(int index) {
		if (previewVisualizers.Count == 3) {
			foreach (PreviewVisualizer previewVisualizer in previewVisualizers) {

				previewVisualizer.Unlock();
			}
		}
		Destination destination = destinations[index];

		previewVisualizers.Find(visualizer => visualizer.destination = destination)?.Unlock();
		
		destinations.RemoveAt(index);
		
		UpdatePreviewTrip();
		
	}

	private void UpdatePreviewTrip() {


		confirmButton.interactable = destinations.Count > 0;
		
		recapImage1.sprite = destinations.Count > 0 ? destinations[0].preview : null;
		recapImage1.color = destinations.Count > 0 ? Color.white : Color.clear;
		
		recapImage2.sprite = destinations.Count > 1 ? destinations[1].preview : null;
		recapImage2.color = destinations.Count > 1 ? Color.white : Color.clear;

		recapImage3.sprite = destinations.Count > 2 ? destinations[2].preview : null;
		recapImage3.color = destinations.Count > 2 ? Color.white : Color.clear;

	}

	public void ConfirmPlan() {
		
		//TODO
	}

}