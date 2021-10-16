using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewVisualizer : MonoBehaviour {
    
    public TextMeshProUGUI text;
    public Image background;
    public Image likeImage;
    
    public void SetText(string message) {
        text.text = message;
    }

    public void SetScore(bool like) {

        likeImage.enabled = like;
        background.color = like ? Color.green : Color.red;

    }
}
