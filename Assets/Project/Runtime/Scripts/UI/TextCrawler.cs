using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Controllers.UI
{
    public class TextCrawler : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] float scrollSpeed;
        [SerializeField] float transitionDelayTime;

        WaitForSeconds transitionDelay;
        RectTransform textCrawler;

        IEnumerator TextCrawlerVert()
        {
            float scrollPos = 0.0f;
            Vector3 startPos = textCrawler.position;
            float height = textCrawler.GetComponent<VerticalLayoutGroup>().preferredHeight + Screen.height ;
            while (scrollPos < height)
            {
                scrollPos += scrollSpeed * Time.deltaTime;
                textCrawler.transform.position = new Vector3(startPos.x, scrollPos, startPos.z);
                yield return null;
            }
            yield return transitionDelay;
            UIController.OnMenu();
        }

        void Awake()
        {
            textCrawler = GetComponent<RectTransform>();
            textCrawler.position = new Vector3(textCrawler.position.x, 0.0f, textCrawler.position.z);
            transitionDelay = new WaitForSeconds(transitionDelayTime);
        }


        IEnumerator Start()
        {
            yield return new WaitForFixedUpdate();
            LayoutRebuilder.ForceRebuildLayoutImmediate(textCrawler);
            StartCoroutine(TextCrawlerVert());
        }
    }
}
