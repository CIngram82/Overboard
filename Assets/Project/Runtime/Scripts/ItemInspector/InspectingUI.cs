using UnityEngine;

public class InspectingUI : MonoBehaviour
{
    [SerializeField] GameObject inspectPanel;


    public void On_Back()
    {
        InspectObject.IsInspecting = false;
        EventsManager.On_Item_Inspected(InspectObject.IsInspecting);
    }
    public void On_Inspect(bool inspecting)
    {
        inspectPanel.SetActive(inspecting);
        EventsManager.On_Camera_Switched(inspecting);

        float blurAmount = inspecting ? 25 : 0;
        PostProcessController.ChangeBlur(blurAmount);
    }

    void SubToEvents(bool subscribe)
    {
        EventsManager.ItemInspected -= On_Inspect;

        if (subscribe)
        {
            EventsManager.ItemInspected += On_Inspect;
        }
    }

    void OnEnable()
    {
        SubToEvents(true);
    }
    void OnDisable()
    {
        SubToEvents(false);
    }
    void Start()
    {
        inspectPanel.SetActive(false);
    }
}





