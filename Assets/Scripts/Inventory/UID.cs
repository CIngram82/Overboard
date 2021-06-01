using System.Text;

public class UID
{
    string seperator = "-";

    public string ID { get; private set; }

    public UID(params string[] arguments)
    {
        UpdateID(arguments);
    }
    public UID()
    {
        ID = string.Empty;
    }

    public void UpdateID(params string[] arguments)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < arguments.Length; i++)
        {
            sb.Append(arguments[i]);
            if (i < arguments.Length)
                sb.Append(seperator);
        }
        ID = sb.ToString();
    }
}
