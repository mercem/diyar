using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;


public class AddElementHome : MonoBehaviour
{
    public GameObject element;
    public GameObject downloader;
    public GameObject modelPanel;
    public GameObject list;
    public string spriteBundleURL;
    public string assetName;
    GameObject[] sprites;
    DatabaseReference reference;
    

    public void AddAllElements(){
        ClearList();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DatabaseConnection.getDatabaseURL());
        reference = FirebaseDatabase.DefaultInstance.RootReference.Child("elements");
        reference.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("not ok");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot dataSnapshot = task.Result;
                StartCoroutine(LoadSpriteAddElements(dataSnapshot));
            }
        });
    }

    public void AddElementsByCategory(string category){
        ClearList();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DatabaseConnection.getDatabaseURL());
        reference = FirebaseDatabase.DefaultInstance.RootReference.Child("elements");
        reference.OrderByChild("category").StartAt(category).EndAt(category).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("not ok");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot dataSnapshot = task.Result;
                StartCoroutine(LoadSpriteAddElements(dataSnapshot));
            }
        });
    }
    
    public void AddElement(DataSnapshot dataSnapshot)
    {
        ClearList();
        StartCoroutine(LoadSpriteAddElements(dataSnapshot));
    }



    IEnumerator LoadSpriteAddElements(DataSnapshot dataSnapshot)
    {
        while (!Caching.ready)
        {
            yield return null;
        }

        //Begin download
        WWW www = WWW.LoadFromCacheOrDownload(spriteBundleURL, 0);
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
            item.transform.Find("Title").GetComponent<Text>().text = dS.Child("name").Value as string;
            item.GetComponent<Button>().onClick.AddListener(() => downloader.GetComponent<AssetDownloader>().Load(dS.Child("lqPrefabLink").Value as string, dS.Child("hqPrefabLink").Value as string));
            item.GetComponent<Button>().onClick.AddListener(() => modelPanel.GetComponent<Button>().interactable = true);
            item.transform.SetParent(list.GetComponent<GridLayoutGroup>().transform,false);
            item.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }



        //get object

        //sprites = Array.ConvertAll(bundleRequest, item => item as GameObject); 
         


        //model.AddComponent<Rotate>();
        //loadingPanel.Play("Panel Close");

        bundle.Unload(false);
        www.Dispose();
    }

    void ClearList()
    {
        int childCount = list.transform.childCount;
        if (childCount != 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                Destroy(list.transform.GetChild(i).gameObject);
            }
        };
    }
     

}
