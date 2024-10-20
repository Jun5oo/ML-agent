using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    GameObject agent;
    float bullet_speed;
    float bullet_random;
    float board_halfWidth; 

    public void SetBullet(GameObject _agent, float _bullet_speed, float _bullet_random, float _board_halfWidth)
    {
        agent = _agent; 
        bullet_speed = _bullet_speed;
        bullet_random = _bullet_random;
        board_halfWidth = _board_halfWidth - 1f;

        RandomBullet(); 
    }

    private void RandomBullet()
    {
        float theta = Random.Range(0, 2 * Mathf.PI);
        transform.localPosition = new Vector3(board_halfWidth * Mathf.Cos(theta), 0.5f, board_halfWidth * Mathf.Sin(theta));
        float randomAngle = Mathf.Atan2(agent.transform.localPosition.z - transform.localPosition.z, agent.transform.localPosition.x - transform.localPosition.x) + Random.Range(-bullet_random, bullet_random);

        float randomSpeed = bullet_speed + Random.Range(-0.5f * bullet_random, 0.5f * bullet_random);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(randomSpeed * Mathf.Cos(randomAngle), 0, randomSpeed * Mathf.Sin(randomAngle));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
            RandomBullet(); 
    }
}
