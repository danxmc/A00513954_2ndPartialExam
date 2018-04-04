using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {
    public Transform player;
    public Vector3 offset;
	
	// Transform the position of the camera in the x axis to follow the player
	void Update () {
        transform.position = new Vector3(player.position.x + offset.x,offset.y, offset.z);
	}
}
