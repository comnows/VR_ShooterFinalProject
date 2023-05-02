using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class MovePlayerToPoint : MonoBehaviour
{
    [SerializeField] private GameObject movePoint;
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("MillEnterNewLevel");
            other.GetComponent<RealtimeTransform>().ClearOwnership();
            other.transform.position = movePoint.transform.position;
            other.GetComponent<RealtimeTransform>().RequestOwnership();
        }
    }
}
