using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGlow : MonoBehaviour
{
   public List<GameObject> glowBackList = new List<GameObject>();

    public  void AddBackdrop(int inventoryIndex)
    {
      
        StartCoroutine(disableBackdrop(inventoryIndex));
    }


    public IEnumerator disableBackdrop(int index)
    {
        glowBackList[index].SetActive(true);
        yield return new WaitForSeconds(4f);
        glowBackList[index].SetActive(false);
    }
  


}
