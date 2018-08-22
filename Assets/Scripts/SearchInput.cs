using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine.UI;


public class SearchInput : MonoBehaviour {

    public GameObject addElementHome;
    public GameObject element;
    public GameObject searchList;
    public GameObject categoryList;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onValueChanged(string query)
    {
        if (query.Length == 0)
        {
            searchList.SetActive(false);
            categoryList.SetActive(true);
        }
        else
        {
            categoryList.SetActive(false);
            searchList.SetActive(true);

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DatabaseConnection.getDatabaseURL());
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference.Child("elements");
            reference.OrderByChild("searchName").StartAt(query).EndAt(query+"\uf8ff").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("not ok");
                }
                else if (task.IsCompleted)
                {
                    int childCount = searchList.transform.childCount;
                    if (childCount != 0)
                    {
                        for (int i = 0; i < childCount; i++)
                        {
                            Destroy(searchList.transform.GetChild(i).gameObject);
                        }
                    };

                    DataSnapshot dataSnapshot = task.Result;
                    foreach (var dS in dataSnapshot.Children)
                    {
                        GameObject item = Instantiate(element);
                        item.transform.Find("Text").GetComponent<Text>().text = dS.Child("name").Value as string;
                        item.GetComponent<Button>().onClick.AddListener(() => addElementHome.GetComponent<AddElementHome>().AddElement(dS));
                        item.transform.SetParent(searchList.GetComponent<VerticalLayoutGroup>().transform, false);
                    }

                }
            });
        }
    }

}
