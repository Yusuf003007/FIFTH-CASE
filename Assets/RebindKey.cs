using TMPro;
using UnityEngine;

public class RebindKey : MonoBehaviour
{
    public KeyCode currentKey = KeyCode.E;

    public TextMeshProUGUI keyText;

    private bool waitingForKey = false;

    void Start()
    {
        UpdateKeyText();
    }

    void OnGUI()
    {
        if (waitingForKey)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                currentKey = e.keyCode;
                waitingForKey = false;

                UpdateKeyText();
            }
        }
    }

    public void StartRebind()
    {
        waitingForKey = true;
        keyText.text = "Press key...";
    }

    void UpdateKeyText()
    {
        keyText.text = currentKey.ToString();
    }
}