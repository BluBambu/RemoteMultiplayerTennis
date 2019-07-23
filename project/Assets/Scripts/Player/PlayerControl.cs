using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerControl : NetworkBehaviour
{
    public GameObject PlayerRacketPrefab;

    [SyncVar(hook = nameof(SetRacketRotation))]
    private Quaternion RacketRotation;
    private Quaternion BaseRacketRotation;

    private Transform Racket;

    private Rigidbody BallRigidBody;
    private Transform BallTransform;

    private void Awake()
    {
        BaseRacketRotation = Quaternion.identity;
        BallRigidBody = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        BallTransform = GameObject.FindGameObjectWithTag("Ball").transform;
    }

    public override void OnStartLocalPlayer() 
    {
        Debug.Log("Initializing local player");

        transform.GetComponentInChildren<Camera>().enabled = true;

        if (isLocalPlayer)
        {
            var resetGyroButton = GameObject.FindGameObjectWithTag("ResetGyroButton");
            resetGyroButton.GetComponent<Button>().onClick.AddListener(ResetBaseGyro);
        }
    }

    private void ResetBaseGyro()
    {
        BaseRacketRotation = Quaternion.Inverse(Racket.localRotation);
    }

    private void Start()
    {
        Racket = Instantiate(PlayerRacketPrefab).transform;
        Racket.parent = transform;
        Racket.localPosition = new Vector3(0.15f, -0.01f, 0.4f);
    }

    private void Update() 
    {
        if (isLocalPlayer)
        {
            if (!Input.gyro.enabled)
            {
                Input.gyro.enabled = true;
            }

            Debug.Log(Input.gyro.attitude);
            CmdUpdateRacketPos(new Quaternion(
                -Input.gyro.attitude.x,
                -Input.gyro.attitude.y,
                Input.gyro.attitude.z,
                Input.gyro.attitude.w));

            if (transform.position.z > 0)
            {
                if (BallRigidBody.velocity.z > 0)
                {
                    transform.position = new Vector3(
                        BallTransform.position.x,
                        BallTransform.position.y,
                        transform.position.z
                    );
                }
            }
            else if (transform.position.z < 0)
            {
                if (BallRigidBody.velocity.z < 0)
                {
                    transform.position = new Vector3(
                        BallTransform.position.x,
                        BallTransform.position.y,
                        transform.position.z
                    );
                }
            }
        }
    }

    [Command]
    private void CmdUpdateRacketPos(Quaternion racketRotation)
    {
        RacketRotation = racketRotation;
    }

    private void SetRacketRotation(Quaternion rotation)
    {
        Racket.localRotation = BaseRacketRotation * rotation;
    }
}
