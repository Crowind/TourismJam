using System;
using System.Collections.Generic;
using DesignPatterns;
using TMPro;
using UnityEngine;

public class ClientsPhaseManager : StateMachine<ClientsPhaseManager> {


	public SpriteRenderer clientSprite;
	public SpriteRenderer balloonSprite;
	public TextMeshPro balloonText;
	
	public List<Client> clients;

	public event Action EndClientsPhase;
	
	
	
	public void Init() {
		//TODO all
		
		Debug.Log("Clients");
		
		EndClientsPhase?.Invoke();
	}
}





public class ClientPhaseState : State<ClientsPhaseManager> {

	public ClientPhaseState(ClientsPhaseManager machine) : base(machine) { }

	public override void HandleInput() {
	}

	public override void Update() {
	}

	public override void FixedUpdate() {
	}

	public override void Init() {
	}

	public virtual void GetInput() { }
}


public class ClientArrives : ClientPhaseState {

	private SpriteRenderer clientSprite;
	private SpriteRenderer balloonSprite;
	private TextMeshPro balloonText;


	public ClientArrives(ClientsPhaseManager machine, TextMeshPro balloonText, SpriteRenderer balloonSprite, SpriteRenderer clientSprite) : base(machine) {
		this.balloonText = balloonText;
		this.balloonSprite = balloonSprite;
		this.clientSprite = clientSprite;

	}

	public override void Init() {
		base.Init();
		
		balloonSprite.color= Color.clear;
		clientSprite.color = Color.clear;
		

	}

}