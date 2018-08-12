using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStarter : MonoBehaviour {

    public AssetDownloader assetDownloader;
	// Use this for initialization
	void Start () {
        assetDownloader.Load(AssetDownloader.vuforiaModelUrl);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
