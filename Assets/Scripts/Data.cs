using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {
    public static Data instance = null;

    public int score;
    public int stage;

    protected LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        score = 0;
        stage = 1;
    }

    /// <summary>
    /// Creates solely one instance of the Data object
    /// </summary>
    private void Awake()
    {
        // Instantiates the Data object
        if (instance == null)
        {
            instance = this;
        }
        // Else destroy it
        else if(instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
