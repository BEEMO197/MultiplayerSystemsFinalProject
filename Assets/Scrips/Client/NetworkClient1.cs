﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using UnityEngine.UIElements;
using System.Linq.Expressions;

public class NetworkClient1 : MonoBehaviour
{
    public UdpClient udp;
    // Start is called before the first frame update
    public GameState gameState;
    public GameObject cubeRef;

    void Start()
    {
        udp = new UdpClient();

        // 52.203.158.53 - Server Ip
        // localhost - Local Ip

        udp.Connect("52.203.158.53", 12345);

        Byte[] sendBytes = Encoding.ASCII.GetBytes("connect");

        udp.Send(sendBytes, sendBytes.Length);

        udp.BeginReceive(new AsyncCallback(OnReceived), udp);

        float repeatTime = 1.0f / 200.0f;
        InvokeRepeating("HeartBeat", repeatTime, repeatTime);

    }

    void OnDestroy()
    {
        udp.Dispose();
    }


    public enum commands
    {
        NEW_CLIENT,
        UPDATE,
        LOST_CLIENT,
        UNIQUE_CLIENT_ID
    };

    [Serializable]
    public class Message
    {
        public commands cmd;
    }

    [Serializable]
    public class Player
    {
        [Serializable]
        public struct receivedColor
        {
            public float R;
            public float G;
            public float B;
        }

        public receivedColor color;
        public Vector3 position;
        public string id;
        public GameObject cube = null;
    }

    [Serializable]
    public class NewPlayer
    {
        public Player newPlayer;
        public Player[] players;
    }

    [Serializable]
    public class DiePlayer
    {
        public Player lostPlayer;
    }

    [Serializable]
    public class GameState
    {
        public Player[] players;
    }

    public struct playerUniqueID
    {
        public string uniqueID;
    }

    public struct PlayerData
    {
        public Vector3 playerLocation;
        public string heartbeat;
    }

    public playerUniqueID uniqueID;

    public PlayerData playerData;
    public Message latestMessage;
    public GameState lastestGameState;
    public NewPlayer lastestNewPlayer;
    public DiePlayer lastestLostPlayer;

    public List<Player> PlayerList;

    public bool newPlayerSpawned = false;

    void OnReceived(IAsyncResult result)
    {
        // this is what had been passed into BeginReceive as the second parameter:
        UdpClient socket = result.AsyncState as UdpClient;

        // points towards whoever had sent the message:
        IPEndPoint source = new IPEndPoint(0, 0);

        // get the actual message and fill out the source:
        byte[] message = socket.EndReceive(result, ref source);

        // do what you'd like with `message` here:
        string returnData = Encoding.ASCII.GetString(message);
        Debug.Log("Got this: " + returnData);

        latestMessage = JsonUtility.FromJson<Message>(returnData);
        try
        {
            switch (latestMessage.cmd)
            {
                case commands.NEW_CLIENT:
                    lastestNewPlayer = JsonUtility.FromJson<NewPlayer>(returnData);
                    newPlayerSpawned = true;
                    break;

                case commands.UPDATE:
                    lastestGameState = JsonUtility.FromJson<GameState>(returnData);
                    break;

                case commands.LOST_CLIENT:
                    lastestLostPlayer = JsonUtility.FromJson<DiePlayer>(returnData);
                    break;

                case commands.UNIQUE_CLIENT_ID:
                    uniqueID = JsonUtility.FromJson<playerUniqueID>(returnData);
                    break;

                default:
                    Debug.Log("Error");
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        // schedule the next receive operation once reading is done:
        socket.BeginReceive(new AsyncCallback(OnReceived), socket);
    }

    void SpawnPlayers()
    {
        if (newPlayerSpawned)
        {
            // Debug.Log(lastestNewPlayer.newPlayer.id);
            if (lastestNewPlayer.players != null)
            {
                foreach (Player player in lastestNewPlayer.players)
                {
                    PlayerList.Add(player);
                    PlayerList.Last().cube = Instantiate(cubeRef);
                    PlayerList.Last().cube.transform.position = player.position;
                    PlayerList.Last().cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(player.color.R, player.color.G, player.color.B));
                    //PlayerList.Last().cube.transform.position = player.position;
                }
            }

            PlayerList.Add(lastestNewPlayer.newPlayer);
            PlayerList.Last().cube = Instantiate(cubeRef);
            //PlayerList.Last().cube.GetComponent<Character>().networkManRef = this;
            PlayerList.Last().cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(lastestNewPlayer.newPlayer.color.R, lastestNewPlayer.newPlayer.color.G, lastestNewPlayer.newPlayer.color.B));

            playerData.playerLocation = new Vector3(0.0f, 0.0f, 0.0f);
            newPlayerSpawned = false;
        }
    }

    void UpdatePlayers()
    {
        //for (int i = 0; i < lastestGameState.players.Length; i++)
        //{
        //    for (int k = i; k < PlayerList.Count(); k++)
        //    {
        //        if (lastestGameState.players[i].id == PlayerList[k].id)
        //        {
        //            //PlayerList[k].color = lastestGameState.players[i].color;
        //            PlayerList[k].cube.GetComponent<Character>().playerRef = lastestGameState.players[i];
        //        }
        //        else
        //        {
        //            // Set Position of everyone else thats not the client
        //            PlayerList[k].cube.transform.position = lastestGameState.players[k].position;
        //        }
        //    }
        //}

        // Loop though players to find controlling Client
        foreach (Player player in PlayerList)
        {
            if (player.id == uniqueID.uniqueID)
            {
                // Loop through the servers players to check for our player, and get the ref
                foreach (Player serverPlayer in lastestGameState.players)
                {
                    if (serverPlayer.id == player.id)
                    {
                        // set our player ref inside our controller
                        //player.cube.GetComponent<Character>().playerRef = serverPlayer;
                    }
                }
            }

            // Update other Players
            else
            {
                foreach (Player serverPlayer in lastestGameState.players)
                {
                    if (serverPlayer.id == player.id)
                    {
                        player.cube.transform.position = serverPlayer.position;
                    }
                }
            }
        }

        // Inside Player Cube Script
    }

    void DestroyPlayers()
    {
        if (lastestLostPlayer.lostPlayer != null)
        {
            foreach (Player player in PlayerList)
            {
                if (player.Equals(lastestLostPlayer.lostPlayer))
                {
                    PlayerList.Remove(player);
                }
            }
        }
    }
    void HeartBeat()
    {
        playerData.playerLocation = new Vector3(0.0f, 0.0f, 0.0f);

        foreach (Player player in PlayerList)
        {
            if (player.id == uniqueID.uniqueID)
            {
                playerData.playerLocation = player.cube.transform.position;
                continue;
            }
        }

        playerData.heartbeat = "heartbeat";

        Byte[] sendBytes = Encoding.ASCII.GetBytes(JsonUtility.ToJson(playerData));
        udp.Send(sendBytes, sendBytes.Length);
    }

    void Update()
    {
        SpawnPlayers();
        UpdatePlayers();
        DestroyPlayers();
    }
}