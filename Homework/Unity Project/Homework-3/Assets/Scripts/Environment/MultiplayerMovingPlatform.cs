using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

/// I've been fighting with this code for about 4 days, but it won. 
/// I tried all sorts of things, asked every AI I could find for help—nothing worked. 
/// The current version kind of works, but it's written in the most brute-force, hacky way possible. 
/// I also had issues with Network Transform—while using it, all platforms were spawning at Vector3.zero. 
/// Took me 3 days to figure out that was the cause. 
/// Now, when clients join the server, the platforms slowly move to the correct positions. 
/// At least they stay where they're supposed to. 
/// If someone manages to solve this properly—good luck. I couldn't.
public class MultiplayerMovingPlatform : NetworkBehaviour
{
    [SerializeField]
    private Transform pointL;
    [SerializeField]
    private Transform pointR;

    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private bool startToRight = true;


    private NetworkVariable<Vector3> nextPosition = new NetworkVariable<Vector3>(writePerm: NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            nextPosition.Value = startToRight ? pointR.position : pointL.position;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition.Value, speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!IsServer || !col.CompareTag("Boundary")) return;

        if (col.gameObject == pointL.gameObject)
        {
            nextPosition.Value = pointR.position;
        }
        else if (col.gameObject == pointR.gameObject)
        {
            nextPosition.Value = pointL.position;
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////// This part kind of works, but has unwanted behavior, 
    /////////////////////////////////////////////////////////////////////////////////////so thst's why in the multiplayer version player does not travell with platform
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (IsClient && collision.gameObject.CompareTag("Player"))
    //    {
    //        NetworkObject netObj = collision.gameObject.GetComponent<NetworkObject>();
    //        if (netObj != null)
    //        {
    //            ReparentServerRpc(netObj, true);
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (IsClient && collision.gameObject.CompareTag("Player"))
    //    {
    //        NetworkObject netObj = collision.gameObject.GetComponent<NetworkObject>();
    //        if (netObj != null)
    //        {
    //            ReparentServerRpc(netObj, false);
    //        }
    //    }
    //}

    //[ServerRpc(RequireOwnership = false)]
    //private void ReparentServerRpc(NetworkObjectReference playerRef, bool isOn)
    //{
    //    if (playerRef.TryGet(out NetworkObject netObj))
    //    {
    //        netObj.transform.parent = isOn ? transform : null;
    //        ReparentClientRpc(playerRef, isOn);
    //    }
    //}

    //[ClientRpc]
    //private void ReparentClientRpc(NetworkObjectReference playerRef, bool isOn)
    //{
    //    if (playerRef.TryGet(out NetworkObject netObj))
    //    {
    //        netObj.transform.parent = isOn ? transform : null;
    //    }
    //}
}