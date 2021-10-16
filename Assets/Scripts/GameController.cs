using System;
using System.Collections.Generic;
using DesignPatterns;

public class GameController : Singleton<GameController> {

	
	public List<Day> days;
	
	public int day = 0;

	public List<Client> GetCurrentDayClients() {

		return days[day].clients;

	}

	public void GenerateReview(Client currentClient, List<Destination> destinations) { 
		days[day].reviews.Add(Like(currentClient,destinations) );
		
	}
	
	

	private static Review Like(Client client, List<Destination> destinations ) {

		bool like = true;
		string comment = "";

		bool rightLength = client.preferredTripLenght == (TripLenght)destinations.Count;

		if (!rightLength) {
			comment = (client.preferredTripLenght < (TripLenght)destinations.Count) ? "Too long! I needed a shorter trip!" : "I needed to spend more time on this vacation...";
		}

		like &= rightLength;
		
		
		foreach (LocationTag clientFavouriteTag in client.favouriteTags) {

			bool exists = destinations.Exists(destination => destination.locationTags.Contains(clientFavouriteTag));
			like &= exists;
			if (!exists) {
				comment = AbsentTagToComment(clientFavouriteTag);
			}

		}
		foreach (LocationTag clientFavouriteTag in client.favouriteTags) {

			bool exists = destinations.Exists(destination => destination.locationTags.Contains(clientFavouriteTag));
			like &= !exists;
			if (exists) {
				comment = PresentTagToComment(clientFavouriteTag);
			}

		}
		


		if (like) {
			comment = client.likeDialogue;
		}

		return new Review(like,comment);
	}

	private static string AbsentTagToComment(LocationTag tag) {

		return tag switch
		{
			LocationTag.Avventura => "I just wanted an adventure...",
			LocationTag.Relax => "Too chaotic for me.",
			LocationTag.Romantico => " Not ideal for a couple.",
			LocationTag.Cultura => "I haven't learned much...",
			LocationTag.Mare => "Where is the sea?",
			LocationTag.Montagna => " I was looking for some mountains",
			LocationTag.Natura => "I asked for a natural park",
			_ => throw new ArgumentOutOfRangeException(nameof(tag), tag, null)
		};

	}

	private static string PresentTagToComment(LocationTag tag) {

		return tag switch
		{
			LocationTag.Avventura => "I'm not suited for these reckless things.",
			LocationTag.Relax => "Boring, I wanted movement.",
			LocationTag.Romantico => "Couple place. Not my case :(",
			LocationTag.Cultura => "I wanted a carefree vacation...",
			LocationTag.Mare => "I've never liked the sea.",
			LocationTag.Montagna => "I'm not interested in the mountains",
			LocationTag.Natura => "Too much nature. I was looking for something else",
			_ => throw new ArgumentOutOfRangeException(nameof(tag), tag, null)
		};

	}
}


[System.Serializable]
public class Day {

	public List<Client> clients;
	[NonSerialized]
	public List<Review> reviews = new List<Review>();
}