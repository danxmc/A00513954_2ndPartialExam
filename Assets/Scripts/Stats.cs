using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
    public Text scoreText;
    public Text stageText;

    protected Data data;

    // Use this for initialization
    void Start () {
        data = GameObject.Find("Data").GetComponent<Data>();
        scoreText.text = data.score.ToString();
        stageText.text = data.stage.ToString();
    }
}
