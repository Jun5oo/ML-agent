using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents;
using System.Linq;
using Unity.Hierarchy;

public class MyAgent : Agent
{
    public Area area;
    public GameObject agentPos; 
    public GameObject player; 
    Rigidbody rb;

    float agentSpeed = 10f;

    public float DecisionWaitingTime = 0.01f;
    float m_currentTime = 0f;

    float prevDistance; 

    // BufferSensorComponent m_BufferSensor;
    // List<float> sensorDistList = new List<float>();

    float ray_length = 20f; 

    public override void Initialize()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        transform.position = agentPos.transform.position;

        Academy.Instance.AgentPreStep += WaitTimeInference; 
        // m_BufferSensor = gameObject.GetComponent<BufferSensorComponent>();  
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions; 
        discreteActionsOut[0] = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            discreteActionsOut[0] = 2;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            discreteActionsOut[0] = 3;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            discreteActionsOut[0] = 4;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        RaycastHit hit;
        Ray ray;

        float angle;
        int rayCount = 40;
        List<Vector3> debugRay = new List<Vector3>();

        for (int i = 0; i < rayCount; i++)
        {
            angle = i * 2.0f * Mathf.PI / rayCount; 
            ray = new Ray(gameObject.transform.position, new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));

            if(Physics.Raycast(ray, out hit, ray_length))
            {
                sensor.AddObservation(hit.distance); 
                // sensorDistList.Clear();
                // sensorDistList.Add(hit.distance * Mathf.Cos(angle) / ray_length); 
                // sensorDistList.Add(hit.distance * Mathf.Sin(angle) / ray_length); 

                if(hit.collider.gameObject.tag == "Wall")
                {
                    sensor.AddObservation(new Vector2(0, 0)); 
                    // sensorDistList.Add(0f);
                    // sensorDistList.Add(0f); 
                }
                else
                {
                    Rigidbody rig = hit.collider.gameObject.GetComponent<Rigidbody>();
                    var velocity = new Vector2(rig.velocity.x, rig.velocity.z); 
                    sensor.AddObservation(velocity);
                    // sensorDistList.Add(rig.velocity.x);
                    // sensorDistList.Add(rig.velocity.z);
                }

                debugRay.Add(hit.point);
               // m_BufferSensor.AppendObservation(sensorDistList.ToArray()); 
            }

            else
            {
                sensor.AddObservation(ray_length);
                sensor.AddObservation(new Vector2(0, 0)); 
            }


        }

        sensor.AddObservation(player.transform.position.x - transform.position.x); 
        sensor.AddObservation(player.transform.position.z - transform.position.z);

        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.z);

        for(int j=0; j<debugRay.Count; j++)
        {
            Debug.DrawRay(gameObject.transform.position, debugRay[j] - gameObject.transform.position, Color.green); 
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        rb.velocity = Vector3.zero; 

        var action = actions.DiscreteActions[0];

        Vector3 force = Vector3.zero;

        switch (action)
        {
            case 1: force = new Vector3(-1, 0, 0) * agentSpeed; break;
            case 2: force = new Vector3(0, 0, 1) * agentSpeed; break;
            case 3: force = new Vector3(0, 0, -1) * agentSpeed; break;
            case 4: force = new Vector3(1, 0, 0) * agentSpeed; break;
            default: force = new Vector3(0, 0, 0) * agentSpeed; break; 
        }

        // rb.AddForce(force, ForceMode.VelocityChange);
        rb.velocity = force; 

        Collider[] block = Physics.OverlapBox(gameObject.transform.position, Vector3.one * 0.5f);

        if (block.Where(Col => Col.gameObject.CompareTag("Bullet")).ToArray().Length != 0)
        {
            SetReward(-5f);
            EndEpisode();
        }

        else if (block.Where(Col => Col.gameObject.CompareTag("Player")).ToArray().Length != 0)
        {
            SetReward(10f);
            EndEpisode();
        }

        else if (block.Where(Col => Col.gameObject.CompareTag("Wall")).ToArray().Length != 0)
        {
            AddReward(-3f); 
        }

        float currentDist = Vector3.Distance(this.transform.position, player.transform.position); 

        if(prevDistance > currentDist)
        {
            AddReward(0.001f); 
        }

        else
        {
            AddReward(-0.001f); 
        }

 

        prevDistance = currentDist;
    }

    public override void OnEpisodeBegin()
    {
        area.ResetEnv();

        int rand = Random.Range(-5, 5); 

        transform.position = agentPos.transform.position;
        transform.position += new Vector3(rand, 0f, 0f); 

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        prevDistance = Vector3.Distance(this.transform.position, player.transform.position); 

    }

    public void SetAgentSpeed(float _speed)
    {
        agentSpeed = _speed; 
    }

    public void WaitTimeInference(int action)
    {
        if (Academy.Instance.IsCommunicatorOn)
        {
            RequestDecision(); 
        }
        else
        {
            if(m_currentTime >= DecisionWaitingTime)
            {
                m_currentTime = 0f;
                RequestDecision(); 
            }

            else
            {
                m_currentTime += Time.fixedDeltaTime; 
            }
        }
    }
}
