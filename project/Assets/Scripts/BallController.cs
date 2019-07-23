using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private const float MinimumYVelocity = 4f;

    private Rigidbody rigidbody;

    private bool hasDoneForBounce;

    private void Start()
    {
        hasDoneForBounce = false;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(50, 0, 200);
    }

    private void Update()
    {
        if ((rigidbody.velocity.y > 0) && rigidbody.velocity.y < MinimumYVelocity && !hasDoneForBounce)
        {
            rigidbody.velocity = new Vector3(
                rigidbody.velocity.x,
                MinimumYVelocity, 
                rigidbody.velocity.z);

            hasDoneForBounce = true;
        }
        else if (rigidbody.velocity.y < 0)
        {
            hasDoneForBounce = false;
        }
    }
}
