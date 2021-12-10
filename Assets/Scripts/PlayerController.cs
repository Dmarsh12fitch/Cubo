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

    

    private float moveDirection;
    private float moveToX;

    private bool boostIsCharged;
    private float boostDuration = 0.4f;
    private float boostSpeed = 120f;

    private float defaultSpeed = 30f;
    private float jumpMod = 7.5f;

    public Material[] prefabsMaterials;
    public string[] prefabMaterialsNAMES;
    private int currentMatIndex;

    private float timer;
    private bool Dead;

    private Vector3 ThisFrameVelocity;

    public enum PlayerState
    {
        Jump,
        Boost,
        Immune,
        Default
    }
    private PlayerState currentPlayerState = PlayerState.Default;
    public PlayerState requestedPlayerState = PlayerState.Default;

    public enum PlayerLane
    {
        Left,
        Center,
        Right
    }
    private PlayerLane currentLane = PlayerLane.Center;
    public PlayerLane requestedMoveDirection = PlayerLane.Center;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        rend = GameObject.Find("Player").GetComponent<MeshRenderer>();
        //cinemachineCameraOffset = GameObject.Find("Main Camera").GetComponent<CinemachineCameraOffset>();
        //camTransform = GameObject.Find("Reg CM Virtual Camera").GetComponent<Transform>();

        randomColorSwap();
    }

    private void Update()
    {
        
        ThisFrameVelocity = new Vector3(0, 0, 0);

        switch (requestedMoveDirection)
        {
            case PlayerLane.Center:
                break;
            case PlayerLane.Left:
                if(currentLane == PlayerLane.Left)
                {
                    break;
                } else if (currentLane == PlayerLane.Center)
                {
                    currentLane = PlayerLane.Left;
                    MoveLeft();
                } else if(currentLane == PlayerLane.Right)
                {
                    currentLane = PlayerLane.Center;
                    MoveLeft();
                }
                break;
            case PlayerLane.Right:
                if(currentLane != PlayerLane.Right)
                {
                    break;
                } else if(currentLane != PlayerLane.Center)
                {
                    currentLane = PlayerLane.Right;
                    MoveRight();
                } else if(currentLane != PlayerLane.Left)
                {
                    currentLane = PlayerLane.Center;
                    MoveRight();
                }
                break;
        }
        requestedMoveDirection = PlayerLane.Center;

        Debug.Log("State : " + currentPlayerState.ToString());

        switch (requestedPlayerState)
        {
            case PlayerState.Default:
                if(currentPlayerState == PlayerState.Default)
                {
                    Default();
                }
                break;
            case PlayerState.Jump:
                if(currentPlayerState == PlayerState.Default)
                {
                    Jump();
                }
                break;
            case PlayerState.Boost:
                if (currentPlayerState == PlayerState.Jump && boostIsCharged)
                {
                    Boost();
                }
                break;
            case PlayerState.Immune:
                Default();
                break;
        }

        requestedPlayerState = PlayerState.Default;
        Movement();
    }


    //modify ThisFrameVelocity x
    void MoveRight()
    {


    }


    //modify ThisFrameVelocity x
    void MoveLeft()
    {


    }




    //modify ThisFrameVelocity z
    void Default()
    {
        ThisFrameVelocity = new Vector3(ThisFrameVelocity.x, ThisFrameVelocity.y, defaultSpeed);
    }

    //modify ThisFrameVelocity z + y
    void Boost()
    {
        boostIsCharged = false;
        currentPlayerState = PlayerState.Boost;
        ThisFrameVelocity = new Vector3(ThisFrameVelocity.x, 0, boostSpeed);
        StartCoroutine(BoostCountdown());
    }

    //modify ThisFrameVelocity y
    void Jump()
    {
        currentPlayerState = PlayerState.Jump;
        rb.AddForce(new Vector3(0, jumpMod, 0), ForceMode.Impulse);
        Debug.Log("GotHERE");
        Default();
    }

    IEnumerator BoostCountdown()
    { 
        yield return new WaitForSeconds(boostDuration);
        currentPlayerState = PlayerState.Immune;
    }

    void Movement()
    {
        rb.velocity = ThisFrameVelocity;
    }




    private void OnCollisionEnter(Collision collision)
    {
        //reset if on ground
        if (collision.gameObject.CompareTag("Ground") && rb.velocity.y < 0)
        {
            currentPlayerState = PlayerState.Default;
        }

        if (currentPlayerState == PlayerState.Boost || currentPlayerState == PlayerState.Immune)
        {
            if (collision.gameObject.CompareTag("BLACK") || collision.gameObject.CompareTag("RED")
                || collision.gameObject.CompareTag("YELLOW") || collision.gameObject.CompareTag("BLUE")
                || collision.gameObject.CompareTag("GREEN") || collision.gameObject.CompareTag("WHITE")
                || collision.gameObject.CompareTag("PURPLE"))
            {
                collision.gameObject.GetComponent<CyberBlockScr>().EXPLODE();   //temp for this. it should be all of them
            }
        }
        else
        {
            if (collision.gameObject.CompareTag(currentMaterialString))
            {
                randomColorSwap();
                collision.gameObject.GetComponent<CyberBlockScr>().EXPLODE();
            }
            else if (collision.gameObject.CompareTag("BLACK") || collision.gameObject.CompareTag("RED")
              || collision.gameObject.CompareTag("YELLOW") || collision.gameObject.CompareTag("BLUE")
              || collision.gameObject.CompareTag("GREEN") || collision.gameObject.CompareTag("PURPLE")
              || collision.gameObject.CompareTag("WHITE"))
            {
                Debug.Log("you lose!");
                Debug.Log(" collided with : " + collision.gameObject.name);
                killPlayer();
            }
        }
    }




    /*
    // Update is called once per frame
    void Update()
    {

        if (!Dead)
        {
            Debug.Log("BEFORE : " + rb.velocity.z);
            //only when not boosting
            if (rb.velocity.z < speed + 1 && !inFloat)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
                Debug.Log("AFTER : " + rb.velocity.z);
            }

            //only during boosting
            if (inBoost)
            {
                if (inFloat)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, boostSpeed);
                } else
                {
                    inFloat = false;
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
                }
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0.05f;
                    randomColorSwap();
                }
            }

            if (inMove)
            {
                if (moveDirection == -1)
                {
                    //moving left
                    if (transform.position.x > moveToX + 0.05f)
                    {
                        rb.velocity = new Vector3(moveDirection * speed / 5, rb.velocity.y, rb.velocity.z);
                    }
                    else
                    {
                        inMove = false;
                        rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                        transform.position = new Vector3(moveToX, transform.position.y, transform.position.z);
                        return;
                    }
                }
                else if (moveDirection == 1)
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
                        transform.position = new Vector3(moveToX, transform.position.y, transform.position.z);
                        return;
                    }
                }

            }
        }
    }





    public void tryJump()
    {
        if (!inAir && !Dead)
        {
            inAir = true;
            rb.AddForce(new Vector3(0, jumpMod, 0), ForceMode.Impulse);
        }
    }

    public void tryBoost()
    {
        if(inAir && !inBoost && !Dead)
        {
            StartCoroutine(boostTime());
            rb.velocity = new Vector3(0, 0, boostSpeed);
        }
    }

    public void tryMove(int moveThisAmount)
    {
        if (!Dead)
        {
            //if in right or left lanes
            if ((transform.position.x >= 0.9f && moveThisAmount == -1) || (transform.position.x <= -0.9f && moveThisAmount == 1))
            {
                moveToX = 0;

                //if in center
            }
            else if (transform.position.x <= 0.1f && transform.position.x >= -0.1f)
            {
                if (moveThisAmount == -1)
                {
                    moveToX = -1;
                }
                else if (moveThisAmount == 1)
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
    }



    IEnumerator boostTime()
    {
        inBoost = true;
        inFloat = true;
        yield return new WaitForSeconds(boostDuration);
        inFloat = false;
        //stay invincible for a bit after
    }
    */


    void randomColorSwap()
    {
        int randMatIndex = Random.Range(0, prefabsMaterials.Length);
        while (randMatIndex == currentMatIndex)
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
        Dead = true;
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<MeshRenderer>());
    }


}
