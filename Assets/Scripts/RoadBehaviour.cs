using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBehaviour : MonoBehaviour
{
    [Tooltip("Prefab samochodu.")]
    public GameObject carPrefab;
    [Tooltip("Prefab ciężarówki.")]
    public GameObject truckPrefab;
    [Tooltip("Międzyczasy spawnu pojazdów (4).")]
    public float[] timesBetweenSpawns = new float[4];
    [Tooltip("Prędkości pojazdów w liniach (4).")]
    public float[] speeds = new float[4];

    private float[] lanesY;
    private bool[] laneDirs = new bool[4] { false, true, false, true };
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

        //laneDirs = new bool[4];
        //for(int i = 0; i < 4; i++)
        //{
        //    if (UnityEngine.Random.value > 0.5) laneDirs[i] = true;
        //    else laneDirs[i] = false;
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            if (canSpawnInLane[i])
            {
                GameObject prefabToSpawn = truckPrefab;
                if (i == 0 || i == 3) prefabToSpawn = carPrefab;
                GameObject c = Instantiate(prefabToSpawn, new Vector3(0, 0, 0), transform.rotation);
                if (speeds[i] > 0)
                {
                    c.transform.position = new Vector3(-15, lanesY[i], 0);
                }
                else
                {
                    c.GetComponent<SpriteRenderer>().flipX = true;
                    c.transform.position = new Vector3(15, lanesY[i], 0);
                }
                c.GetComponent<CarBehaviour>().speed = speeds[i];
                canSpawnInLane[i] = false;
                StartCoroutine(WaitForSpawn(i));
            }
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
    }
}
