using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour {

    public TextMeshProUGUI text;
    
    private void Start() {

        text.text = GameController.instance.goodReviews + "/" + (GameController.instance.goodReviews + GameController.instance.badReviews);


    }

}
