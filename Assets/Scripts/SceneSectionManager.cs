using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSectionManager : MonoBehaviour
{
    public GameObject[] sceneSections;
    private List<GameObject> instantiatedSceneSections = new List<GameObject>();

    //private Transform playerTransform;

    private int lastSpawnedZ = 400;









    // Start is called before the first frame update
    void Start()
    {
        //playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        instantiatedSceneSections.Add(GameObject.Find("SceneSegmentTypeA"));
        instantiatedSceneSections.Add(GameObject.Find("SceneSegmentTypeA (1)"));
    }

    // Update is called once per frame
    void Update()
    {
        CheckToSpawnNextSection();


    }

    void CheckToSpawnNextSection()
    {
        if (transform.position.z > lastSpawnedZ - 300)
        {
            lastSpawnedZ += 400;
            var GO = Instantiate(sceneSections[0]);
            GO.transform.Translate(0, 0, lastSpawnedZ);
            instantiatedSceneSections.Add(GO);
            DeletePastSection();
        }
    }

    void DeletePastSection()
    {
        /*          //this despawning doesn't work
        foreach(GameObject section in instantiatedSceneSections)
        {
            if (transform.position.z > section.transform.position.z + 400)
            {
                Destroy(section);
                instantiatedSceneSections.Remove(section);
            }
        }
        */
    }

}
