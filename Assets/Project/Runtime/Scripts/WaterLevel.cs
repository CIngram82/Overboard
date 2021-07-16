using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    float targetY;
    private bool isRising = false;
    static float t = 0.0f;

    private void Start()
    {
        targetY = transform.position.y;
    }
    void FixedUpdate()
    {
        RiseWaterByButton();
        if (!isRising)
        {
            return;
        }

      

        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(transform.position.y, targetY, t);
        transform.position = new Vector3(pos.x, pos.y, pos.z);

        // Tiny amount of time so it takes about 20 seconds for t to get close to 1
        t += 0.000002f;

        // Check if we are very close the the target. If so reset. 
        if (transform.position.y > targetY - 0.001f)
        {
            t = 0;
            isRising = false;
        }
    }

    public void TriggerWaterRise(float riseAmount)
    {
        isRising = true;
        targetY += riseAmount;  //0.66f
        t = 0;
    }
    public void SetWaterLevel(float level)
    {
        transform.position = new Vector3(transform.position.x, level, transform.position.z);
        targetY = transform.position.y;
    }

    void RiseWaterByButton()
    {
        if(Input.GetKey(KeyCode.P))
        {
            Debug.Log("P pressed");
            TriggerWaterRise(1);
        }
    }
}
