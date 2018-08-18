using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyAI : MonoBehaviour {

    private Rigidbody2D m_Rigidbody2D;
    private float hMove, vMove;
    private int dir;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        hMove = 0;
        vMove = 0;
        dir = 2;
    }

    // Use this for initialization
    void Start () {
		switch(dir)
        {
            case 0:
                hMove = 1;
                vMove = 0;
                break;
            case 1:
                hMove = 0;
                vMove = 1;
                break;
            case 2:
                hMove = -1;
                vMove = 0;
                break;
            case 3:
                hMove = 0;
                vMove = -1;
                break;
            default:
                hMove = 0;
                vMove = 0;
                break;
        }
     
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Called at at a fixed time interval
    void FixedUpdate()
    {
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
}
