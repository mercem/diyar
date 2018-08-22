using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAA : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(transform.childCount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TextLogger(string txt)
    {
        Debug.Log(txt);
    }

}
