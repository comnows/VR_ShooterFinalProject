using UnityEngine;
using UnityEngine.Animations;
using System.Collections.Generic;
using Normal.Realtime;

public class PlayerConnectionManager : MonoBehaviour {
    [SerializeField] private GameObject _prefab;

    public GameObject spawnPoint;
    private Realtime _realtime;
    private UIAmmo uIAmmo;
    private void Awake() 
    {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;

        uIAmmo = GameObject.FindObjectOfType<UIAmmo>();
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

        playerGameObject.transform.position = spawnPoint.transform.position;

        GameObject playerGameObjectChild = playerGameObject.transform.GetChild(0).gameObject;

        GameObject cameraHolder = playerGameObjectChild.transform.GetChild(1).gameObject;

        GameObject cameraRecoil = cameraHolder.transform.GetChild(0).gameObject;

        GameObject weaponCamera = cameraRecoil.transform.GetChild(0).gameObject;

        GameObject normalCamera = cameraRecoil.transform.GetChild(1).gameObject;

        weaponCamera.GetComponent<Camera>().enabled = true;

        normalCamera.SetActive(true);

        RealtimeView _realtimeView = playerGameObject.GetComponent<RealtimeView>();  
        if (_realtimeView.isOwnedLocallyInHierarchy)       
         uIAmmo.InitScript(playerGameObject);
    }
}