using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Singleton = null;
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else if (Singleton != this)
        {
            Destroy(this.gameObject);
        }
    }

    private PlayerController PlayerControllerScript;

    private Vector2 startTouchPos;
    private Vector2 currentTouchPos;
    private Vector2 endTouchPos;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;




    private void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        Swipe();
    }


    public void Swipe()
    {
        //begin touch
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

        //during touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPos = Input.GetTouch(0).position;
            Vector2 Distance = currentTouchPos - startTouchPos;

            if (!stopTouch)
            {
                if(Distance.x < -swipeRange)
                {
                    //left
                    PlayerControllerScript.tryMove(-1);
                    stopTouch = true;
                } else if(Distance.x > swipeRange)
                {
                    //right
                    PlayerControllerScript.tryMove(1);
                    stopTouch = true;
                } else if(Distance.y > swipeRange)
                {
                    //up
                    PlayerControllerScript.tryJump();
                    stopTouch = true;
                } else if(Distance.y < -swipeRange)
                {
                    //down
                    stopTouch = true;
                }
            }
        }

        //end touch
        if(Input.touchCount> 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPos = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPos - startTouchPos;

            if(Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                //you just tapped
                PlayerControllerScript.tryBoost();
            }
        }
    }



}
