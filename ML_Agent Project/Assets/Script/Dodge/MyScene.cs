using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class MyScene : MonoBehaviour
{

    public GameObject area = null;

    private int areaNum = 9;
    private int rows = 3;
    private int interval = 60;

    EnvironmentParameters m_ResetParameters = null; 

    void Start()
    {
        m_ResetParameters = Academy.Instance.EnvironmentParameters;
        areaNum = (int)m_ResetParameters.GetWithDefault("areaNum", areaNum);

        float x = 0;
        float z = 0;

        for (int i = 0; i < areaNum; i++)
        {
            GameObject areaPrefab = Instantiate(area, transform); 
            if(i % rows == 0 && i != 0)
            {
                x = 0;
                z += interval; 
            }

            areaPrefab.transform.position = new Vector3(x, 0, z);
            x += interval; 
        }
    }

}
