using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayChangePhaseManager : MonoBehaviour {

	public TextMeshProUGUI text;
	public Image background;
	
	public event Action DayChangeDone;

	public void ChangeDay(int dayEnded) {

		GameController.instance.ChangeDay();
		
		
		background.color = Color.clear;

		text.text = "Day " + ++dayEnded;
		text.color = Color.clear;

		background.DOColor(new Color(0, 0, 0, 0.5f), 4f).SetEase(Ease.OutQuart).OnComplete(

			() => {
				background.DOColor(Color.clear, 4f).SetEase(Ease.InQuart).OnComplete(() => {
					DayChangeDone?.Invoke();
					gameObject.SetActive(false);
				});
			});

		text.DOColor(Color.white, 2.5f).OnComplete(() => {
			text.DOColor(Color.clear, 1.5f).OnComplete(() => {
				text.text = "Day " + ++dayEnded;
				text.DOColor(Color.white, 1.5f).OnComplete(() => {
					text.DOColor(Color.clear, 2.5f);
				});
			});
			text.transform.DOLocalMoveY(250, 1.5f).OnComplete(() => {
				text.transform.position -= Vector3.up * 500;
				text.transform.DOLocalMoveY(0, 1.5f);
			});
		});


	}
}