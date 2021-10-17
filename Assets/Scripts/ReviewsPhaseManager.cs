using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ReviewsPhaseManager:MonoBehaviour {

	public Image nextButtonImage;
	public Button nextButton;

	private List<ReviewVisualizer> currentReviewVisualizers = new List<ReviewVisualizer>();
	
	public GameObject reviewsContainer;

	public GameObject reviewVisualizerTemplate;
	
	public event Action EndReviewsPhase;

	public AnimationCurve fadeInCurve;
	public AnimationCurve fadeOutCurve;

	[ContextMenu("Init")]
	public void Init(List<Review> reviews) {
		
		GenerateReviews(reviews);

		nextButtonImage.color = Color.clear;
		((RectTransform)transform).localScale = Vector3.one*0.00001f;
		((RectTransform)transform).DOScale(1, 1f).SetEase(fadeInCurve).OnComplete(() => {
			
			nextButtonImage.DOColor(Color.white, 1).OnComplete(() => {
				nextButton.interactable = true;
			});
		});
	}

	public void EndPhaseButton() {

		FadeOut();
		
	}

	private void FadeOut() {
		
		nextButton.interactable = true;
		nextButtonImage.DOColor(Color.clear, 0.4f).OnComplete(() => {

			((RectTransform)transform).DOScale(0.00001f, 1f).SetEase(fadeOutCurve).OnComplete(() => {

				gameObject.SetActive(false);
				
				EndReviewsPhase?.Invoke();
			});

		});
		
		
	}

	private void GenerateReviews(List<Review> reviews) {

		reviewVisualizerTemplate.SetActive(false);
		
		foreach (ReviewVisualizer currentReviewVisualizer in currentReviewVisualizers) {
			Destroy(currentReviewVisualizer.gameObject);
		}
		currentReviewVisualizers.Clear();
		
		foreach (Review review in reviews) {

			GameObject newReview = Instantiate(reviewVisualizerTemplate,reviewsContainer.transform);
			newReview.SetActive(true);
			var reviewVisualizer = newReview.GetComponent<ReviewVisualizer>();
			currentReviewVisualizers.Add(reviewVisualizer);

			reviewVisualizer.SetText(review.text);
			reviewVisualizer.SetScore(review.like);

			if (review.like) {
				GameController.instance.goodReviews++;
			}
			else {
				GameController.instance.badReviews++;
			}


		}
	}


	
}