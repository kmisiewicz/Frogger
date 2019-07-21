using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Tooltip("Prędkość gracza.")]
    public float speed = 1.0f;
    [Tooltip("Grafika pojawiająca się w miejscu dojścia na łąkę.")]
    public GameObject happyPepe;

    private Rigidbody2D rb;
    private ParticleSystem deathParticle;
    private SpriteRenderer sprite;
    private Vector2 respawnPosition;
    private bool movementLocked = false;
    public bool MovementLocked { get { return movementLocked; } }
    private Collider2D collider;
    private int lives = 3;
    private bool isOnWater;
    public bool IsOnWater { get { return isOnWater; } set { isOnWater = value; } }
    private bool verticalMovement = true;
    private List<GameObject> spawnedHappyFrogs;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPosition = transform.position;
        collider = GetComponent<Collider2D>();
        spawnedHappyFrogs = new List<GameObject>();
        deathParticle = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementLocked)
        {
            Vector2 newVelocity = rb.velocity;
            float moveX, moveY;
            moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            newVelocity.x = moveX;
            rb.velocity = newVelocity;
            if(verticalMovement)
            {
                if(Input.GetKeyDown(KeyCode.W))
                {
                    transform.Translate(0, 1, 0);
                    verticalMovement = false;
                    StartCoroutine(WaitForVerticalMovement());
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.Translate(0, -1, 0);
                    verticalMovement = false;
                    StartCoroutine(WaitForVerticalMovement());
                }
                Vector3 pos = transform.position;
                pos.y = Mathf.RoundToInt(pos.y);
                transform.position = pos;
            }

            if (isOnWater && transform.parent == null && !movementLocked)
            {
                GameController.instance.GameOver();
            }
        }
    }

    private void FixedUpdate()
    {
        transform.parent = null;
    }

    public void DieAndRespawn()
    {
        StartCoroutine(DeathAndRespawn());
    }

    private IEnumerator DeathAndRespawn()
    {
        deathParticle.Play();
        sprite.enabled = false;
        lives--;
        GameController.instance.UpdateLives(lives);
        rb.velocity = Vector2.zero;
        movementLocked = true;
        isOnWater = false;
        collider.enabled = false;
        yield return new WaitForSeconds(2.0f);
        GameController.instance.ResetTimer();
        collider.enabled = true;
        transform.position = respawnPosition;
        if (lives == 0)
        {
            lives = 3;
            GameController.instance.ResetAll();
            foreach (GameObject o in spawnedHappyFrogs)
            {
                Destroy(o);
            }
            spawnedHappyFrogs.Clear();
        }
        sprite.enabled = true;
        GameController.instance.ToggleDeathScreen();
        yield return new WaitForSeconds(0.5f);
        movementLocked = false;
        GameController.instance.StartTimer();
    }

    public void GoToSafeZone(Vector2 newPosition, int happyFrogs)
    {
        StartCoroutine(Win(newPosition, happyFrogs));
    }

    private IEnumerator Win(Vector3 newPosition, int happyFrogs)
    {
        rb.velocity = Vector2.zero;
        isOnWater = false;
        movementLocked = true;
        collider.enabled = false;

        float counter = 0.0f;
        Vector3 startPosition = transform.position;
        while(counter < 0.5f)
        {
            transform.position = Vector3.Lerp(startPosition, newPosition, counter / 0.5f);
            counter += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.position = newPosition;

        spawnedHappyFrogs.Add(Instantiate(happyPepe, newPosition, Quaternion.identity));
        if (happyFrogs == 3)
        {
            yield return new WaitForSeconds(1.5f);
            lives = 3;
            GameController.instance.ResetAll();
            foreach(GameObject o in spawnedHappyFrogs)
            {
                Destroy(o);
            }
            spawnedHappyFrogs.Clear();
        }
        transform.position = respawnPosition;
        collider.enabled = true;
        GameController.instance.ResetTimer();
        yield return new WaitForSeconds(0.5f);
        movementLocked = false;
        GameController.instance.StartTimer();
    }

    private IEnumerator WaitForVerticalMovement()
    {
        yield return new WaitForSeconds(0.1f);
        verticalMovement = true;
    }
}
