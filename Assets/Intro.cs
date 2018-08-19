using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{

    AudioSource intro;
    bool played = false;
    // Use this for initialization
    void Start()
    {
        intro = GetComponent<AudioSource>();
        //playIntro();
    }

    void Awake()
    {
       
        //Time.timeScale = 0f;
        //playIntro();
        //Time.timeScale = 0f;

    }

    void playIntro()
    {
        Time.timeScale = 0f;
        intro.Play();
        while (intro.isPlaying) { }
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!played)
        {
            played = true;
            playIntro();
        }
    }
}
