using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyAI : MonoBehaviour {

    private Rigidbody2D m_Rigidbody2D;
    public float hMove, vMove, speed;
    public int dir;
    private BoxCollider2D[] dirs;
    public GameObject map;
    public GameObject TargetTile;
    public GameObject pm;
    public GameObject home;
    public Animator am;
    public float dRight, dUp, dLeft, dDown;
    public bool up, down, right, left, canChangeDir, paused;
    public int startDir;

    private const float spDefault = 400, spSlow = 300, spFast = 700;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        dirs = GetComponents<BoxCollider2D>();
        am = GetComponent<Animator>();
        hMove = 0;
        vMove = 0;
        dir = 2;
        speed = spDefault;
        up = false;
        down = true;
        right = false;
        left = true;
        canChangeDir = false;
        paused = false;
        am.SetInteger("dir", startDir);
    }

    // Use this for initialization
    void Start () {
        //transform.position = new Vector3(0, 11);

    }

    bool dirFree(int dir)
    {
        return !dirs[dir+1].IsTouching(map.GetComponent<Collider2D>());
    }

    int evalDir()
    {
        this.right = dirFree(0);
        this.up = dirFree(1);
        this.left = dirFree(2);
        this.down = dirFree(3);
        switch (this.dir)
        {
            case 0:
                left = false;
                break;
            case 1:
                down = false;
                break;
            case 2:
                right = false;
                break;
            case 3:
                up = false;
                break;
            default:
                break;

        }

        int ret = 0;

        Vector3 rightV = new Vector3(transform.position.x + 2, transform.position.y);
        dRight = Mathf.Abs((rightV - TargetTile.transform.position).magnitude);
        Vector3 upV = new Vector3(transform.position.x, transform.position.y + 2);
        dUp = Mathf.Abs((upV - TargetTile.transform.position).magnitude);
        Vector3 leftV = new Vector3(transform.position.x - 2, transform.position.y);
        dLeft = Mathf.Abs((leftV - TargetTile.transform.position).magnitude);
        Vector3 downV = new Vector3(transform.position.x, transform.position.y - 2);
        dDown = Mathf.Abs((downV - TargetTile.transform.position).magnitude);

        float shortest = 100;
        if (this.right && dRight < shortest)
        {
            shortest = dRight;
            ret = 0;
        }
        if (this.up && dUp < shortest)
        {
            shortest = dUp;
            ret = 1;
        }
        if (this.left && dLeft < shortest)
        {
            shortest = dLeft;
            ret = 2;
        }
        if (this.down && dDown < shortest)
        {
            shortest = dDown;
            ret = 3;
        }
        Debug.Log("Shortest distance is: " + shortest + ", Direction changing to: " + ret);
        this.right = dirFree(0);
        this.up = dirFree(1);
        this.left = dirFree(2);
        this.down = dirFree(3);
        am.SetInteger("dir", ret);
        return ret;
    }

    bool isJunction()
    {
        if (!right && dirFree(0) && dir != 2)
        {
            return true;
        }
        if (!up && dirFree(1) && dir != 3)
        {
            return true;
        }
        if (!left && dirFree(2) && dir != 0)
        {
            return true;
        }
        if (!down && dirFree(3) && dir != 1)
        {
            return true;
        }
        return false;
    }

    public void setMode(int mode)
    {
        am.SetInteger("mode", mode);
    }
	
	// Update is called once per frame
	void Update () {
        if (paused)
            return;

            //am.SetInteger("mode", GameObject.Find("Timer").GetComponent<Intro>().ghostMode);
        if (am.GetInteger("mode") == 3 && (dirs[0].IsTouching(home.GetComponent<BoxCollider2D>()))) {
            am.SetInteger("mode", 0);
            speed = spDefault;
        }
        if (dirs[0].IsTouching(pm.GetComponent<CircleCollider2D>()))
        {
            if (am.GetInteger("mode") < 2) 
                pm.GetComponent<pmMovement>().killed();
            if (am.GetInteger("mode") == 2)
            {
                am.SetInteger("mode", 3);
                pm.GetComponent<pmMovement>().score += 200;
                speed = spFast;
            }
        }
        if (isJunction())
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            dir = evalDir();
        }
        if (right && !dirFree(0))
            right = false;
        if (up && !dirFree(1))
            up = false;
        if (left && !dirFree(2))
            left = false;
        if (down && !dirFree(3))
            down = false;

        switch (dir)
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

    // Called at at a fixed time interval
    void FixedUpdate()
    {
        if (paused)
            return;
        m_Rigidbody2D.velocity = new Vector2(hMove * speed * Time.fixedDeltaTime, vMove * speed * Time.fixedDeltaTime);

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

    }
}
