using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaSceneStarter : MonoBehaviour {

    public AssetDownloader assetDownloader;
	// Use this for initialization
	void Start () {
        assetDownloader.Load(null ,AssetDownloader.forwardedModelUrl);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
