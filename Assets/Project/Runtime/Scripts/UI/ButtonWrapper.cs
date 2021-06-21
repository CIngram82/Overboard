using TMPro;

namespace UnityEngine.UI.Ext
{
    public class ButtonWrapper : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _label;

        public TextMeshProUGUI Label
        {
            get
            {
                if (_label != null) return _label;
                return _label = GetComponentInChildren<TextMeshProUGUI>();
            }
        }
    }
}
