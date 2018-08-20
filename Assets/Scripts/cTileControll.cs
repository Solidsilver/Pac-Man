using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cTileControll : MonoBehaviour {
    //Chase: 0, Scatter: 1, Frightened: 2
    public int mode;
    public GameObject pm;
    public GameObject clyde;
    public GameObject setup;
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
        if (setup.GetComponent<Intro>().ghostMode == 0)
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
        else if (setup.GetComponent<Intro>().ghostMode == 1 && setup.GetComponent<Intro>().ghostMode == 2)
        {
            transform.position = new Vector3(-27, -31);
        }
        if (GameObject.Find("Clyde").GetComponent<Animator>().GetInteger("mode") == 3)
        {
            transform.position = new Vector3(0, 9, transform.position.z);
        }
    }
}
