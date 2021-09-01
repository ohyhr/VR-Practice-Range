using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int force = 20;
    public float lifetime = 10;

    public void Launch()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
        Destroy(gameObject, lifetime);
    }
}
