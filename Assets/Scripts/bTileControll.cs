using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bTileControll : MonoBehaviour {
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
            transform.position = pm.transform.position;
        } else
        {
            transform.position = new Vector3(23, 39);
        }
	}

    void FixedUpdate()
    {

    }
}
