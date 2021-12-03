using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private PlayerController PlayerControllerScript;

    public GameObject[] prefabBlockArray;

    private GameObject[] instantiatedBlocksArray = new GameObject[6];
    private int blocksSpawned = 0;
    private bool exitIsThere;




    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        for(int i = -1; i < 2; i++)
        {
            for(int j = 1; j < 3; j++)
            {
                int rand = (int) Random.Range(0, prefabBlockArray.Length);    //TWO more than the number of blocks
                if(rand != prefabBlockArray.Length - 1)                               //ONE less than above
                {
                    var GO = Instantiate(prefabBlockArray[rand], gameObject.transform);
                    GO.transform.transform.Translate(i, j, 0);
                    instantiatedBlocksArray[blocksSpawned] = GO;
                    blocksSpawned++;
                }
            }
        }
        if(blocksSpawned == 6)
        {
            int rand2 = (int) Random.Range(0, prefabBlockArray.Length - 1);
            /*
            int rand3 = (int) Random.Range(0, 2);
            if(rand3 == 0)
            {
                if (PlayerControllerScript.currentMaterialString.Equals("BLUE"))
                {
                    var GO = Instantiate(prefabBlockArray[1], gameObject.transform);
                    GO.transform.transform.Translate(instantiatedBlocksArray[rand2].gameObject.transform.position.x,
                        instantiatedBlocksArray[rand2].gameObject.transform.position.y + 0.5f, 0);
                } else if (PlayerControllerScript.currentMaterialString.Equals("GREEN"))
                {
                    var GO = Instantiate(prefabBlockArray[2], gameObject.transform);
                    GO.transform.transform.Translate(instantiatedBlocksArray[rand2].gameObject.transform.position.x,
                        instantiatedBlocksArray[rand2].gameObject.transform.position.y + 0.5f, 0);
                } else if (PlayerControllerScript.currentMaterialString.Equals("PURPLE"))
                {
                    var GO = Instantiate(prefabBlockArray[3], gameObject.transform);
                    GO.transform.transform.Translate(instantiatedBlocksArray[rand2].gameObject.transform.position.x,
                        instantiatedBlocksArray[rand2].gameObject.transform.position.y + 0.5f, 0);
                } else if (PlayerControllerScript.currentMaterialString.Equals("RED"))
                {
                    var GO = Instantiate(prefabBlockArray[4], gameObject.transform);
                    GO.transform.transform.Translate(instantiatedBlocksArray[rand2].gameObject.transform.position.x,
                        instantiatedBlocksArray[rand2].gameObject.transform.position.y + 0.5f, 0);
                } else if (PlayerControllerScript.currentMaterialString.Equals("WHITE"))
                {
                    var GO = Instantiate(prefabBlockArray[5], gameObject.transform);
                    GO.transform.transform.Translate(instantiatedBlocksArray[rand2].gameObject.transform.position.x,
                        instantiatedBlocksArray[rand2].gameObject.transform.position.y + 0.5f, 0);
                } else if (PlayerControllerScript.currentMaterialString.Equals("YELLOW"))
                {
                    var GO = Instantiate(prefabBlockArray[6], gameObject.transform);
                    GO.transform.transform.Translate(instantiatedBlocksArray[rand2].gameObject.transform.position.x,
                        instantiatedBlocksArray[rand2].gameObject.transform.position.y + 0.5f, 0);
                }
            }
            */
            Destroy(instantiatedBlocksArray[rand2]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
