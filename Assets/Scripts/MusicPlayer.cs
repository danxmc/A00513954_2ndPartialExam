using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    /// <summary>
    /// Instance of the MusicPlayer
    /// </summary>
    private static MusicPlayer instance = null;

    /// <summary>
    /// Creates solely one instance of the MusicPlayer
    /// </summary>
    private void Awake()
    {
        // Instantiates the MusicPlayer
        if (instance == null)
        {
            instance = this;
        }
        // Else destroy it
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
