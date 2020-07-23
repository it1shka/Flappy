using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Controller : MonoBehaviour
{
    public float jumpForce = 5f;
    public Rigidbody2D rigidbody;
    public GameObject deathEffect;

    private int score, record;
    private bool isRecord;

    public TextMeshProUGUI tmp;
    public GameObject recordText, watchVideoMenu;

    public enum PLAYMODE {EASY, HARD, ULTRA, INSANE}
    private PLAYMODE CurrentPlaymode;

    public enum MODE {NORMAL, REVERSE, FULLREVERSE, FULLREVERSE2};
    public static MODE CurrentMode { get; private set; }
    public int switchCount = 50;
    private int curSwitchCount;

    public PostProcessorSettings pps;
    public WallSpawner wallSpawner;
    public RectTransform pauseButton;
    public static bool isWatched, isPaused;
    public Collider2D myCollider;
    public float invisibleTime = 5f;
    public SpriteRenderer myRend;
    public LineRenderer myLine;
    public Material indicateMat, indicateLineMat, defMat, defLineMat;

    public AudioManager audioManager;

    private void Awake()
    {
        pps.BlurEnabled = false;
        pps.BlurRadius = 0f;
        curSwitchCount = switchCount;
        isRecord = false;
        score = 0;
        record = PlayerPrefs.GetInt("record");
        CurrentMode = MODE.NORMAL;
        isWatched = false;
        isPaused = false;
        Time.timeScale = 1f;
        Physics2D.gravity = new Vector2(0f, -9.81f);

        CurrentPlaymode = (PLAYMODE)PlayerPrefs.GetInt("currentmode");
        if (!audioManager)
            audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (!isPaused) 
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            foreach(var touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && 
                    !RectTransformUtility.RectangleContainsScreenPoint(pauseButton, touch.position))
                    Jump();
            }
#else
            if (Input.GetKeyDown(KeyCode.Space))
            Jump();
#endif
        }

        //optional
        ClampPosition();
        
    }

    private void ClampPosition()
    {
        transform.position = new Vector3(transform.position.x,
            Mathf.Clamp(transform.position.y, -5.0f, 5.0f)
            );
    }

    private void Jump()
    {
        audioManager.Play("jump");

        switch (CurrentMode) {
            case MODE.NORMAL:
            case MODE.FULLREVERSE2:
            if (rigidbody.velocity.y < 0f)
                rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                break;

            case MODE.REVERSE:
            case MODE.FULLREVERSE:
            if (rigidbody.velocity.y > 0f)
                rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(-Vector2.up * jumpForce, ForceMode2D.Impulse);

                break;
        }
    }

    public void SaveStats()
    {
        if (isRecord)
        {
            PlayerPrefs.SetInt("record", score);
            PlayerPrefs.SetInt("modeindex", (int)CurrentPlaymode);
            PlayerPrefs.Save();
        }
    }

    public void OnDestroy()
    {
        SaveStats();
    }

    public void Death()
    {
        audioManager.Play("explosion2");

        if (deathEffect)
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        FindObjectOfType<DeathMenuInvoker>().InvokeDeathMenu();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isWatched)
            watchVideoMenu.SetActive(true);
        else
            Death();
    }

    public void DisableCollider()
    {
        StartCoroutine(DisableColliderCoroutine());
    }

    private IEnumerator DisableColliderCoroutine()
    {
        myCollider.enabled = false;
        myRend.material = indicateMat;
        myLine.material = indicateLineMat;
        yield return new WaitForSeconds(invisibleTime);
        myCollider.enabled = true;
        myRend.material = defMat;
        myLine.material = defLineMat;
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        score++;
        tmp.text = $"{score}";
        if(score > record && !isRecord)
        {
            isRecord = true;
            recordText.SetActive(true);
        }
        curSwitchCount--;
        if (curSwitchCount <= 0)
        {
            audioManager.Play("powerup");

            wallSpawner.IncreaseSpeed();
            curSwitchCount = switchCount;

            //switch next mode
            switch (CurrentPlaymode) {
                case PLAYMODE.EASY:

                    break;
                case PLAYMODE.HARD:

                    rigidbody.velocity = Vector2.zero;
                    CurrentMode = CurrentMode == MODE.NORMAL ?
                    MODE.REVERSE : MODE.NORMAL;
                    Physics2D.gravity *= -1;

                    break;

                case PLAYMODE.ULTRA:

                    switch (CurrentMode)
                    {
                        case MODE.NORMAL:
                            rigidbody.velocity = Vector2.zero;
                            CurrentMode = MODE.REVERSE;
                            Physics2D.gravity *= -1;
                            break;
                        case MODE.REVERSE:
                            Camera.main.transform.
                                rotation = Quaternion.Euler(0f, 0f, 180f);
                            rigidbody.velocity = Vector2.zero;
                            CurrentMode = MODE.FULLREVERSE;
                            break;
                        case MODE.FULLREVERSE:
                            Camera.main.transform
                                .rotation = Quaternion.Euler(0f, 0f, 0f);
                            rigidbody.velocity = Vector2.zero;
                            CurrentMode = MODE.NORMAL;
                            Physics2D.gravity *= -1;
                            break;
                    }

                    break;

                case PLAYMODE.INSANE:

                    switch (CurrentMode)
                    {
                        case MODE.NORMAL:
                            rigidbody.velocity = Vector2.zero;
                            CurrentMode = MODE.REVERSE;
                            Physics2D.gravity *= -1;
                            break;
                        case MODE.REVERSE:
                            Camera.main.transform.
                                rotation = Quaternion.Euler(0f, 0f, 180f);
                            rigidbody.velocity = Vector2.zero;
                            CurrentMode = MODE.FULLREVERSE;
                            break;
                        case MODE.FULLREVERSE:
                            rigidbody.velocity = Vector2.zero;
                            CurrentMode = MODE.FULLREVERSE2;
                            Physics2D.gravity *= -1;
                            break;
                        case MODE.FULLREVERSE2:
                            Camera.main.transform.
                                rotation = Quaternion.Euler(0f, 0f, 0f);
                            rigidbody.velocity = Vector2.zero;
                            CurrentMode = MODE.NORMAL;
                            break;
                    }

                    break;
            }
            

        }
    }
}
