using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer rend;
    public string currentMaterialString;


    public GameObject prefabExplosion;


    //private Transform camTransform;
    //private CinemachineCameraOffset cinemachineCameraOffset;

    private bool inAir;
    private bool inBoost;
    private bool inFloat;
    private bool inMove;

    private float moveDirection;
    private float moveToX;

    private float boostDuration = 0.4f;
    private float boostSpeed = 90f;
    private float speed = 30f;
    private float jumpMod = 7.5f;

    public Material[] prefabsMaterials;
    public string[] prefabMaterialsNAMES;
    private int currentMatIndex;

    private float timer;



    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        rend = GameObject.Find("Player").GetComponent<MeshRenderer>();
        //cinemachineCameraOffset = GameObject.Find("Main Camera").GetComponent<CinemachineCameraOffset>();
        //camTransform = GameObject.Find("Reg CM Virtual Camera").GetComponent<Transform>();

        randomColorSwap();
    }

    // Update is called once per frame
    void Update()
    {
        //only when not boosting
        if (rb.velocity.z < speed + 1 && !inBoost)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
        }

        //only during boosting
        if (inBoost)
        {
            if (inFloat)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 0.05f;
                randomColorSwap();
            }
        }

        if (inMove)
        {
            if(moveDirection == -1)
            {
                //moving left
                if(transform.position.x > moveToX + 0.05f)
                {
                    rb.velocity = new Vector3(moveDirection * speed / 5, rb.velocity.y, rb.velocity.z);
                } else
                {
                    inMove = false;
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                    transform.position = new Vector3(moveToX, transform.position.y, transform.position.z + Time.deltaTime);
                    return;
                }
            } else if(moveDirection == 1)
            {
                //moving right
                if (transform.position.x < moveToX - 0.05f)
                {
                    rb.velocity = new Vector3(moveDirection * speed / 5, rb.velocity.y, rb.velocity.z);
                }
                else
                {
                    inMove = false;
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                    transform.position = new Vector3(moveToX, transform.position.y, transform.position.z + Time.deltaTime);
                    return;
                }
            }

        }

    }





    public void tryJump()
    {
        if (!inAir)
        {
            inAir = true;
            rb.AddForce(new Vector3(0, jumpMod, 0), ForceMode.Impulse);
        }
    }

    public void tryBoost()
    {
        if(inAir && !inBoost)
        {
            StartCoroutine(boostTime());
            rb.velocity = new Vector3(0, 0, boostSpeed);
        }
    }

    public void tryMove(int moveThisAmount)
    {
        //if in right or left lanes
        if((transform.position.x >= 0.9f && moveThisAmount == -1) || (transform.position.x <= -0.9f && moveThisAmount == 1))
        {
            moveToX = 0;

            //if in center
        }
        else if(transform.position.x <= 0.1f && transform.position.x >= -0.1f)
        {
            if(moveThisAmount == -1)
            {
                moveToX = -1;
            } else if(moveThisAmount == 1)
            {
                moveToX = 1;
            }
        }

        //set direction
        if (transform.position.x > moveToX)
        {
            moveDirection = -1;
        }
        else if (transform.position.x < moveToX)
        {
            moveDirection = 1;
        }
        inMove = true;
    }



    IEnumerator boostTime()
    {
        inBoost = true;
        inFloat = true;
        yield return new WaitForSeconds(boostDuration);
        inFloat = false;
        //stay invincible for a bit after
    }

    private void OnCollisionEnter(Collision collision)
    {
        //reset if on ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            inAir = false;
            if (inBoost)
            {
                 
                //cinemachineCameraOffset.ForceCameraPosition(new Vector3(0, 5.33333f, -3.33333f), new Quaternion(0, 0, 0, 0));
                //camTransform.position = new Vector3(camTransform.position.x, 5.333333f, -3.33333f);
                inBoost = false;
            }
        }

        if (inBoost)
        {
            if(collision.gameObject.CompareTag("BLACK") || collision.gameObject.CompareTag("RED")
                || collision.gameObject.CompareTag("YELLOW") || collision.gameObject.CompareTag("BLUE")
                || collision.gameObject.CompareTag("GREEN") || collision.gameObject.CompareTag("WHITE")
                || collision.gameObject.CompareTag("PURPLE"))
            {
                collision.gameObject.GetComponent<CyberBlockScr>().EXPLODE();   //temp for this. it should be all of them
                //explode everything!
                //rb.AddForce(new Vector3(0, 0, boostSpeed / 2), ForceMode.Impulse);
                rb.velocity = new Vector3(0, 0, boostSpeed);
            }
        } else
        {
            if (collision.gameObject.CompareTag(currentMaterialString))
            {
                randomColorSwap();
                collision.gameObject.GetComponent<CyberBlockScr>().EXPLODE();
                //rb.AddForce(new Vector3(0, 0, speed / 2), ForceMode.Impulse);
            } else if(collision.gameObject.CompareTag("BLACK") || collision.gameObject.CompareTag("RED")
                || collision.gameObject.CompareTag("YELLOW") || collision.gameObject.CompareTag("BLUE")
                || collision.gameObject.CompareTag("GREEN") || collision.gameObject.CompareTag("PURPLE")
                || collision.gameObject.CompareTag("WHITE"))
            {
                //explode you!
                Debug.Log("you lose!");
                killPlayer();
            }
        }
    }

    void randomColorSwap()
    {
        
        int randMatIndex = Random.Range(0, prefabsMaterials.Length);
        while(randMatIndex == currentMatIndex)
        {
            randMatIndex = Random.Range(0, prefabsMaterials.Length);
        }
        currentMatIndex = randMatIndex;
        rend.material = prefabsMaterials[currentMatIndex];
        currentMaterialString = prefabMaterialsNAMES[currentMatIndex];
    }


    void killPlayer()
    {
        GameObject GO = Instantiate(prefabExplosion, gameObject.transform.position, gameObject.transform.rotation);
        GO.GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material = prefabsMaterials[currentMatIndex];
        GameObject GO2 = Instantiate(prefabExplosion, gameObject.transform.position, gameObject.transform.rotation);
        GO2.GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material = prefabsMaterials[currentMatIndex];
        Destroy(GetComponent<PlayerController>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<MeshRenderer>());
    }


}
