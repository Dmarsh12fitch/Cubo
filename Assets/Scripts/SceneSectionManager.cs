using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSectionManager : MonoBehaviour
{
    public GameObject[] sceneSections;
    private List<GameObject> instantiatedSceneSections = new List<GameObject>();

    //private Transform playerTransform;

    private int lastSpawnedZ = 1200;
    private int lastIndexUsed = 0;

    private bool firstTime = true;






    // Start is called before the first frame update
    void Start()
    {
        //playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        instantiatedSceneSections.Add(GameObject.Find("Segment BEGINNING"));
        instantiatedSceneSections.Add(GameObject.Find("SegmentF"));
        instantiatedSceneSections.Add(GameObject.Find("demo"));
        instantiatedSceneSections.Add(GameObject.Find("SegmentD"));
        //instantiatedSceneSections.Add(GameObject.Find("Segment SECOND"));     //this will be done but not yet
    }

    // Update is called once per frame
    void Update()
    {
        CheckToSpawnNextSection();


    }

    void CheckToSpawnNextSection()
    {
        if (transform.position.z > lastSpawnedZ - 1100)
        {
            SpawnNextSection();
            if (firstTime)
            {
                firstTime = false;
            } else
            {
                DeletePastSection();
            }
        }
    }

    void SpawnNextSection()
    {
        lastSpawnedZ += 400;
        int rand = 0;
        if (lastIndexUsed >= 6 && lastIndexUsed <= 8)
        {
            rand = Random.Range(6, 9);
            int rand2 = Random.Range(0, sceneSections.Length);
            int rand3 = Random.Range(0, 2);
            if(rand3 == 1)
            {
                rand = rand2;
            }
        } else
        {
            rand = Random.Range(0, sceneSections.Length);
        }
        var GO = Instantiate(sceneSections[rand]);
        lastIndexUsed = rand;
        GO.transform.Translate(0, 0, lastSpawnedZ);
        instantiatedSceneSections.Add(GO);
    }

    void DeletePastSection()
    {
        Destroy(instantiatedSceneSections[0]);
        instantiatedSceneSections.RemoveAt(0);
    }

}
