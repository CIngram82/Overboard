using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tooltipText;
    GameObject tooltip;


    public void EnableTooltip(string text)
    {
        tooltipText.text = text;
        tooltip.SetActive(true);
    }
    public void DisableTooltip()
    {
        tooltipText.text = string.Empty;
        tooltip.SetActive(false);
    }

    private void Awake()
    {
        tooltip = transform.GetChild(0).gameObject;
        DisableTooltip();
        GameEvents.ToolTipActivated += EnableTooltip;
        GameEvents.ToolTipDeactivated += DisableTooltip;
    }
    private void Update()
    {
        if (tooltip.activeSelf)
        {
            transform.position = Input.mousePosition;
        }
    }
}
