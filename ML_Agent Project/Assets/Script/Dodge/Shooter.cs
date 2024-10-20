using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public TestArea area; 
    public GameObject bullet;

    WaitForSeconds _wait = new WaitForSeconds(0.5f);

    bool ableToShoot; 

    void Start()
    {
        ableToShoot = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToShoot)
        {
            StartCoroutine(coFire()); 

            GameObject _bullet = Instantiate(bullet);
            _bullet.transform.position = this.transform.position;
            area.GetComponent<TestArea>()._testBullets.Add(_bullet); 
        }
    }

    IEnumerator coFire()
    {
        if (!ableToShoot)
            yield return null;

        ableToShoot = false; 

        yield return _wait;

        ableToShoot = true; 
    }
}
