using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Agent"))
        {
            Destroy(this.gameObject);
            // Manager.Pool.Push(this.gameObject); 
        }
    }
}
