using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextIntValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiTextIntValue.text = soInt.valueInt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        uiTextIntValue.text = soInt.valueInt.ToString();
    }
}
