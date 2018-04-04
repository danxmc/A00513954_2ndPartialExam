using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    /// <summary>
    /// When the item touches the player, the item will destroy itself, after .25 seconds
    /// </summary>
    /// <param name="collision">The collision of an object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject, .25f);
        }
    }
}
