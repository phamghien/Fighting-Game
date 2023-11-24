using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Actions : MonoBehaviour
{
    public float JumpSpeed = 1.0f;
    public float FlipSpeed = 0.8f; 
    public GameObject Player1;
    private Animator Anim;
    private AnimatorStateInfo Player1Layer0;
    private bool HeavyMoving = false;
    public float PunchSlideAmt = 2f;
    private AudioSource MyPlayer;
    public AudioClip PunchWoosh;
    public AudioClip KickWoosh;
    public static bool HitsP2 = false; 


    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>(); 

    }

    // Update is called once per frame
    void Update()
    {

        //Heavy Punch Slide
        if(HeavyMoving == true)
        {
            if (Player2Move.FacingLeftP2 == true)
            {
                Player1.transform.Translate(-PunchSlideAmt * Time.deltaTime, 0, 0);
            }
            if (Player2Move.FacingRightP2 == true)
            {
                Player1.transform.Translate(PunchSlideAmt * Time.deltaTime, 0, 0);
            }
        }

        //Listen to the animator
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        //Standing attacks
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetButtonDown("Fire1P2"))
            {
                Anim.SetTrigger("LightPunch");
                HitsP2 = false; 
            }

            if (Input.GetButtonDown("Fire2P2"))
            {
                Anim.SetTrigger("HeavyPunch");
                HitsP2 = false;
            }

            if (Input.GetButtonDown("Fire3P2"))
            {
                Anim.SetTrigger("LightKick");
                HitsP2 = false;
            }

            if (Input.GetButtonDown("JumpP2"))
            {
                Anim.SetTrigger("HeavyKick");
                HitsP2 = false;
            }

            if (Input.GetButtonDown("BlockP2"))
            {
                Anim.SetTrigger("BlockOn");
            }

        }

        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("BlockP2"))
            {
                Anim.SetTrigger("BlockOff");
            }
        }

        //Crouching attack
        if (Player1Layer0.IsTag("Crouching"))
        {
            if (Input.GetButtonDown("Fire3P2"))
            {
                Anim.SetTrigger("LightKick");
                HitsP2 = false;
            }
        }


        //Ariel Moves
        if (Player1Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("JumpP2"))
            {
                Anim.SetTrigger("HeavyKick");
                HitsP2 = false;
            }
        }
    }

    public void JumpUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
    }

    public void HeavyMove()
    {
        StartCoroutine(PunchSlide());
    }

    public void FlipUp()
    {
        Player1.transform.Translate(0, FlipSpeed, 0);
       // Player1.transform.Translate(0.6f, 0, 0);
    }

    public void FlipBack()
    {
        Player1.transform.Translate(0, FlipSpeed, 0);
       // Player1.transform.Translate(-0.6f, 0, 0);
    }

    public void PunchWooshSound()
    {
        MyPlayer.clip = PunchWoosh;
        MyPlayer.Play(); 
    }

    public void KickWooshSound()
    {
        MyPlayer.clip = KickWoosh;
        MyPlayer.Play();
    }

    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(0.1f);
        HeavyMoving = false;
    }
}