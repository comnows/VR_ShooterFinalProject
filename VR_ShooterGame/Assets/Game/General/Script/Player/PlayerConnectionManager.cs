using UnityEngine;
using UnityEngine.Animations;
using System.Collections.Generic;
using System.Collections;
using Normal.Realtime;

public class PlayerConnectionManager : MonoBehaviour {
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _prefabVRPlayer;
    [SerializeField] private GameObject pcButton;
    [SerializeField] private GameObject vrButton;
    public GameObject spawnPoint;
    private Realtime _realtime;
    GameObject playerGameObject;
    private UIAmmo uIAmmo;
    private UIScore uIScore;
    private RealtimeTransform _realtimeTransform;

    public enum Platform {PC,VR};
    public Platform currentPlatform;

    private void Awake() 
    {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;

        uIAmmo = GameObject.FindObjectOfType<UIAmmo>();
        uIScore = GameObject.FindObjectOfType<UIScore>();
    }

    public void SetPCPlatform()
    {
        Debug.Log("MillPCPress");
        currentPlatform = Platform.PC;
        CloseButton();
    }

    public void SetVRPlatform()
    {
        Debug.Log("MillVRPress");
        currentPlatform = Platform.VR;
        CloseButton();
    }
    
    private void DidConnectToRoom(Realtime realtime) 
    {
        // Instantiate the CubePlayer for this client once we've successfully connected to the room. Position it 1 meter in the air.
        var options = new Realtime.InstantiateOptions {
            ownedByClient            = true,    // Make sure the RealtimeView on this prefab is owned by this client.
            preventOwnershipTakeover = true,    // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
            useInstance              = realtime // Use the instance of Realtime that fired the didConnectToRoom event.
            };

        switch (currentPlatform)
        {
        case Platform.PC:
            playerGameObject = Realtime.Instantiate(_prefab.name, options);
            CreatePCPlayer();
            break;
        case Platform.VR:
            playerGameObject = Realtime.Instantiate(_prefabVRPlayer.name, options);
            break;
        default:
            playerGameObject = Realtime.Instantiate(_prefab.name, options);
            CreatePCPlayer();
            break;
        }
        
        //GameObject playerGameObject = Realtime.Instantiate(_prefab.name, options);

        playerGameObject.transform.position = spawnPoint.transform.position;

        ChangePlayerName(playerGameObject);
        CloseButton();
        // RealtimeView _realtimeView = playerGameObject.GetComponent<RealtimeView>();  
        // if (_realtimeView.isOwnedLocallyInHierarchy)
        // {       
        //     uIAmmo.InitScript(playerGameObject);
        //     uIScore.InitScript(playerGameObject);
        // }
    }

    private void CreatePCPlayer()
    {
        GameObject playerGameObjectChild = playerGameObject.transform.GetChild(0).gameObject;

        GameObject cameraHolder = playerGameObjectChild.transform.GetChild(1).gameObject;

        GameObject cameraRecoil = cameraHolder.transform.GetChild(0).gameObject;

        GameObject weaponCamera = cameraRecoil.transform.GetChild(0).gameObject;

        GameObject normalCamera = cameraRecoil.transform.GetChild(1).gameObject;

        weaponCamera.GetComponent<Camera>().enabled = true;

        normalCamera.SetActive(true);

        RealtimeView _realtimeView = playerGameObject.GetComponent<RealtimeView>();  
        if (_realtimeView.isOwnedLocallyInHierarchy)
        {       
            uIAmmo.InitScript(playerGameObject);
            uIScore.InitScript(playerGameObject);
        }
    }

    private void CloseButton()
    {
        pcButton.SetActive(false);
        vrButton.SetActive(false);
    }

    private void ChangePlayerName(GameObject connectedPlayer)
    {
        GameObject [] allPlayerInGame = GameObject.FindGameObjectsWithTag("Player");
        string connectedPlayerName = "Player " + allPlayerInGame.Length;
        connectedPlayer.GetComponent<PlayerSyncData>().ChangedPlayerName(connectedPlayerName);
    }
}