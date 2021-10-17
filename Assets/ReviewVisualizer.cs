using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewVisualizer : MonoBehaviour {
    
    public TextMeshProUGUI text;
    public Image background;
    public Sprite likeSprite;
    public Sprite dislikeSprite;
    
    public void SetText(string message) {
        text.text = message;
    }

    public void SetScore(bool like) {

        background.sprite = like ? likeSprite : dislikeSprite;

    }
}
