using System;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientsPhaseManager : StateMachine<ClientsPhaseManager> {


	public SpriteRenderer clientSprite;
	public SpriteRenderer balloonSprite;
	public TextMeshPro balloonText;

	public Button flyersButton;
	public FlyersOutline flyersOutline;
	
	
	private Queue<Client> clients = new Queue<Client>();
	public Client currentClient;

	public event Action EndClientsPhase;

	protected override void Awake() {
		base.Awake();

		var clientArrives = new ClientArrives(this);
		var clientInteraction = new ClientInteraction(this);
		possibleStates.Add(typeof(ClientArrives),clientArrives);
		possibleStates.Add(typeof(ClientInteraction),clientInteraction);

		flyersButton.interactable = false;
		flyersOutline.Stop();
		
		state = null;

	}

	public void Init(List<Client> newClients) {

		clients.Clear();
		foreach (Client newClient in newClients) {
			
			clients.Enqueue(newClient);
		}

		currentClient = clients.Dequeue();
		
		ChangeState(typeof(ClientArrives));
		//EndClientsPhase?.Invoke();
		
	}
}





public class ClientPhaseState : State<ClientsPhaseManager> {

	protected ClientPhaseState(ClientsPhaseManager machine) : base(machine) { }

	public override void HandleInput() {
	}

	public override void Update() {
	}

	public override void FixedUpdate() {
	}

	public override void Init() {
	}
	
}


public class ClientArrives : ClientPhaseState {




	public ClientArrives(ClientsPhaseManager machine) : base(machine) {

	}

	public override void Init() {
		base.Init();
		
		machine.balloonSprite.color= Color.clear;
		machine.clientSprite.color = Color.clear;
		machine.clientSprite.sprite = machine.currentClient.sprite;
		
		machine.balloonText.text = "";
		
		machine.clientSprite.DOColor(Color.white, 1.5f).OnComplete(() => {
			
			machine.balloonSprite.color = Color.white;
			machine.StartCoroutine(TypeWriterCoroutine(machine.currentClient.dialogue1,0.06f));

		});

	}

	private IEnumerator TypeWriterCoroutine(string textToWrite, float period) {

		float timePassed=0;

		foreach (char c in textToWrite) {
			while (timePassed < period) {
				timePassed += Time.deltaTime;
				yield return null;
			}
			
			timePassed = 0;
			machine.balloonText.text += c;

		}
		machine.ChangeState(typeof(ClientInteraction));
	}

}


public class ClientInteraction : ClientPhaseState {
	public ClientInteraction(ClientsPhaseManager machine) : base(machine) {
		
	}

	public override void Init() {
		base.Init();
		machine.flyersButton.interactable = true;
		machine.flyersOutline.Go();


	}
}