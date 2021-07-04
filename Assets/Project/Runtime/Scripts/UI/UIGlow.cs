using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Image image =  glowBackList[index].GetComponent<Image>();
        Color color = image.color;
        color.a = .0f;
        for (float i = 0; i < .5f; i += .1f)
        {
            color.a += i;
            yield return new WaitForSeconds(.3f);
            image.color = color;
        }

        glowBackList[index].SetActive(false);
        yield return null;
    }
  


}
