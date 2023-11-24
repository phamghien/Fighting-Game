using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2JumpScript : MonoBehaviour
{
    public GameObject Player2;

    private void OnTriggerEnter(Collider other)
    {
        if (Player1Move.FacingLeft == true)
        {
            // If objects collides with P1SpaceDetector, move player 
            if (other.gameObject.CompareTag("P1SpaceDetector"))
            {
                Player2.transform.Translate(-0.8f, 0, 0);
            }
        }

        if (Player1Move.FacingRight == true)
        {
            // If objects collides with P1SpaceDetector, move player 
            if (other.gameObject.CompareTag("P1SpaceDetector"))
            {
                Player2.transform.Translate(0.8f, 0, 0);
            }
        }
    }
}
