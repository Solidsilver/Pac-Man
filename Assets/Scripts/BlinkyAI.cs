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
    public float dRight, dUp, dLeft, dDown;
    public bool up, down, right, left, canChangeDir;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        dirs = GetComponents<BoxCollider2D>();
        hMove = 0;
        vMove = 0;
        dir = 2;
        speed = 400f;
        up = false;
        down = true;
        right = false;
        left = true;
        canChangeDir = false;
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
	
	// Update is called once per frame
	void Update () {

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
