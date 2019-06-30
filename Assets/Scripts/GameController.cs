using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHelpers;

public class GameController : MonoSingleton<GameController>
{
    [Tooltip("Obiekt gracza.")]
    public GameObject playerObject;
    [Tooltip("Obiekt Canvasa zawierającego interfejs.")]
    public Canvas canvas;
    [Tooltip("Czas kiedy liczone są punkty.")]
    public float timer = 10.0f;

    private PlayerBehaviour player;
    private UIController ui;
    private int frogsSaved = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<PlayerBehaviour>();
        ui = canvas.GetComponent<UIController>();
        ui.timer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        ToggleDeathScreen();
        player.DieAndRespawn();
        ui.StopTimer();
    }

    public void ToggleDeathScreen()
    {
        ui.ToggleDeathScreen();
    }

    public void GoToSafeZone(Vector3 newPosition)
    {
        frogsSaved++;
        player.GoToSafeZone(newPosition, frogsSaved);
        ui.SetHappyFrogs(frogsSaved);
        ui.StopTimer();
        ui.AddPointsFromTimer();
        ui.AddPoints(50);
        if(frogsSaved == 3)
        {
            ui.ToggleWinScreen();
            StartCoroutine(WinRoutine());
        }
    }

    public void SetPlayerOnWater(bool isOnWater)
    {
        if(!player.MovementLocked) player.IsOnWater = isOnWater;
    }

    public void SetLogAsPlayerParent(Transform logTransform)
    {
        player.transform.parent = logTransform;
    }

    public void ResetTimer()
    {
        ui.ResetTimer();
    }

    public void StartTimer()
    {
        ui.StartTimer();
    }

    private IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        ui.ToggleWinScreen();
    }

    public void UpdateLives(int lives)
    {
        ui.UpdateLives(lives);
    }

    public void ResetAll()
    {
        ui.UpdateLives(3);
        ui.ResetAll();
        frogsSaved = 0;

        GameObject[] safeZones = GameObject.FindGameObjectsWithTag("SafeZone");
        foreach(GameObject s in safeZones)
        {
            s.GetComponent<Collider2D>().enabled = true;
        }
    }
}
