using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;

using UnityEngine;
using static Normal.Realtime.Realtime;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    Realtime realtime;

    private void Awake()
    {
        realtime = GetComponent<Realtime>();
        realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        Debug.Log("Connected to room!");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {





        //instantiates the player prefab using realtime.Instantiate
        GameObject player = Realtime.Instantiate(playerPrefab.name,
                                ownedByClient: true,
                                preventOwnershipTakeover: true,
                                useInstance: realtime);
    }


}
