using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    protected Rigidbody rb;

    // Use this for initialization
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
