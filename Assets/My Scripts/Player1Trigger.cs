using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Trigger : MonoBehaviour
{

    public Collider Col;
    public float DamageAmt = 0.1f; 

   
    // Turn off box collider when hit and turn back on after 

    void Update()
    {
        if (NewBehaviourScript.Hits == false)
        {
            Col.enabled = true; 
        }

        else
        {
            Col.enabled = false; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player2"))
        {
            NewBehaviourScript.Hits = true;
            SaveScript.Player2Health -= DamageAmt;
            // Red bar does not go down until combo attacks stop 
            if (SaveScript.Player2Timer < 2.0f)
            {
                SaveScript.Player2Timer += 2.0f;
            }
        }
    }
}
