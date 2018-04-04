using UnityEngine;
using UnityEngine.SceneManagement;		// Required to switch scenes
using System.Collections;

public class LevelManager : MonoBehaviour {

    /// <summary>
    /// Calls for a scene with the SceneManager by it's name
    /// </summary>
    /// <param name="name">The name of the scene</param>
	public void LoadLevel(string name)
    {
		SceneManager.LoadScene(name);
	}

    /// <summary>
    /// Quits the game
    /// </summary>
	public void EndGame()
    {
		//Debug.Log ("Quit requested");
		Application.Quit ();
	}

    /// <summary>
    /// Loads the next scene in the build settings order
    /// </summary>
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
