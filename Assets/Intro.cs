using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private float timer;
    public int ghostMode, phase;
    AudioSource intro;
    bool gamePaused;

    public GameObject pm;
    public GameObject blinky;
    public GameObject inky;
    public GameObject pinky;
    public GameObject clyde;

    // Use this for initialization
    void Start()
    {
        intro = GetComponent<AudioSource>();
        timer = 0;
        phase = 1;
        ghostMode = 1;
        //intro.Play();
        introMusic();
    }

    void Awake()
    {
        gamePaused = false;
    }

    void pause()
    {
        gamePaused = true;
        pm.GetComponent<pmMovement>().paused = true;
        pm.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        blinky.GetComponent<BlinkyAI>().paused = true;
        blinky.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        inky.GetComponent<BlinkyAI>().paused = true;
        inky.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        pinky.GetComponent<BlinkyAI>().paused = true;
        pinky.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        clyde.GetComponent<BlinkyAI>().paused = true;
        clyde.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
    }
    void play()
    {
        gamePaused = false;
        pm.GetComponent<pmMovement>().paused = false;
        blinky.GetComponent<BlinkyAI>().paused = false;
        inky.GetComponent<BlinkyAI>().paused = false;
        pinky.GetComponent<BlinkyAI>().paused = false;
        clyde.GetComponent<BlinkyAI>().paused = false;

    }

    

    void FixedUpdate()
    {
        if (gamePaused)
        {
            return;
        }
       timer += Time.fixedDeltaTime;
    }

    void playReset()
    {
        
        pm.transform.position = new Vector3(0, -13);
        blinky.transform.position = new Vector3(0, 11);
        inky.transform.position = new Vector3(-4, 5);
        pinky.transform.position = new Vector3(0, 5);
        clyde.transform.position = new Vector3(4, 5);
        Invoke("introMusic", 2);
    }
    void introMusic()
    {
        pause();
        intro.Play();
        Invoke("play", intro.clip.length);
    }
    void pacReset()
    {
        pm.GetComponent<Animator>().SetBool("isDead", false);
    }

    public void PartialReset()
    {
        pause();
        Invoke("playReset", pm.GetComponent<pmMovement>().m_die.clip.length - 1);
        Invoke("pacReset", 1.3f);
    }

    public void endGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void setGhostMode(int mode)
    {
        timer = 0;
        blinky.GetComponent<BlinkyAI>().setMode(mode);
        inky.GetComponent<BlinkyAI>().setMode(mode);
        pinky.GetComponent<BlinkyAI>().setMode(mode);
        clyde.GetComponent<BlinkyAI>().setMode(mode);
        this.ghostMode = mode;
    }

    void Update()
    {
        if (gamePaused)
            return;
        if ((ghostMode == 2 || ghostMode == 3) && timer >= 7)
        {
            setGhostMode(0);
        }
        if (ghostMode == 1)
        {
            if ((phase == 1 || phase == 2) && timer >= 7.0f)
            {
                ghostMode = 0;
                timer = 0;
            }
            if ((phase == 3 || phase == 4) && timer >= 5.0f)
            {
                ghostMode = 0;
                timer = 0;
            }
        } else if (ghostMode == 0)
        {
            if ((phase == 1 || phase == 2) && timer >= 20.0f)
            {
                ghostMode = 1;
                timer = 0;
                phase += 1;
            }else if ((phase == 3 || phase == 4) && timer >= 20.0f)
            {
                ghostMode = 1;
                timer = 0;
                phase += 1;
            }
                
        }

        
    }
}
