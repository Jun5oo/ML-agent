using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Random = UnityEngine.Random;

public class Area : MonoBehaviour
{

    public GameObject agent;
    public GameObject bullet;  
    public GameObject env;
    public GameObject[] walls;
    public GameObject player;

    List<GameObject> bullets = new List<GameObject>();
    List<Bullet> bulletScripts = new List<Bullet>();

    public float board_halfWidth = 20f;
    public float bulletSpeed = 20f;
    public int bulletNum = 15;
    public float bulletRandom = 0.2f;
    public float agentSpeed = 3.0f;

    public Shooter[] shooters; 

    MyAgent agentScript = null;
    EnvironmentParameters m_ResetParameters = null; 


    // Start is called before the first frame update
    void Start()
    {
        m_ResetParameters = Academy.Instance.EnvironmentParameters;
        // agentScript = agent.GetComponent<MyAgent>();

        // InitBullet(); 
    }

    private void InitBullet()
    {
        bulletScripts.Clear(); 
        
        for(int i=0; i<bullets.Count; i++)
        {
            Destroy(bullets[i]); 
        }

        bullets.Clear();

        for(int j=0; j<bulletNum; j++)
        {
            GameObject _bullet = Instantiate(bullet, transform); 
            _bullet.transform.position = this.transform.position; 

            Bullet _bulletScript = _bullet.GetComponent<Bullet>();
            _bulletScript.SetBullet(agent, bulletSpeed, bulletRandom, board_halfWidth);

            bullets.Add(_bullet);
            bulletScripts.Add(_bulletScript); 
        }
    }

    public void ResetEnv()
    {
        if (m_ResetParameters == null)
            m_ResetParameters = Academy.Instance.EnvironmentParameters;

        /*
        board_halfWidth = m_ResetParameters.GetWithDefault("boardHalfWidth", board_halfWidth);
        bulletSpeed = m_ResetParameters.GetWithDefault("bulletSpeed", bulletSpeed);
        bulletNum = (int)m_ResetParameters.GetWithDefault("bulletNum", bulletNum);
        bulletRandom = m_ResetParameters.GetWithDefault("bulletRandom", bulletRandom);
        agentSpeed = m_ResetParameters.GetWithDefault("agentSpeed", agentSpeed);
        
        if (agentScript == null)
            agentScript = agent.GetComponent<MyAgent>();

        agentScript.SetAgentSpeed(agentSpeed);

        for(int i=0; i<walls.Length; i++)
            walls[i].transform.localScale = new Vector3(40f, 10f, 1f);

        InitBullet(); 

        float theta = Random.Range(0, 2 * Mathf.PI);
        player.transform.localPosition = new Vector3((board_halfWidth -2) * Mathf.Cos(theta), 0.5f, (board_halfWidth -2) * Mathf.Sin(theta));

        */

        foreach(var shooter in shooters)
        {
            shooter.Clear(); 
        }
    }

}
