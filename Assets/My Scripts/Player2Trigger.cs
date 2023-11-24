using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Trigger : MonoBehaviour
{

    public Collider Col;
    public float DamageAmt = 0.1f; 

   
    // Turn off box collider when hit and turn back on after 

    void Update()
    {
        if (Player2Actions.HitsP2 == false)
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
        if(other.gameObject.CompareTag("Player1"))
        {
            Player2Actions.HitsP2 = true;
            SaveScript.Player1Health -= DamageAmt;
            // Red bar does not go down until combo attacks stop 
            if (SaveScript.Player1Timer < 2.0f)
            {
                SaveScript.Player1Timer += 2.0f;
            }
        }
    }
}
