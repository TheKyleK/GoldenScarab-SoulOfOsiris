using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public Transform leftFoot;
    public Transform rightFoot;
    public float footstepVolume;
    public float rayDistance;
    public LayerMask ignoreMask;
    public Sound stone;
    public Sound sand;


    public void PlayFootStepSoundLeft()
    {
        RaycastHit hit;

        if (Physics.Raycast(leftFoot.transform.position, -transform.up, out hit, rayDistance, ~ignoreMask))
        {
            if (hit.transform.CompareTag("Stone"))
            {
                SoundManager.current.PlaySound(stone, leftFoot.position, footstepVolume);
            }

            if (hit.transform.CompareTag("Sand"))
            {
                SoundManager.current.PlaySound(sand, leftFoot.position, footstepVolume);
            }
        }
    }

    public void PlayFootStepSoundRight()
    {
        RaycastHit hit;

        if (Physics.Raycast(rightFoot.transform.position, -transform.up, out hit, rayDistance, ~ignoreMask))
        {
            if (hit.transform.CompareTag("Stone"))
            {
                SoundManager.current.PlaySound(stone, rightFoot.position, footstepVolume);
            }

            if (hit.transform.CompareTag("Sand"))
            {
                SoundManager.current.PlaySound(sand, rightFoot.position, footstepVolume);
            }
        }
    }
}
