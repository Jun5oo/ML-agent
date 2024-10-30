using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] shootDirections; 
    public GameObject bullet;

    List<GameObject> bullets = new List<GameObject>();

    WaitForSeconds _wait = new WaitForSeconds(0.5f);

    bool ableToShoot; 

    void Start()
    {
        ableToShoot = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if(ableToShoot)
            StartCoroutine(coFire()); 
    }

    IEnumerator coFire()
    {
        // int rand = Random.Range(0, 3);
        // int rand1 = Random.Range(3, 5); 
        int rand = Random.Range(0, 5);
        
        GameObject _bullet1 = Instantiate(bullet); 
        bullets.Add(_bullet1);
        _bullet1.transform.position = shootDirections[rand].transform.position;
        _bullet1.transform.rotation = shootDirections[rand].transform.rotation;

        /*
        GameObject _bullet2 = Instantiate(bullet);
        _bullet2.transform.position = shootDirections[rand2].transform.position;
        _bullet2.transform.rotation = shootDirections[rand2].transform.rotation;
        bullets.Add(_bullet2);
        */ 


        ableToShoot = false;

        yield return _wait;

        ableToShoot = true;
    }

    public void Clear()
    {
        foreach(var _bullet in bullets)
        {
            Destroy(_bullet); 
        }

        bullets.Clear(); 
    }
}
