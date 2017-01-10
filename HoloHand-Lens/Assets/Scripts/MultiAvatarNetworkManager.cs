﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiAvatarNetworkManager : NetworkManager {

    //subclass for sending network messages
    public class AvatarMessage : MessageBase
    {
        //default to index 0 which should be a "generic" avatar
        public int avatarIndex = 0;
        public string avatarName = "";
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        AvatarMessage message = extraMessageReader.ReadMessage<AvatarMessage>();
        int selectedAvatar = message.avatarIndex;
        Debug.Log("[MultiAvatarNetworkManager:OnServerAddPlayer] server add with message " + selectedAvatar);
        GameObject player = Instantiate(spawnPrefabs[selectedAvatar]);
        //if this is a hololens, lets add it's extra data for the Docent UI
        HololensAvatarLogic hlPlayer = player.GetComponent<HololensAvatarLogic>();
        if (hlPlayer != null)
        {
            hlPlayer.IP = conn.address;
            hlPlayer.ID = message.avatarName;
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("[MultiAvatarNetworkManager:OnClientConnect]");
        AvatarMessage msg = new AvatarMessage();
        for (int i = 0; i < spawnPrefabs.Count; i++)
        {
            if (spawnPrefabs[i].Equals(playerPrefab))
            {
                msg.avatarIndex = i;
#if UNITY_WSA_10_0
                msg.avatarName = Config.instance.id;
#endif
                break;
            }
        }

        ClientScene.AddPlayer(conn, 0, msg);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }
}
