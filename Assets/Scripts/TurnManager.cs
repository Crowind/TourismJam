using System;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public class TurnManager : StateMachine<TurnManager> {

	public int currentDay;
	
	public List<Review> pendingReviews;
	public List<Client> pendingClients;
	
	public ReviewsPhaseManager reviewsPhaseManager;
	public ClientsPhaseManager clientsPhaseManager;
	public DayChangePhaseManager dayChangePhaseManager;
	
	protected override void Awake() {
		base.Awake();
		
		var reviewsPhaseState = new ReviewsPhase(this,reviewsPhaseManager);
		var clientPhaseState = new ClientPhase(this,clientsPhaseManager);
		var dayEndState = new DayChangePhase(this,dayChangePhaseManager);
		
		possibleStates.Add(typeof(ReviewsPhase),reviewsPhaseState);
		possibleStates.Add(typeof(ClientPhase),clientPhaseState);
		possibleStates.Add(typeof(DayChangePhase), dayEndState);

		ChangeState(typeof(ClientPhase));
	}

	private void Start() {

		pendingClients = GameController.instance.GetCurrentDayClients();
	}

}

public class TurnPhase:State<TurnManager> {
	protected TurnPhase(TurnManager machine) : base(machine) { }

	public override void HandleInput() {
		
	}

	public override void Update() {
	}

	public override void FixedUpdate() {
	}

	public override void Init() {
	}
}


public class ReviewsPhase : TurnPhase {
	private readonly ReviewsPhaseManager reviewsPhaseManager;
	private List<Review> reviews;

	public ReviewsPhase(TurnManager machine, ReviewsPhaseManager reviewsPhaseManager) : base(machine) {
		this.reviewsPhaseManager = reviewsPhaseManager;
		reviewsPhaseManager.EndReviewsPhase += () => {
			machine.ChangeState(typeof(ClientPhase));
		};
	}
	
	public override void Init() {
		base.Init();
		reviews = machine.pendingReviews;
		
		if (reviews.Count == 0) {
			machine.ChangeState(typeof(ClientPhase));
		}
		else {
			reviewsPhaseManager.gameObject.SetActive(true);
			reviewsPhaseManager.Init(reviews);
		}
	}

}


public class ClientPhase : TurnPhase {
	private readonly ClientsPhaseManager clientsPhaseManager;
	private List<Client> clients;

	public ClientPhase(TurnManager machine, ClientsPhaseManager clientsPhaseManager) : base(machine) {
		this.clientsPhaseManager = clientsPhaseManager;

		clientsPhaseManager.EndClientsPhase += () => {
			machine.ChangeState(typeof(DayChangePhase));
		};

	}

	public override void Init() {
		base.Init();
		clients = machine.pendingClients;
		clientsPhaseManager.gameObject.SetActive(true);
		clientsPhaseManager.Init(clients);
		
	}
}


public class DayChangePhase : TurnPhase {
	private readonly DayChangePhaseManager dayChangePhaseManager;
	private int dayEnded;

	public DayChangePhase(TurnManager machine, DayChangePhaseManager dayChangePhaseManager) : base(machine) {
		this.dayChangePhaseManager = dayChangePhaseManager;
		dayChangePhaseManager.DayChangeDone += () => {
			
			machine.ChangeState(typeof(ReviewsPhase));
		};


	}

	public override void Init() {
		base.Init();
		dayEnded = machine.currentDay++;
		dayChangePhaseManager.gameObject.SetActive(true);
		dayChangePhaseManager.ChangeDay(dayEnded);
	}

}

[System.Serializable]
public class Client {

	public Sprite sprite;
	public string dialogue1;
	public string likeDialogue;

	public TripLenght preferredTripLenght;
	public List<LocationTag> favouriteTags;
	public List<LocationTag> hatedTags;
}

public enum TripLenght {
	Breve=1,
	Medio=2,
	Lungo=3
}

[System.Serializable]
public class Review {
	public string text;
	public bool like;

	public Review(bool like, string text) {
		this.like = like;
		this.text = text;
	}
}