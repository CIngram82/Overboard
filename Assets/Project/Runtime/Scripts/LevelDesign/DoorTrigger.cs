using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("isOpen", true);
        //AudioScript._instance.PlaySoundEffect("Door Creak");
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isOpen", false);
    }
}
