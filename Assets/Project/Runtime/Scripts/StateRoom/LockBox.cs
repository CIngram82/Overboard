using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBox : MonoBehaviour
{
    [SerializeField] GameObject doorKey;
    [SerializeField] GameObject animKey;

    public void InsertKey()
    {
        animKey.SetActive(true);
    }
 
   public void Unlock()
   {
        doorKey.SetActive(true);
   }

}
