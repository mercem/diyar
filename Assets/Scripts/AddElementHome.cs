using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System;

public class AddElementHome : MonoBehaviour
{

    public GameObject element;
    public GameObject downloader;
    public GameObject modelPanel;
    public string sprite_bundle_url;
    public string assetName;
    GameObject[] sprites;
    public string ChildDb;
    DatabaseReference reference;


	// Use this for initialization
	void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DatabaseConnection.databaseURL);
        reference = FirebaseDatabase.DefaultInstance.RootReference.Child(ChildDb);
        reference.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("not ok");
            }
            else if (task.IsCompleted)
            {
                Debug.Log("ok");
                DataSnapshot dataSnapshot = task.Result;

                StartCoroutine(LoadSpriteAddElement(dataSnapshot,sprite_bundle_url));
            }
        });
    }

    public void getAllElements(){
        Start();
    }

    public void getElementsByCategory(String category){
        reference.OrderByChild("category").StartAt(category).EndAt(category).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("not ok");
            }
            else if (task.IsCompleted)
            {
                Debug.Log("ok");
                DataSnapshot dataSnapshot = task.Result;

                StartCoroutine(LoadSpriteAddElement(dataSnapshot, sprite_bundle_url));
            }
        });
    } 



    IEnumerator LoadSpriteAddElement(DataSnapshot dataSnapshot,string url)
    {
        while (!Caching.ready)
        {
            yield return null;
        }

        //Begin download
        WWW www = WWW.LoadFromCacheOrDownload(url, 0);
        yield return www;

        //Load the downloaded bundle
        AssetBundle bundle = www.assetBundle;

        //Add elements
        foreach (var dS in dataSnapshot.Children)
        {
            GameObject item = Instantiate(element);
            //Load an asset from the loaded bundle
            AssetBundleRequest bundleRequest = bundle.LoadAssetAsync(dS.Key,typeof(Sprite));
            yield return bundleRequest;
            item.transform.Find("Image").GetComponent<Image>().sprite = bundleRequest.asset as Sprite;
            item.transform.Find("Title").GetComponent<Text>().text = dS.Child("name").Value as String;
            item.GetComponent<Button>().onClick.AddListener(() => downloader.GetComponent<AssetDownloader>().Load(dS.Child("hq_prefab_link").Value as String));
            item.GetComponent<Button>().onClick.AddListener(() => modelPanel.GetComponent<Button>().interactable = true);
            item.transform.SetParent(GetComponent<GridLayoutGroup>().transform);
            item.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }



        //get object

        //sprites = Array.ConvertAll(bundleRequest, item => item as GameObject); 
         


        //model.AddComponent<Rotate>();
        //loadingPanel.Play("Panel Close");

        bundle.Unload(false);
        www.Dispose();
    }
}
