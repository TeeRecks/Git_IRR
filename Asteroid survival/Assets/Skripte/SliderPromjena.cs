using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SliderPromjena : MonoBehaviour
{
    private Text ValueText;

    private void Start()
    {
        ValueText = GetComponent<Text>();
        GlobalneVarijable.Volumen = 0.75f;
    }

    public void OnSliderValueChanged(float value)
    {
        ValueText.text = value.ToString("0.00");
        GlobalneVarijable.Volumen = value;
    }
}
