﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabLogic : MonoBehaviour {

    public GameObject pane;
    public Text TitleText;
    public Image ConnectedImage, WarningImage;

    public string Title
    {
        get
        {
            return TitleText.text;
        }
        set
        {
            TitleText.text = value;
        }
    }

    public void SetConnected(bool connected)
    {
        ConnectedImage.enabled = connected;
    }

    public void SetWarning(bool warning)
    {
        WarningImage.enabled = warning;
    }

    void Start()
    {
        //set up via Unity Inspector
        //GetComponent<Button>().onClick.AddListener(clicked);
    }

    public void clicked()
    {
        Debug.Log("[TabLogic:clicked] " + Title + " clicked");
        //notify the tab wrangler
        HololensTabWrangler.Instance.setActiveTab(this);
        //bring the associated content pane to the front
        pane.transform.SetAsLastSibling();
    }
}
