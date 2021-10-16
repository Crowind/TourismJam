using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailsScreen : MonoBehaviour {

    public Image photo;
    public TextMeshProUGUI destinationName;
    public TextMeshProUGUI description;

    public Destination destination;
    
    public TripPlanner tripPlanner;
    
    public void Init(Destination newDestination) {

        destination = newDestination;
        photo.sprite = newDestination.photo;
        destinationName.text = newDestination.name;
        description.text = newDestination.description;

    }


    public void AddDestination() {
        
        tripPlanner.AddDestination(destination);
        gameObject.SetActive(false);
        
    }
}
