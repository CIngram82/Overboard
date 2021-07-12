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
        [SerializeField] Vector3 startPos;
        [SerializeField] Image fadeImage;
        [SerializeField] float fadeSpeed;

        WaitForSeconds transitionDelay;
        RectTransform textCrawler;
        float targetAlpha = 1;


        IEnumerator Fade()
        {
            Color tempColor = fadeImage.color;
            while (fadeImage.color.a < targetAlpha)
            {
                tempColor.a = Mathf.MoveTowards(fadeImage.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                fadeImage.color = tempColor;
                yield return null;
            }
            //yield return transitionDelay;
            UIController.OnMenu();
        }

        void Update()
        {
            if (textCrawler.anchoredPosition.y < 0)
            {
                //Debug.Log($"{textCrawler.anchoredPosition.y} < {height} + {Screen.height + (Screen.height * (1 - canvasScale))}");
                textCrawler.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
            }
            else
            {
                StartCoroutine(Fade());
            }
        }

        void Awake()
        {
            textCrawler = GetComponent<RectTransform>();
            textCrawler.anchoredPosition = startPos;
            transitionDelay = new WaitForSeconds(transitionDelayTime);
        }


        IEnumerator Start()
        {
            yield return new WaitForFixedUpdate();
            LayoutRebuilder.ForceRebuildLayoutImmediate(textCrawler);
            //height = textCrawler.GetComponent<VerticalLayoutGroup>().preferredHeight;
        }
    }
}
