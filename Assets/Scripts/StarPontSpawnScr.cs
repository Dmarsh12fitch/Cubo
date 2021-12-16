using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPontSpawnScr : MonoBehaviour
{
    public GameObject[] starPointArray;

    private int starPointsSpawned = 0;











    // Start is called before the first frame update
    void Start()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                int rand = (int)Random.Range(0, starPointArray.Length);    //TWO more than the number of blocks
                if (rand != starPointArray.Length - 1 && starPointsSpawned < 2)                               //ONE less than above
                {
                    int rand2 = Random.Range(0, 10);
                    if(rand2 == 0)
                    {
                        var GO = Instantiate(starPointArray[rand], gameObject.transform);
                        GO.transform.transform.Translate(i, j, 0);
                        starPointsSpawned++;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
