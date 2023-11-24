using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Anim;
    public float WalkSpeed = 0.001f;
    private bool isJumping = false;
    private AnimatorStateInfo Player1Layer0; //this will allow us to listen to the animation playing and see if it has a tag
    private bool CanWalkLeft = true;
    private bool CanWalkRight = true;
    public GameObject Player1;
    public GameObject opponent;
    private Vector3 OppPosition;
    public static bool FacingLeft = false;
    public static bool FacingRight = true;
    public static bool WalkLeftP1 = true;
    public static bool WalkRightP1 = true; 
    public AudioClip LightPunch;
    public AudioClip HeavyPunch;
    public AudioClip LightKick;
    public AudioClip HeavyKick;
    private AudioSource MyPlayer;
    public GameObject Restrict; 

    //Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        StartCoroutine(FaceRight());
        MyPlayer = GetComponentInChildren<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Check if K.O
        if (SaveScript.Player1Health <= 0)
        {
            Anim.SetTrigger("KnockOut");
            Player1.GetComponent<NewBehaviourScript>().enabled = false;
            StartCoroutine(KnockedOut());
        }

        //Listen to the animator
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0); //this will allow us to see which current animation layer is playing and use tags set in animator motions - 0 is base layer

        //Setting the bounds of the player so they cannot exit screen
        Vector3 screenBounds = Camera.main.WorldToScreenPoint(this.transform.position); //this will set where the player is on 2D coordinate on the screen - the character - camera needs to be tagged MainCamera

        if (screenBounds.x > Screen.width - 200) //if the player position reaches the furthest point on the screen to the right then the boolean is set to false. Meaning the player cannot walk anyfurther off the screen
        {
            CanWalkRight = false;
        }
        else
        {
            CanWalkRight = true;
        }

        if (screenBounds.x < 200) //if the player position reaches the furthest point on the screen to the left (which is 0) then the boolean is set to false. Meaning the player cannot walk anyfurther off the screen
        {
            CanWalkLeft = false;
        }

        else
        {
            CanWalkLeft = true;
        }

        //Get the opponents position
        OppPosition = opponent.transform.position;

        //Facing left or right of the opponent

        if (OppPosition.x > Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());
        }

        if (OppPosition.x < Player1.transform.position.x)

        {
            StartCoroutine(FaceRight());
        }


        // Walking left and right
        //constrain only to certain animations and to the bounds of the screen
        if (Player1Layer0.IsTag("Motion")) //this checks if the player is tagged with motion then you can do the other motions eg. listens to animations is on idle which is motion and then can do walk forward and backward which is motion too
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (CanWalkRight == true)
                {
                    if (WalkRightP1 == true)
                    {
                        Anim.SetBool("Forward", true);
                        transform.Translate(WalkSpeed, 0, 0);
                    }
                }
            }


            if (Input.GetAxis("Horizontal") < 0)
            {
                if (CanWalkLeft == true) 
                {
                      if (WalkLeftP1 == true)
                        {
                            Anim.SetBool("Backward", true);
                            transform.Translate(-WalkSpeed, 0, 0);
                        }
                }
            }
        
        

            if (Input.GetAxis("Horizontal") == 0)
            {
                Anim.SetBool("Forward", false);
                Anim.SetBool("Backward", false);
            }
        }

        //Jumping and crouching
        if (Input.GetAxis("Vertical") > 0)
        {
            if (isJumping == false)
            {
                isJumping = true;
                Anim.SetTrigger("Jump");
                StartCoroutine(JumpPause());
            }
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            Anim.SetBool("Crouch", true);
        }


        if (Input.GetAxis("Vertical") == 0)
        {
            Anim.SetBool("Crouch", false);
        }

        // Reset the restrict
        if (Restrict.gameObject.activeInHierarchy == false)
        {
            WalkLeftP1 = true;
            WalkRightP1 = true; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FistLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightPunch;
            MyPlayer.Play(); 
        }
        if (other.gameObject.CompareTag("FistHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip = HeavyPunch;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("KickHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip = HeavyKick;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("KickLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightKick;
            MyPlayer.Play();
        }
    }


    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        isJumping = false;
    }

    IEnumerator FaceLeft()
    {
        if (FacingLeft == true)
        {
            FacingLeft = false;
            FacingRight = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
            Anim.SetLayerWeight(1, 0); //sets the layer in the animation window and the weight
        }
    }

    IEnumerator FaceRight()
    {
        if (FacingRight == true)
        {
            FacingRight = false;
            FacingLeft = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, -180, 0);
            Anim.SetLayerWeight(1, 1); //sets the layer in the animation window
        }
    }
    IEnumerator KnockedOut()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Player1Move>().enabled = false;
    }
}