using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
    /// <summary>
    /// Public Texts in the canvas', used to display the total score and stage
    /// </summary>
    public Text scoreText;
    public Text stageText;

    protected Data data;

    // Use this for initialization
    void Start () {
        data = GameObject.Find("Data").GetComponent<Data>();
        scoreText.text = data.score.ToString();
        stageText.text = data.stage.ToString();

        data.score = 0;
        data.stage = 0;
    }
}
