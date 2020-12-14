using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkMessages
{
    public enum Commands{
        PLAYER_UPDATE,
        PLAYER_BULLET,
        SERVER_UPDATE,
        PLAYER_JOINED,
        PLAYER_LEFT,
        HANDSHAKE,
        PLAYER_INPUT
    }

    [System.Serializable]
    public class NetworkHeader{
        public Commands cmd;
    }

    [System.Serializable]
    public class HandshakeMsg:NetworkHeader{
        public NetworkObjects.NetworkPlayer player;
        public HandshakeMsg(){      // Constructor
            cmd = Commands.HANDSHAKE;
            player = new NetworkObjects.NetworkPlayer();
        }
    }

    [System.Serializable]
    public class PlayerJoinMessage : NetworkHeader
    {
        public NetworkObjects.NetworkPlayer player;
        public PlayerJoinMessage()
        {      // Constructor
            cmd = Commands.PLAYER_JOINED;
            player = new NetworkObjects.NetworkPlayer();
        }
    }

    [System.Serializable]
    public class PlayerUpdateMsg:NetworkHeader{
        public NetworkObjects.NetworkPlayer player;
        public PlayerUpdateMsg(){      // Constructor
            cmd = Commands.PLAYER_UPDATE;
            player = new NetworkObjects.NetworkPlayer();
        }
    };

    [System.Serializable]
    public class PlayerBulletMsg : NetworkHeader
    {
        public NetworkObjects.NetworkPlayer player;
        public Vector3 clickedLocation;
        public float range;
        public float bulletSpeed;

        public PlayerBulletMsg()
        {      // Constructor
            cmd = Commands.PLAYER_UPDATE;
            player = new NetworkObjects.NetworkPlayer();
        }
    };

    public class PlayerInputMsg:NetworkHeader{
        public Input myInput;
        public PlayerInputMsg(){
            cmd = Commands.PLAYER_INPUT;
            myInput = new Input();
        }
    }

    public class PlayerLeaveMsg : NetworkHeader
    {
        public NetworkObjects.NetworkPlayer player;
        public PlayerLeaveMsg()
        {      // Constructor
            cmd = Commands.PLAYER_LEFT;
            player = new NetworkObjects.NetworkPlayer();
        }
    }

    [System.Serializable]
    public class  ServerUpdateMsg:NetworkHeader{
        public List<NetworkObjects.NetworkPlayer> players;
        public ServerUpdateMsg(){      // Constructor
            cmd = Commands.SERVER_UPDATE;
            players = new List<NetworkObjects.NetworkPlayer>();
        }
    }
} 

namespace NetworkObjects
{
    [System.Serializable]
    public class NetworkObject{
        public string id;
    }
    [System.Serializable]
    public class NetworkPlayer : NetworkObject{
        
        // Player Render Variables
        public GameObject cube;
        public Color cubeColor;
        public Vector3 cubPos;
        public Quaternion cubRot;

        // Player Game Variables
        public string Username;
        public Classes playerClass;
        public float health;
        public int level;
        public int score;

        // Bullet Variables
        public float bulletSpeed;
        public float range;

        public NetworkPlayer(){
            cubeColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }

    [System.Serializable]
    public struct NetworkPlayerJson
    {
        public string NewUserCheck;

        public string Username;
        public string Password;
        public string Level;
        public string Score;
        public string Health;

        public string playerClass;
    }
}
