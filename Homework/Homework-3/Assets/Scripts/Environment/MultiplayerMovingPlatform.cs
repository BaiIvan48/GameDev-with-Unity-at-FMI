using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class MultiplayerMovingPlatform : NetworkBehaviour
{
    [SerializeField] private Transform pointL;
    [SerializeField] private Transform pointR;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool startToRight = true;

    private Vector3 nextPosition;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            nextPosition = startToRight ? pointR.position : pointL.position;
        }
    }

    private void FixedUpdate()
    {
        if (IsServer)
        {
            Vector3 currentPosition = transform.position;
            float time = speed * Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(currentPosition, nextPosition, time);

            ShowClientRpc(currentPosition, nextPosition,time);
        }
    }

    [ClientRpc]
    private void ShowClientRpc(Vector3 current, Vector3 target, float maxDistanceDelta)
    {
        transform.position = Vector3.MoveTowards(current, target, maxDistanceDelta);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (IsServer && col.CompareTag("Boundary"))
        {
            nextPosition = (col.transform == pointL) ? pointR.position : pointL.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetParentServerRpc(collision.gameObject.GetComponent<NetworkObject>().NetworkObjectId, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetParentServerRpc(collision.gameObject.GetComponent<NetworkObject>().NetworkObjectId, false);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetParentServerRpc(ulong playerId, bool attach)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(playerId, out NetworkObject playerObject))
        {
            playerObject.transform.SetParent(attach ? transform : null, true);
            SetParentClientRpc(playerObject.NetworkObjectId, attach);
        }
    }

    [ClientRpc]
    private void SetParentClientRpc(ulong playerId, bool attach)
    {
        if (!IsServer && NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(playerId, out NetworkObject playerObject))
        {
            playerObject.transform.SetParent(attach ? transform : null, true);
        }
    }
}


//public class MovingPlatform : MonoBehaviour
//{
//    [SerializeField]
//    private Transform pointL;
//    [SerializeField]
//    private Transform pointR;

//    [SerializeField]
//    private float speed = 2;
//    [SerializeField]
//    private bool startToRight = true;

//    private Vector3 nextPosition;

//    void Start()
//    {
//        nextPosition = startToRight ? pointR.position : pointL.position;
//    }

//    private void FixedUpdate()
//    {
//        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.fixedDeltaTime);
//    }

//    void OnTriggerEnter2D(Collider2D col)
//    {
//        if (col.CompareTag("Boundary"))
//        {
//            nextPosition = (col.transform == pointL) ? pointR.position : pointL.position;
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            collision.gameObject.transform.parent = transform;
//        }
//    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            collision.gameObject.transform.parent = null;
//        }
//    }

//}