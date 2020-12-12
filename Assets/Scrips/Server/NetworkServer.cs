﻿using UnityEngine;
using UnityEngine.Assertions;
using Unity.Collections;
using Unity.Networking.Transport;
using NetworkMessages;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class NetworkServer : MonoBehaviour
{
    public float updateTime = 1.0f / 30.0f;
    public NetworkDriver m_Driver;
    public ushort serverPort;
    private NativeList<NetworkConnection> m_Connections;

    public List<NetworkObjects.NetworkPlayer> serverPlayerList = new List<NetworkObjects.NetworkPlayer>();

    void Start ()
    {
        m_Driver = NetworkDriver.Create();
        var endpoint = NetworkEndPoint.AnyIpv4;
        endpoint.Port = serverPort;
        if (m_Driver.Bind(endpoint) != 0)
            Debug.Log("Failed to bind to port " + serverPort);
        else
            m_Driver.Listen();

        m_Connections = new NativeList<NetworkConnection>(16, Allocator.Persistent);

        StartCoroutine(UpdatePlayers());
    }

    IEnumerator UpdatePlayers()
    {
        while (true)
        {
            ServerUpdateMsg suMsg = new ServerUpdateMsg();
            suMsg.players = serverPlayerList;

            foreach (var connection in m_Connections)
            {
                if (connection.IsCreated)
                {
                    SendToClient(JsonUtility.ToJson(suMsg), connection);
                }
            }

            yield return new WaitForSeconds(updateTime);
        }
    }

    void SendToClient(string message, NetworkConnection c)
    {
        var writer = m_Driver.BeginSend(NetworkPipeline.Null, c);
        NativeArray<byte> bytes = new NativeArray<byte>(Encoding.ASCII.GetBytes(message),Allocator.Temp);
        writer.WriteBytes(bytes);
        m_Driver.EndSend(writer);
    }
    public void OnDestroy()
    {
        m_Driver.Dispose();
        m_Connections.Dispose();
    }

    void OnConnect(NetworkConnection c)
    {
        m_Connections.Add(c);
        Debug.Log("Accepted a connection");

        // Example to send a handshake message:
        HandshakeMsg m = new HandshakeMsg();
        m.player.id = c.InternalId.ToString();
        SendToClient(JsonUtility.ToJson(m), c);

        NetworkObjects.NetworkPlayer connectingPlayer = new NetworkObjects.NetworkPlayer();

        connectingPlayer.id = c.InternalId.ToString();
        connectingPlayer.cubeColor = UnityEngine.Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
        connectingPlayer.cubPos = new Vector3(0.0f, 0.0f, 0.0f);
        connectingPlayer.cubRot = new Quaternion();

        for (int i = 0; i < m_Connections.Length; i++)
        {
            if (m_Connections[i] != c)
            {
                PlayerJoinMessage pjMsg = new PlayerJoinMessage();
                pjMsg.player = connectingPlayer;
                SendToClient(JsonUtility.ToJson(pjMsg), m_Connections[i]);
            }
        }

        serverPlayerList.Add(connectingPlayer);

        foreach (NetworkObjects.NetworkPlayer player in serverPlayerList)
        {
            PlayerJoinMessage pjMsg2 = new PlayerJoinMessage();
            pjMsg2.player = player;
            SendToClient(JsonUtility.ToJson(pjMsg2), c);
        }
    }

    void OnData(DataStreamReader stream, int i){
        NativeArray<byte> bytes = new NativeArray<byte>(stream.Length,Allocator.Temp);
        stream.ReadBytes(bytes);
        string recMsg = Encoding.ASCII.GetString(bytes.ToArray());
        NetworkHeader header = JsonUtility.FromJson<NetworkHeader>(recMsg);

        switch(header.cmd){
            case Commands.HANDSHAKE:
                HandshakeMsg hsMsg = JsonUtility.FromJson<HandshakeMsg>(recMsg);
                Debug.Log("Handshake message received!");
                break;

            case Commands.PLAYER_UPDATE:
                PlayerUpdateMsg puMsg = JsonUtility.FromJson<PlayerUpdateMsg>(recMsg);
                Debug.Log("Player update message received!");

                foreach (NetworkObjects.NetworkPlayer player in serverPlayerList)
                {
                    if (player.id == puMsg.player.id)
                    {
                        player.cubeColor = puMsg.player.cubeColor;
                        player.cubPos = puMsg.player.cubPos;
                        player.cubRot = puMsg.player.cubRot;
                    }
                }
                break;

            case Commands.PLAYER_JOINED:
                PlayerJoinMessage pjMsg = JsonUtility.FromJson<PlayerJoinMessage>(recMsg);
                Debug.Log("Player join message received!");
                break;

            case Commands.SERVER_UPDATE:
                ServerUpdateMsg suMsg = JsonUtility.FromJson<ServerUpdateMsg>(recMsg);
                Debug.Log("Server update message received!");
                break;

            case Commands.PLAYER_LEFT:
                PlayerLeaveMsg plMsg = JsonUtility.FromJson<PlayerLeaveMsg>(recMsg);
                Debug.Log("Player leave message recieved!");
                KillServerPlayer(plMsg.player);
                break;

            default:
                Debug.Log("SERVER ERROR: Unrecognized message received!");
                break;
        }
    }

    void KillServerPlayer(NetworkObjects.NetworkPlayer leavingPlayer)
    {
        foreach (NetworkObjects.NetworkPlayer player in serverPlayerList)
        {
            if (player.id == leavingPlayer.id)
            {
                serverPlayerList.Remove(player);
            }
        }
    }

    void OnDisconnect(int i){
        Debug.Log("Client disconnected from server");

        PlayerLeaveMsg plMsg = new PlayerLeaveMsg();

        foreach (NetworkObjects.NetworkPlayer player in serverPlayerList)
        {
            if (player.id == m_Connections[i].InternalId.ToString())
            {
                plMsg.player = player;
                KillServerPlayer(plMsg.player);
            }
        }

        foreach (var client in m_Connections)
        {
            if (client != m_Connections[i])
            {
                SendToClient(JsonUtility.ToJson(plMsg), client);
            }
        }

        m_Connections[i] = default(NetworkConnection);
    }

    void Update ()
    {
        m_Driver.ScheduleUpdate().Complete();

        // CleanUpConnections
        for (int i = 0; i < m_Connections.Length; i++)
        {
            if (!m_Connections[i].IsCreated)
            {

                m_Connections.RemoveAtSwapBack(i);
                --i;
            }
        }

        // AcceptNewConnections
        NetworkConnection c = m_Driver.Accept();
        while (c  != default(NetworkConnection))
        {            
            OnConnect(c);

            // Check if there is another new connection
            c = m_Driver.Accept();
        }

        // Read Incoming Messages
        DataStreamReader stream;
        for (int i = 0; i < m_Connections.Length; i++)
        {
            Assert.IsTrue(m_Connections[i].IsCreated);
            
            NetworkEvent.Type cmd;
            cmd = m_Driver.PopEventForConnection(m_Connections[i], out stream);
            while (cmd != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    OnData(stream, i);
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    OnDisconnect(i);
                }

                cmd = m_Driver.PopEventForConnection(m_Connections[i], out stream);
            }
        }
    }
}