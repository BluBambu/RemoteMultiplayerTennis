using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    public GameObject PlayerRacketPrefab;

    [SyncVar(hook = nameof(SetRacketRotation))]
    private Quaternion RacketRotation;

    private Transform Racket;

    public override void OnStartLocalPlayer() 
    {
        Debug.Log("Initializing local player");

        GetComponent<Camera>().enabled = true;
    }

    private void Awake()
    {
        Racket = Instantiate(PlayerRacketPrefab).transform;
        Racket.parent = transform;
        Racket.localPosition = new Vector3(0.15f, -0.01f, 0.4f);
    }

    private void OnUpdate() 
    {
        if (isLocalPlayer)
        {
            var eulerAngle = RacketRotation.eulerAngles;
            RacketRotation = Quaternion.Euler(eulerAngle + Vector3.right * Time.deltaTime * 10);
            SetRacketRotation(RacketRotation);
        }
    }

    private void SetRacketRotation(Quaternion rotation)
    {
        Racket.rotation = rotation;
    }
}
