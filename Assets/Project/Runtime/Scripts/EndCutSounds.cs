using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutSounds : MonoBehaviour
{
   public void BoatSplash()
   {
        AudioScript._instance.PlaySoundEffect("Fall and Splash");
   }

    public void CrankSpin()
    {
        AudioScript._instance.PlaySoundEffect("Crank Single");
    }

    public void TentacleSmash1()
    {
        AudioScript._instance.PlaySoundEffect("Tentacle Smash 1");
    }

    public void TentacleSmash2()
    {
        AudioScript._instance.PlaySoundEffect("Tentacle Smash 2");
    }

    public void TentacleSquish()
    {
        //AudioScript._instance.PlaySoundEffect("Squish");
    }

    public void TentacleWaterMovement()
    {
       // AudioScript._instance.PlaySoundEffect("TentacleWater");
    }

    public void TentacleWoosh()
    {
        //AudioScript._instance.PlaySoundEffect("TentacleWoosh");
    }

    public void Run()
    {
        AudioScript._instance.PlayWoodFootsteps();
    }
}
