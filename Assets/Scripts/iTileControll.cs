using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTileControll : MonoBehaviour {
    //Chase: 0, Scatter: 1, Frightened: 2
    public int mode;
    public GameObject pm;
    public GameObject blinky;
    public GameObject setup;

    // Use this for initialization
    void Start () {
        mode = 0;
    }

    private float Dx(float offset)
    {
        return (pm.transform.position.x + offset)- blinky.transform.position.x;
    }

    private float Dy(float offset)
    {
        return (pm.transform.position.y + offset) - blinky.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (setup.GetComponent<Intro>().ghostMode == 0)
        {
            //transform.position = new Vector3(pm.transform.position.x, pm.transform.position.y, transform.position.z);
            switch (pm.GetComponent<pmMovement>().direction)
            {
                case 0:
                    transform.position = (new Vector3(pm.transform.position.x + 4 + Dx(4), pm.transform.position.y + Dy(0), 0));
                    break;
                case 1:
                    transform.position = (new Vector3(pm.transform.position.x + Dx(0), pm.transform.position.y + Dy(4), 0));
                    break;
                case 2:
                    transform.position = (new Vector3(pm.transform.position.x + 4 + Dx(-4), pm.transform.position.y + Dy(0), 0));
                    break;
                case 3:
                    transform.position = (new Vector3(pm.transform.position.x + Dx(0), pm.transform.position.y + Dy(-4), 0));
                    break;
                default:
                    break;
            }

        }
        else if (setup.GetComponent<Intro>().ghostMode == 1 && setup.GetComponent<Intro>().ghostMode == 2)
        {
            transform.position = new Vector3(27, -31);
        }
        if (GameObject.Find("Inky").GetComponent<Animator>().GetInteger("mode") == 3)
        {
            transform.position = new Vector3(0, 9, transform.position.z);
        }
    }
}
