using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cTileControll : MonoBehaviour {
    //Chase: 0, Scatter: 1, Frightened: 2
    public int mode;
    public GameObject pm;
    public GameObject clyde;
    // Use this for initialization
    void Start() {

    }

    private float distToPac()
    {
        return (pm.transform.position - clyde.transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            if (distToPac() > 16)
            {
                transform.position = pm.transform.position;
            }
            else if (distToPac() < 16)
            {
                transform.position = new Vector3(-27, -31);
            }
        }
        else
        {
            transform.position = new Vector3(-27, -31);
        }
    }
}
