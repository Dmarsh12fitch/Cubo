using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberBlockScr : MonoBehaviour
{
    public GameObject prefabExplosionEffect;
    public Material MyColorMat;

    public void EXPLODE()
    {
        GameObject GO = Instantiate(prefabExplosionEffect, gameObject.transform.position, gameObject.transform.rotation);
        GO.GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material = MyColorMat;
        Destroy(gameObject);
    }


}
