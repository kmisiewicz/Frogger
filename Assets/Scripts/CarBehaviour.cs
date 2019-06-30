using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    [Tooltip("Prędkość pojazdu.")]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
        else if(collision.tag == "Player")
        {
            GameController.instance.GameOver();
        }
    }
}
