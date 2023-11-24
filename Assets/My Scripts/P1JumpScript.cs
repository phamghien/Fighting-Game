using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1JumpScript : MonoBehaviour
{
    public GameObject Player1;

    private void OnTriggerEnter(Collider other)
    {
        if (Player1Move.FacingRight == true)
        {
            // If objects collides with P2SpaceDetector, move player 
            if (other.gameObject.CompareTag("P2SpaceDetector"))
            {
                Player1.transform.Translate(-0.8f, 0, 0);
            }
        }

        if (Player1Move.FacingLeft == true)
        {
            // If objects collides with P2SpaceDetector, move player 
            if (other.gameObject.CompareTag("P2SpaceDetector"))
            {
                Player1.transform.Translate(0.8f, 0, 0);
            }
        }
    }
}
