using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    [Tooltip("Prefab kłody.")]
    public GameObject woodLog;
    [Tooltip("Międzyczasy spawnu kłód (4).")]
    public float[] timesBetweenSpawns = new float[4];
    [Tooltip("Prędkości kłód w liniach (4).")]
    public float[] speeds = new float[4];
    [Tooltip("Prędkości kłód w liniach (4).")]
    public float[] lengths = new float[4];

    private float[] lanesY;
    private bool[] canSpawnInLane = new bool[4] { true, true, true, true };

    // Start is called before the first frame update
    void Start()
    {
        lanesY = new float[4];
        float d = transform.localScale.y / 8.0f;
        lanesY[0] = transform.position.y - (3 * d);
        lanesY[1] = transform.position.y - d;
        lanesY[2] = transform.position.y + d;
        lanesY[3] = transform.position.y + (3 * d);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (canSpawnInLane[i])
            {
                
                GameObject c = Instantiate(woodLog, new Vector3(0, 0, 0), transform.rotation);
                if (speeds[i] > 0)
                {
                    c.transform.position = new Vector3(-15, lanesY[i], 0);
                }
                else
                {
                    c.transform.position = new Vector3(15, lanesY[i], 0);
                }
                c.transform.localScale = new Vector3(lengths[i], 0.8f, 1);
                c.GetComponent<WoodLogBehaviour>().speed = speeds[i];
                canSpawnInLane[i] = false;
                StartCoroutine(WaitForSpawn(i));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameController.instance.SetPlayerOnWater(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameController.instance.SetPlayerOnWater(false);
        }
    }

    private IEnumerator WaitForSpawn(int lane)
    {
        yield return new WaitForSeconds(timesBetweenSpawns[lane]);
        canSpawnInLane[lane] = true;
    }

    private void OnValidate()
    {
        if (timesBetweenSpawns.Length != 4) Array.Resize(ref timesBetweenSpawns, 4);
        if (speeds.Length != 4) Array.Resize(ref speeds, 4);
        if (lengths.Length != 4) Array.Resize(ref lengths, 4);
    }
}
