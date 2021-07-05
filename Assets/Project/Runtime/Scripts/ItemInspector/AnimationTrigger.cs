using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator anim;


    public void PlayOpen()
    {
        anim.SetBool("open", true);
    }

    public void PlayKey()
    {
        anim.SetBool("keyFound", true);
    }

}





