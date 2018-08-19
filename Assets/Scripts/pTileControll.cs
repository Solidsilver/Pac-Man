using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pTileControll : MonoBehaviour {
    //Chase: 0, Scatter: 1, Frightened: 2
    public int mode;
    public GameObject pm;

    // Use this for initialization
    void Start () {
        mode = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (mode == 0)
        {
            //transform.position = new Vector3(pm.transform.position.x, pm.transform.position.y, transform.position.z);
            switch(pm.GetComponent<pmMovement>().direction)
            {
                case 0:
                    transform.position = new Vector3(pm.transform.position.x + 8, pm.transform.position.y);
                    break;
                case 1:
                    transform.position = new Vector3(pm.transform.position.x, pm.transform.position.y + 8);
                    break;
                case 2:
                    transform.position = new Vector3(pm.transform.position.x - 8, pm.transform.position.y);
                    break;
                case 3:
                    transform.position = new Vector3(pm.transform.position.x, pm.transform.position.y - 8);
                    break;
                default:
                    break;
            }
        }
        else
        {
            transform.position = new Vector3(-23, 39);
        }
    }
}
