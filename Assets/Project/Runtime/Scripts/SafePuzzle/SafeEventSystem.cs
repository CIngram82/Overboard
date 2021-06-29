using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeEventSystem : MonoBehaviour
{
    GameObject[] piston;
    GameObject[] ring;

    [SerializeField] float checkOffsetScale;
    [SerializeField] float checkDistance;
    Vector3[,] checks = new Vector3[6, 5];

    int[] relativeMovement;
    Vector3 mouseDownCursorLocation;
    int mouseDownObjectLocation;

    // Start is called before the first frame update
    void Start()
    {

        float twoOverSqrtThree = 2 / Mathf.Sqrt(3);
        float scale = transform.lossyScale.x * checkOffsetScale;

        for (int i = 0; i < 5; i++)
        {
            //checks[0,i] = new Vector3(0, 1, -(i - 1)) * scale;
            //checks[1,i] = new Vector3(-twoOverSqrtThree * (i - 1), 1, -0.5f * (i - 1)) * scale;
            //checks[2,i] = new Vector3(-twoOverSqrtThree * (i - 1), 1, 0.5f * (i - 1)) * scale;
            //checks[3,i] = new Vector3(0, 1, (i - 1)) * scale;
            //checks[4,i] = new Vector3(twoOverSqrtThree * (i - 1), 1, 0.5f * (i - 1)) * scale;
            //checks[5,i] = new Vector3(twoOverSqrtThree * (i - 1), 1, -0.5f * (i - 1)) * scale;

            //checks[0, i] = new Vector3(0, 1, -(i - 1)) * scale;
            //checks[1, i] = new Vector3(-twoOverSqrtThree * (i - 1), 1, -0.5f * (i - 1)) * scale;
            //checks[2, i] = new Vector3(-twoOverSqrtThree * (i - 1), 1, 0.5f * (i - 1)) * scale;
            //checks[3, i] = new Vector3(0, 1, (i - 1)) * scale;
            //checks[4, i] = new Vector3(twoOverSqrtThree * (i - 1), 1, 0.5f * (i - 1)) * scale;
            //checks[5, i] = new Vector3(twoOverSqrtThree * (i - 1), 1, -0.5f * (i - 1)) * scale;
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Debug.DrawLine(checks[i,j], new Vector3(checks[i,j].x, 0f, checks[i,j].z), Color.white, 2f);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
