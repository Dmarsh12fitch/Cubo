using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starPointCryberScr : MonoBehaviour
{

    public GameObject[] parts;

    public GameObject prefabExplosionEffect;
    public Material MyColorMat;

    private Vector3 rot1Speed;
    private Vector3 rot2Speed;
    private Vector3 rot3Speed;

    private float timer = 1;

    // Start is called before the first frame update
    void Start()
    {
        rot1Speed = randRotSpeed();
        rot2Speed = randRotSpeed();
        rot3Speed = randRotSpeed();
    }

    void Update()
    {
        parts[0].transform.Rotate(rot1Speed);
        parts[1].transform.Rotate(rot2Speed);
        parts[2].transform.Rotate(rot3Speed);

        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = 1.5f;
            rot1Speed = randRotSpeed();
            rot2Speed = randRotSpeed();
            rot3Speed = randRotSpeed();
        }
    }

    public void EXPLODE()
    {
        GameObject GO = Instantiate(prefabExplosionEffect, gameObject.transform.position, gameObject.transform.rotation);
        GO.GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material = MyColorMat;
        Destroy(gameObject);
    }



    Vector3 randRotSpeed()
    {
        return new Vector3(Random.Range(-7, 7), Random.Range(-7, 7), Random.Range(-7, 7));
    }

}
