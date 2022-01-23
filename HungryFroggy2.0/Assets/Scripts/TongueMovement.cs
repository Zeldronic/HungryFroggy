using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TongueMovement : MonoBehaviour
{

    private Vector2 target= new Vector2((float)0.25, (float)-1.7);
    private int animCheck = 0;//0-no anim, 1-1/4, 2-3/4, 3-max, 4-3/4, 5-1/4 
    private int frame = 0;
    private bool newtarget= false;
    private Vector3 currentEndpoint;

    public float timer = 10;
    public Text timerText;

    public int numEnemies = 7;
    public Text flyNumText;

    public Text winLoseText;

    //public bool gameStart = false;

    public GameObject introBackground;
    public Text introText;
    public float introTimer= 3;
    bool theEnd = false;
    public float endTimer = 3;

    public AudioSource musicSource;
    public AudioSource enemySource;
    public AudioClip introClip;
    public AudioClip gameClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip eatClip;
    bool firstIntroPlay = true;
    bool firstgamePlay = true;
    bool firstendgamePLay = true;
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (firstIntroPlay == true)
        {
            musicSource.clip = introClip;
            musicSource.Play();
            firstIntroPlay = false;
        }

        introTimer -= Time.deltaTime;

        //if end is triggered last frame start coutdown
        if (theEnd == true)
        {
            endTimer -= Time.deltaTime;
        }

        if(endTimer < 0)
        {
            Application.LoadLevel(0);
        }


        //Wait for 2 seconds to start the game
        if (introTimer < 0)
        {
           
           
            introBackground.SetActive(false);
            introText.text = "";

            if (firstgamePlay == true)
            {
                musicSource.clip = gameClip;
                musicSource.Play();
                firstgamePlay = false;
            }


            if (timer > 0)
            {
               
                timer -= Time.deltaTime;
                float seconds = Mathf.FloorToInt(timer + 0.5f);
                timerText.text = seconds.ToString();

                if (Input.GetKey(KeyCode.Space))
                {
                    // Debug.Log("Space Pressed");

                    if (animCheck == 0)
                    {
                        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        //Debug.Log("x="+ Input.mousePosition.x + " and target x="+ target.x);
                        // Debug.Log("y="+ Input.mousePosition.y + " and target y=" + target.y);
                        newtarget = true;
                    }
                }


                frame++;
                if (frame == 2)
                {
                    AnimateCheck(target);
                    frame = 0;
                }

                // Debug.Log("Time remaining" + timer.ToString());
            }
            else
            {
                timer = 0;
                if (numEnemies > 0)
                {
                    winLoseText.text = "GAME OVER \nGame by: Graci Baker";
                    theEnd = true;
                    if (firstendgamePLay == true)
                    {
                        musicSource.clip = loseClip;
                        musicSource.Play();
                        firstendgamePLay = false;
                    }
                }
                //Debug.Log("Times UP!");
            }


        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("Trigger Activated");
        if (other.gameObject.CompareTag("Enemy"))
        {
            numEnemies--;
            flyNumText.text = numEnemies.ToString();
           // enemySource = other.gameObject.GetComponent<AudioSource>();
            enemySource.clip = eatClip;
            enemySource.Play();
            other.gameObject.SetActive(false);

            if(numEnemies == 0)
            {
                winLoseText.text = "YOU WIN! \nGame by: Graci Baker";
                theEnd = true;

                if (firstendgamePLay == true)
                {
                    musicSource.clip = winClip;
                    musicSource.Play();
                    firstendgamePLay = false;
                }
            }
        }
    }

    //to update what stage of the animation the line render is on and Update     //NOTE: start of line renderer (0.25, -1.7)
    void AnimateCheck(Vector2 target)
    {
       if (newtarget == false)
            return;

        LineRenderer linerenderer = gameObject.GetComponent<LineRenderer>();
        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
        
        switch (animCheck)
        {
            case 0:     // 1/4
                Vector3 endpoint0 = new Vector3(((float)((3 * 0.25) / 4) + (target.x / 4)), (float)(((3 * -1.7) / 4) + (target.y / 4)), 0);
                linerenderer.SetPosition(1, endpoint0);
                collider.offset = endpoint0;
               // currentEndpoint = endpoint0;
               // Debug.Log("Endpoint for 1/4 = (" + currentEndpoint.x + ", " + currentEndpoint.y + ")" );
                animCheck=1;
                break;
            case 1:     // 3/4
                Vector3 endpoint1 = new Vector3((float)((0.25 / 4) + ((3 * target.x) / 4)), (float)((-1.7 / 4) + ((3 * target.y) / 4)), 0);
                linerenderer.SetPosition(1, endpoint1);
                collider.offset = endpoint1;
               // currentEndpoint = endpoint1;
               // Debug.Log("Endpoint for 3/4 = (" + currentEndpoint.x + ", " + currentEndpoint.y + ")");
                animCheck =2;
                break;
            case 2:    // max
                Vector3 endpoint2 = new Vector3((float)target.x, (float)target.y, 0);
                linerenderer.SetPosition(1, endpoint2);
                currentEndpoint = endpoint2;
                collider.offset = endpoint2;
               // Debug.Log("Endpoint for max = (" + currentEndpoint.x + ", " + currentEndpoint.y + ")");
                animCheck =3;
                break;
            case 3:    // 3/4
                Vector3 endpoint3 = new Vector3((float)((0.25 / 4) + ((3 * target.x) / 4)), (float)((-1.7 / 4) + ((3 * target.y) / 4)), 0);
                linerenderer.SetPosition(1, endpoint3);
                collider.offset = endpoint3;
                animCheck =4;
                break;
            case 4:    // 1/4
                Vector3 endpoint4 = new Vector3(((float)((3 * 0.25) / 4) + (target.x / 4)), (float)(((3 * -1.7) / 4) + (target.y / 4)), 0);
                linerenderer.SetPosition(1, endpoint4);
                collider.offset = endpoint4;
                animCheck =5;
                break;
            case 5:    // start
                Vector3 endpoint5 = new Vector3( (float) 0.25, (float)-1.7, 0);
                linerenderer.SetPosition(1, endpoint5);
                collider.offset = endpoint5;
                animCheck =0;
                newtarget = false;
                break;
        }

    }
}
