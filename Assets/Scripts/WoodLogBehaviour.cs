using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogBehaviour : MonoBehaviour
{
    [Tooltip("Prędkość kłody.")]
    public float speed;
    [Tooltip("Czas, co jaki istnieje możliwość zanurzenia w sekundach.")]
    public float drownCheckInterval = 1.0f;
    [Tooltip("Prawdopodobieństwo zanurzenia.")][Range(0.0f, 1.0f)]
    public float drownFactor = 0.4f;
    [Tooltip("Czas, jaki kłoda jest pod wodą.")]
    public float drownTime = 1.0f;

    private bool underwater = false;
    private Collider2D ground;
    private Animator anim;
    
    private void Start()
    {
        ground = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        InvokeRepeating("Drown", drownCheckInterval, drownCheckInterval);
    }
    
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void Drown()
    {
        if(!underwater && UnityEngine.Random.value < drownFactor)
        {
            anim.SetBool("Drown", true);
        }
    }

    private void Undrown()
    {
        if(underwater)
        {
            anim.SetBool("Drown", false);
        }
    }

    private void ToggleCollider()
    {
        ground.enabled = !ground.enabled;
        if(underwater)
        {
            Debug.Log(1);
            Invoke("Undrown", drownTime);
        }
    }

    private void TriggerDrownAbility()
    {
        underwater = !underwater;
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
