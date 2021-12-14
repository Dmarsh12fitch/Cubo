using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starPointCryberScr : MonoBehaviour
{

    public GameObject[] parts;

    private Quaternion part1Old;
    private Quaternion part1New;

    private float speed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        part1Old = transform.rotation;
        part1New = randRot();
    }

    // Update is called once per frame
    void Update()
    {
        rotatePart1();
    }

    void rotatePart1()
    {
        parts[0].transform.rotation = Quaternion.Lerp(part1Old, part1New, speed);
    }

    Quaternion randRot()
    {
        return new Quaternion(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
    }

}
