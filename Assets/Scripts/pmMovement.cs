using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pmMovement : MonoBehaviour
{

    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer m_render;
    private AudioSource m_chomp;
    private Animator m_anim;
    public float speed = 400f;
    public float hMove = 0f, vMove = 0f;
    public bool canMoveUp, canMoveDown, canMoveLeft, canMoveRight;

    public int score;
    private int pelletsEaten = 0;
    private bool playing;
    private bool dead;
    public int direction;

    public Text scoreText;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_render = GetComponent<SpriteRenderer>();
        m_chomp = GetComponent<AudioSource>();
        m_anim = GetComponent<Animator>();
        canMoveRight = true;
        canMoveLeft = true;
        canMoveDown = true;
        canMoveUp = true;
        score = 0;
        playing = false;
        m_anim.SetBool("isDead", false);

        scoreText.text = "00";
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float axisH = Input.GetAxisRaw("Horizontal");
        float axisV = Input.GetAxisRaw("Vertical");



        if ((canMoveRight && axisH > 0) || (canMoveLeft && axisH < 0))
        {
            hMove = axisH * speed;
        }
        else if (Mathf.Abs(m_Rigidbody2D.velocity.x) < 5)
        {
            hMove = 0;
        }
        if ((canMoveDown && axisV < 0) || (canMoveUp && axisV > 0))
        {
            vMove = axisV * speed;
        }
        else if (Mathf.Abs(m_Rigidbody2D.velocity.y) < 5)
        {
            vMove = 0;
        }

        if (playing && !m_chomp.isPlaying)
        {
            m_chomp.Play();
            playing = false;
        }

        
        if (Mathf.Abs(m_Rigidbody2D.velocity.x) > 0 || Mathf.Abs(m_Rigidbody2D.velocity.y) > 0)
        {
            if (m_Rigidbody2D.velocity.x > 2)
            {
                m_anim.SetInteger("dir", 0);
                direction = 0;
            }
            else if (m_Rigidbody2D.velocity.x < -2)
            {
                m_anim.SetInteger("dir", 2);
                direction = 2;
            }
            if (m_Rigidbody2D.velocity.y > 2)
            {
                m_anim.SetInteger("dir", 1);
                direction = 1;
            }
            else if (m_Rigidbody2D.velocity.y < -2)
            {
                m_anim.SetInteger("dir", 3);
                direction = 3;
            }
        }
        else
        {
            m_anim.SetInteger("dir", -1);

        }
        
    }

    // Update at a fixed rate;
    void FixedUpdate()
    {

        m_chomp.pitch += 0.000000001f;
        m_Rigidbody2D.velocity = new Vector2(hMove * Time.fixedDeltaTime, vMove * Time.fixedDeltaTime);
        if (transform.position.x >= 29.5f)
        {
            transform.position = new Vector3(-29.4f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -29.5f)
        {
            transform.position = new Vector3(29.4f, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pellet"))
        {
            other.gameObject.SetActive(false);
            score += 10;
            pelletsEaten++;
            playing = true;
        }
        if (other.gameObject.CompareTag("PowerPellet"))
        {
            other.gameObject.SetActive(false);
            score += 50;
        }
        updateScore();
    }

    void updateScore()
    {
        scoreText.text = score + "";
    }
}
