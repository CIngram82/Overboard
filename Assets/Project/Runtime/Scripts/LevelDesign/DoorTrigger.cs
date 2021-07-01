using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("isOpen", true);
        anim.SetBool("isUnlocked", true);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isOpen", false);
        anim.SetBool("isUnlocked", false);
    }
}
