using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public GameObject agent; 
    Rigidbody rb;
    float speed = 20;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        agent = GameObject.FindGameObjectWithTag("Agent"); 
        direction = (agent.transform.position - this.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Agent"))
        {
            Destroy(this.gameObject); 
        }
    }
}
