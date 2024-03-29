﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class AssetDownloader : MonoBehaviour 
{
	GameObject model;
    public Animator loadingPanel;
	public Transform spawnPos;
    public static string forwardedModelUrl;
    private string assetName = "modele";
    
	IEnumerator LoadBundle(string url) 
	{
	    while (!Caching.ready)
	    {
			yield return null;
	    }

	    //Begin download
	    WWW www = WWW.LoadFromCacheOrDownload (url, 0);
	    yield return www;

	    //Load the downloaded bundle
	    AssetBundle bundle = www.assetBundle;

	    //Load an asset from the loaded bundle
		AssetBundleRequest bundleRequest = bundle.LoadAssetAsync (assetName, typeof(GameObject));
	    yield return bundleRequest;

		//get object
		GameObject obj = bundleRequest.asset as GameObject;

        model = Instantiate(obj, spawnPos.position, Quaternion.identity) as GameObject;
        model.AddComponent<Rotate>();
        loadingPanel.Play("Panel Close");

        bundle.Unload(false);
        www.Dispose();
	}

	public void Load(string forwardedModelUrl, string downloadedUrl)
	{

        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://projem02.firebaseio.com/");
        //DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        //reference.OrderByChild("name").StartAt("c").GetValueAsync().ContinueWith(task => {
        //    if (task.IsFaulted)
        //    {
        //        Debug.Log("not ok");
        // }
        //    else if (task.IsCompleted)
        //    {
        //       Debug.Log("ok");
        //       DataSnapshot ss = task.Result;
        // }
        //});

        if (model)
        {
            Destroy(model);
        }


        AssetDownloader.forwardedModelUrl = forwardedModelUrl;

       
        //loadingPanel.Play("Panel Open");
        StartCoroutine(LoadBundle(downloadedUrl));		
	}




}
