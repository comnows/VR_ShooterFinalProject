using UnityEngine;
using UnityEngine.Animations;
using System.Collections.Generic;
using Normal.Realtime;

public class PlayerManager : MonoBehaviour {
    [SerializeField] private GameObject _prefab;

    private Realtime _realtime;
    
    private void Awake() 
    {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime) 
    {
        // Instantiate the CubePlayer for this client once we've successfully connected to the room. Position it 1 meter in the air.
        var options = new Realtime.InstantiateOptions {
            ownedByClient            = true,    // Make sure the RealtimeView on this prefab is owned by this client.
            preventOwnershipTakeover = true,    // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
            useInstance              = realtime // Use the instance of Realtime that fired the didConnectToRoom event.
            };
        GameObject playerGameObject = Realtime.Instantiate(_prefab.name, options);

        GameObject camera = playerGameObject.transform.GetChild(1).gameObject;
        camera.SetActive(true);
    }
}