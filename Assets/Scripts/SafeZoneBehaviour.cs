using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameController.instance.GoToSafeZone(transform.position);
            GetComponent<Collider2D>().enabled = false;
        }
        
    }
}
