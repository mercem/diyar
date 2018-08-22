using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryLoader : MonoBehaviour {

    public GameObject addElementHome;

    // Use this for initialization
    void Start () {
		
	}

    public void LoadByName(string name)
    {
        addElementHome.GetComponent<AddElementHome>().AddElementsByCategory(name);
    }

    public void LoadAll()
    {
        addElementHome.GetComponent<AddElementHome>().AddAllElements();
    }

}
