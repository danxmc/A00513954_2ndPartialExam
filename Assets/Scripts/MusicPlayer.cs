using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    MusicPlayer instance;

	// Use this for initialization
	void Start () {
        // If the instance is not null, then we 
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
	}
	
}
