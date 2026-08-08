using UnityEngine;
using TMPro;

public class SOUIFloatUpdate : MonoBehaviour
{
    public SOFloat soFloat;
    public TextMeshProUGUI uiTextFloatValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiTextFloatValue.text = soFloat.valueFloat.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        uiTextFloatValue.text = soFloat.valueFloat.ToString();
    }
}
