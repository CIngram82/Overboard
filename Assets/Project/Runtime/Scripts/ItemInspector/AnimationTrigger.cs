using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] Animation anim;


    public void PlayAnimation()
    {
        anim.Play();
    }
}





