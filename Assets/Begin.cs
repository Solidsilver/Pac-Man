using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Begin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(896, 1152, false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
            SceneManager.LoadScene("LvlOne");
	}
}
