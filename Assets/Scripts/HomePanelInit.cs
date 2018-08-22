using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanelInit : MonoBehaviour {

    public GameObject addElementHome;

	void Start () {
        addElementHome.GetComponent<AddElementHome>().AddAllElements();
	}
	

}
