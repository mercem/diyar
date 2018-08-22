using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class DatabaseConnection : MonoBehaviour {

    private DatabaseReference reference;
    private DataSnapshot dataSnapshot;
    private bool wait = true;
    private static string databaseURL = "https://projem02.firebaseio.com";

	// Use this for initialization
	void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(databaseURL);
        this.reference = FirebaseDatabase.DefaultInstance.RootReference;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static string getDatabaseURL() {
        return databaseURL;
    }

    //  ***** bu ikisi beraber olan hali ****
    //public DataSnapshot getAllElements(){
    //    // startcoroutine asycn o yuzden return yaptiginda object bos oluor bunu sync yapmak lzm
    //    StartCoroutine(getAllElementsIE());

    //    //while(this.wait){
    //    //    StartCoroutine(setDelay());
    //    //}
    //    return this.dataSnapshot;
    //}

     
    //IEnumerator getAllElementsIE()
    //{
    //    var task = this.reference.GetValueAsync();

    //    while (task.IsCompleted == false)
    //    {
    //        yield return null;
    //    }

    //    if (task.IsFaulted)
    //    {
    //        throw task.Exception;
    //    }

    //    if (task.IsCompleted)
    //    {
    //        this.dataSnapshot = task.Result;

    //    }
    //}


    public DataSnapshot getAllElements(){
        this.reference.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("not ok");
         }
            else if (task.IsCompleted)
            {
               Debug.Log("ok");
                this.dataSnapshot = task.Result;
         }
        });

        return this.dataSnapshot;
    }

    void getElementsByCategory (){
        
    }

}
