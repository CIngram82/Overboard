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
        AudioScript._instance.PlaySoundEffect("Crank Fast");
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
        AudioScript._instance.PlaySoundEffect("Kraken_Slimy_Whoosh_01");
    }

    public void OtherTentacleSquish()
    {
        AudioScript._instance.PlaySoundEffect("Kraken_Slimy_Whoosh_02 1");
    }

    public void TentacleWaterMovement()
    {
       // AudioScript._instance.PlaySoundEffect("TentacleWater");
    }

    public void TentacleWoosh()
    {
        AudioScript._instance.PlaySoundEffect("Kraken_Whoosh");
    }
    public void OtherTentacleWoosh()
    {
        AudioScript._instance.PlaySoundEffect("Kraken_Whoosh_01");
    }

    public void KrakenRoar()
    {
        AudioScript._instance.PlaySoundEffect("Kraken_Roar");
    }
    
    public void HeavyBreathing()
    {
        AudioScript._instance.PlaySoundEffect("Heavy Breathing");
    }

    public void Run()
    {
        AudioScript._instance.PlayWoodFootsteps();
    }
}
