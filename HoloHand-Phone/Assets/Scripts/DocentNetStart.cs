﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DocentNetStart : INetStartLogic {
    public override void LoadConfig()
    {
        if (PlayerPrefs.HasKey("serverIP"))
        {
            string ip = PlayerPrefs.GetString("serverIP");
            if (ip != null && ip.Length > 0)
            {
                Debug.Log("[DocentNetStart:LoadConfig] loaded IP: " + ip);
                NetworkManager.singleton.networkAddress = ip;
            }
        }
    }

    public override void OnApplicationPause(bool pauseStatus)
    {
        //if we are pausing, disconnect from the server
        // so we don't end up in a weird state when we un-pause
        if (pauseStatus)
        {
            Network.Disconnect();
        }
        base.OnApplicationPause(pauseStatus);
    }
}
