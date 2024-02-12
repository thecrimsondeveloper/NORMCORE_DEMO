using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.Events;
using static Normal.Realtime.Realtime;

public class RealtimeManager : MonoBehaviour
{

    public UnityEvent OnJoinedRoomEvent = new UnityEvent();
    public UnityEvent OnLeftRoomEvent = new UnityEvent();

    private Realtime _realtime;

    private void Awake()
    {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;
        _realtime.didDisconnectFromRoom += DidDisconnectFromRoom;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        OnJoinedRoomEvent.Invoke();
    }

    private void DidDisconnectFromRoom(Realtime realtime)
    {
        OnLeftRoomEvent.Invoke();
    }



    public void ConnectToRoom()
    {
        _realtime.Connect("PongRoom");
    }

    public void DisconnectFromRoom()
    {
        _realtime.Disconnect();
    }


}
