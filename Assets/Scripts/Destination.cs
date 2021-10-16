

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Destinations/Create Destination", fileName = "Destination", order = 0)]

public class Destination : ScriptableObject {

	public Sprite preview;
	public Sprite photo;
	public string description;

	public int capacity;

	public List<LocationTag> locationTags;


}



public enum LocationTag {
	
	Avventura = 0,
	Relax = 1,
	Romantico = 2,
	Cultura = 3,
	Mare = 4,
	Montagna = 5,
	Natura = 6
	
}