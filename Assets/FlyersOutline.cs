using UnityEngine;
using UnityEngine.UI;

public class FlyersOutline : MonoBehaviour {


	private Image flyersOutline;
	private bool stop;

	private void Awake() {
		flyersOutline = GetComponent<Image>();
	}

	private void Update() {
		if (stop) {
			return;
		}
		flyersOutline.color = Color.Lerp(Color.white, Color.clear, 0.5f+Mathf.Sin(Time.time*2)/2);
	}

	public void Stop() {
		stop = true;
		flyersOutline.color = Color.clear;
	}

	public void Go() {
		stop = false;
	}
}