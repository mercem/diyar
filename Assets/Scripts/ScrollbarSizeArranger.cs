using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSizeArranger : MonoBehaviour
{

    public Scrollbar scrollbar;

    void Start()
    {
        scrollbar.value = 1;
    }

    public void ResetList()
    {
        scrollbar.value = 1;
    }
}