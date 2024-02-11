using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;
using static Normal.Realtime.Realtime;

public class RealtimeManager : MonoBehaviour
{


    private Realtime _realtime;

    private void Awake()
    {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;

        _realtime.Connect("PongRoom");
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        Debug.Log("Connected to room!");
    }
}
