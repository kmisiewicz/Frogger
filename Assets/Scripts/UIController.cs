using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image timerBar;
    public float timer;
    public Text scoreText;
    public Text happyFrogs;
    public Text totalScore;
    public Text livesText;

    private GameObject deathScreen;
    private GameObject winScreen;
    private bool deathScreenVisible = false;
    private bool winScreenVisible = false;
    private bool timerGoing = true;
    private float counter = 0.0f;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        winScreen = GameObject.FindGameObjectWithTag("WinScreen");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerGoing)
        {
            timerBar.fillAmount = Mathf.Lerp(1.0f, 0.0f, counter / timer);
            counter += Time.deltaTime;
            if (counter >= timer)
            {
                timerBar.fillAmount = 0.0f;
                timerGoing = false;
            }
        }
    }

    public void ToggleDeathScreen()
    {
        deathScreenVisible = !deathScreenVisible;
        deathScreen.GetComponent<Animator>().SetBool("IsVisible", deathScreenVisible);
    }

    public void ToggleWinScreen()
    {
        totalScore.text = string.Format("Total score: {0}", score);
        winScreenVisible = !winScreenVisible;
        winScreen.GetComponent<Animator>().SetBool("IsVisible", winScreenVisible);
    }

    public void AddPointsFromTimer()
    {
        score += (int)(timerBar.fillAmount * 100.0f);
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void ResetTimer()
    {
        timerGoing = false;
        timerBar.fillAmount = 1.0f;
        counter = 0.0f;
    }

    public void StopTimer()
    {
        timerGoing = false;
    }

    public void StartTimer()
    {
        timerGoing = true;
    }

    public void SetHappyFrogs(int happyFrogCount)
    {
        happyFrogs.text = string.Format("Happy Frogs: {0}/3", happyFrogCount);
    }

    public void ResetAll()
    {
        score = 0;
        happyFrogs.text = "Happy Frogs: 0/3";
        scoreText.text = "Score: 0";
    }

    public void UpdateLives(int lives)
    {
        livesText.text = string.Format("Lives: {0}/3", lives);
    }
}
