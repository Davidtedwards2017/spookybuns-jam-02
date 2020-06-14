using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using UnityEngine.UI;

public class ChoiceOptionElement : Module
{
    public ChoiceDialogueOption OptionData;
    public Button Button;
    public Text TextArea;

    void Start()
    {
        Button.onClick.AddListener(OptionSelected);
    }

    public void Initialize(ChoiceDialogueOption optionData)
    {
        OptionData = optionData;
        TextArea.text = optionData.Value;
    }

    public void OptionSelected()
    {
        SendMessageUpwards("ChoiceOptionSelected", OptionData);
    }
}
