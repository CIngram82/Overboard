using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] string animBoolName = "isOpen";
    [SerializeField] Animator anim;
    [SerializeField] bool isTrigger;


    public void Play()
    {
        if (!isTrigger)
        {
            anim.SetBool(animBoolName, !anim.GetBool(animBoolName));
        }
        else
        {
            PlayTrigger();
        }
    }
    public void PlayTrigger()
    {
        anim.SetTrigger(animBoolName);
    }
    public void PlayKey()
    {
        anim.SetBool("keyFound", true);
    }

}





