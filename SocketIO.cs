using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using System;
using Assets.Scripts;

public class SocketIO : MonoBehaviour {
    public readonly string serverURL = "http://localhost:3000";

    public delegate void GameListingReturn(GameListing);

    protected Socket socket = null;

    // Use this for initialization
    void Start () {
        print("test");
        openConnection();
    }
    void openConnection()
    {
        if (socket == null)
        {
            print("Attempting to connect to " + serverURL);
            socket = IO.Socket(serverURL);
            socket.On(Socket.EVENT_CONNECT, onConnection);
            socket.On(Socket.EVENT_CONNECT_ERROR, (e) => print("connection fail:"+e.ToString()));
            socket.On(Socket.EVENT_CONNECT_TIMEOUT, (e) => print("connection timeout:" + e.ToString()));
            socket.On(Socket.EVENT_ERROR, (e) => print("Error:" + e.ToString()));
            socket.On(Socket.EVENT_DISCONNECT, () => print("Disconnected"));
            socket.On("pong", () => {
                print("pong");
                getGames((GameListing listing) => {
                    if (listing.id == "1") joinPubGame(listing.id);
                });
            });
        }
    }
    void onConnection()
    {
        print("Connection Successuful");
        print("ping testing...");
        socket.Emit("ping");
    }
    void getGames(GameListingReturn func)
    {
        print("Requesting Game Data..");
        socket.On("gameListings", (data) => {
            print("data recived");
            print(data.ToString());
            GameListing[] gameListings = JsonConvert.DeserializeObject<GameListing[]>(data.ToString());
            foreach(var gameListing in gameListings) func(gameListing);
        });
        socket.Emit("gameListings");
    }
    void joinPubGame(string gameId = "")
    {
        socket.Emit("requestPublic", gameId);
    }
    void joinPrivateGame(string gameId)
    {
        socket.Emit("requestPrivate", gameId);
    }
	// Update is called once per frame
	void Update () {
		
	}
}