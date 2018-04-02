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
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
