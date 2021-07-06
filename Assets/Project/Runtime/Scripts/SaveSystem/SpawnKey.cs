using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
   [SerializeField] GameObject key;

    public void Unlock()
    {
        key.SetActive(true);
    }
}
