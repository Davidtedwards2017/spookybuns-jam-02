using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using System;

public class ChoiceDialogueModule : Module
{
    private bool _ShouldProceed;
    public Transform OptionsArea;
    public ChoiceOptionElement OptionPrefab;
    
    public IEnumerator PerformDialogue(ChoiceDialogueEntry dialogueEntry)
    {
        _ShouldProceed = false;
        ClearOptions();
        SpawnOptions(dialogueEntry);
        yield return new WaitUntil(() => _ShouldProceed);
    }

    private void ClearOptions()
    {
        foreach(Transform child in OptionsArea)
        {
            Destroy(child.gameObject);
        }
    }

    private void SpawnOptions(ChoiceDialogueEntry dialogueEntry)
    {
        foreach(var option in dialogueEntry.Options)
        {
            SpawnOption(option);
        }
    }

    private void SpawnOption(ChoiceDialogueOption data)
    {
        var instance = Instantiate(OptionPrefab, OptionsArea);
        instance.Initialize(data);
    }

    private void ChoiceOptionSelected(ChoiceDialogueOption selected)
    {
        _ShouldProceed = true;
        //not a great pattern, may change this to subscriber/broadcast model
        ConversationController.Instance.ChoiceOptionSelected(selected);
    }
}