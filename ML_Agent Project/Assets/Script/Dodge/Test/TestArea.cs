using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Random = UnityEngine.Random;

public class TestArea : MonoBehaviour
{
    public GameObject agent;
    public GameObject bullet;
    public GameObject env;
    public GameObject player;

    public GameObject[] randPos;
    public GameObject[] shooterPos;  

    public List<GameObject> _testBullets = new List<GameObject>();

    public float bulletSpeed = 20f;
    public float agentSpeed = 3.0f;

    MyAgent agentScript = null;
    EnvironmentParameters m_ResetParameters = null;


    // Start is called before the first frame update
    void Start()
    {
        m_ResetParameters = Academy.Instance.EnvironmentParameters;
        agentScript = agent.GetComponent<MyAgent>();

        // InitBullet(); 
    }

    public void ResetEnv()
    {
        if (m_ResetParameters == null)
            m_ResetParameters = Academy.Instance.EnvironmentParameters;

        agentSpeed = m_ResetParameters.GetWithDefault("agentSpeed", agentSpeed);

        player.transform.position = randPos[Random.Range(0, randPos.Length)].transform.position; 
        // InitBullet(); 
        DestroyBullets();
    }

    void DestroyBullets()
    {
        for (int i = 0; i < _testBullets.Count; i++)
        {
            if (_testBullets[i] != null)
                Destroy(_testBullets[i]);
        }

        _testBullets.Clear();
    }
}
