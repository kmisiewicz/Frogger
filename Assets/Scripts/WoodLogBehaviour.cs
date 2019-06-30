using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogBehaviour : MonoBehaviour
{
    [Tooltip("Prędkość kłody.")]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
            GameController.instance.SetLogAsPlayerParent(transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameController.instance.SetLogAsPlayerParent(transform);
        }
    }
}
