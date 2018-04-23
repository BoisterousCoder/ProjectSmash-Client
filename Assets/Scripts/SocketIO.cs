using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;
using System.Diagnostics;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using Assets.Scripts.Server;

public class SocketIO : MonoBehaviour {

    void setHandlers()
    {
        Master.Wrapper
            .On(Socket.EVENT_CONNECT_ERROR, (e) => OnFail("connection fail:" + e.ToString()))
            .On(Socket.EVENT_CONNECT_TIMEOUT, (e) => OnFail("connection timeout:" + e.ToString()))
            .On(Socket.EVENT_ERROR, (e) => OnFail("Error:" + e.ToString()))
            .On(Socket.EVENT_DISCONNECT, (e) => print("Disconnected"))
            .On("warn", (msg) => print("The server is warning:" + msg.ToString()))
            .On("error", (msg) => print("The following server error occured: " + msg.ToString()))
            .On("log", (msg) => print("Server>> "+msg));

        print("Requesting Game Data..");
        //Game.Listings(Master.Wrapper, (GameListing listing) => 
        //{
        //    if (listing.id == 1) OnCharSelect(listing);
        //});
    }

    // Use this for initialization
    void Start () {
        Master.Wrapper = new JSWrapper(print, setHandlers);
    }

    void OnFail(string failmsg)
    {
        print("ERROR--"+failmsg);
    }
    void OnStart()
    {
        Master.Game.Move(22.61);
    }
    void OnCharSelect(GameListing listing)
    {
        Master.Game = new Game(Master.Wrapper, print, listing.id);
        Master.Game.SelectCharecter("template", (msg) => OnStart());
    }
    void OnApplicationQuit()
    {
        Master.Wrapper.ForceClose();
    }
}