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



    public void PlayFootStepSoundLeft()
    {
        RaycastHit hit;

        if (Physics.Raycast(leftFoot.transform.position, -transform.up, out hit, rayDistance, ~ignoreMask))
        {
            if (hit.transform.CompareTag("Concrete"))
            {
                SoundManager.current.PlaySound(Sound.FootStep, leftFoot.position, footstepVolume);
            }
            
            if (hit.transform.CompareTag("Wood"))
            {
                SoundManager.current.PlaySound(Sound.FootStepWood, leftFoot.position, footstepVolume);
            }
        }
    }

    public void PlayFootStepSoundRight()
    {
        RaycastHit hit;

        if (Physics.Raycast(rightFoot.transform.position, -transform.up, out hit, rayDistance, ~ignoreMask))
        {
            if (hit.transform.CompareTag("Concrete"))
            {
                SoundManager.current.PlaySound(Sound.FootStep, leftFoot.position, footstepVolume);
            }

            if (hit.transform.CompareTag("Wood"))
            {
                SoundManager.current.PlaySound(Sound.FootStepWood, leftFoot.position, footstepVolume);
            }
        }
    }
}
