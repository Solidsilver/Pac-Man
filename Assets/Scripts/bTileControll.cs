using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bTileControll : MonoBehaviour {
    //Chase: 0, Scatter: 1, Frightened: 2, Dead: 3
    public int mode;
    public GameObject pm;
    public GameObject setup;
    

	// Use this for initialization
	void Start () {
        mode = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (setup.GetComponent<Intro>().ghostMode == 0)
        {
            transform.position = pm.transform.position;
        } else if (setup.GetComponent<Intro>().ghostMode == 1 || setup.GetComponent<Intro>().ghostMode == 2)
        {
            transform.position = new Vector3(23, 39);
        }
        if (GameObject.Find("Blinky").GetComponent<Animator>().GetInteger("mode") == 3)
        {
            transform.position = new Vector3(0, 9, transform.position.z);
        }
    }

    void FixedUpdate()
    {

    }
}
