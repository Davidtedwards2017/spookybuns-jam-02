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

        if(OptionsArea.childCount == 0)
        {
            ConversationController.Instance.ChoiceOptionSelected(null);
        }
        else
        {
            yield return new WaitUntil(() => _ShouldProceed);
        }

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
            if (ShouldCreate(option))
            {
                SpawnOption(option);
            }
        }
    }

    private bool ShouldCreate(ChoiceDialogueOption data)
    {
        if (data.OnlyOnce && 
            StatsController.Instance.GetFlagValue(string.Format("selected.{0}", data.Id)) > 0)
        {
            return false;
        }

        if(data.Condition != null && 
            !ConversationController.ProcessConditionalStatement(data.Condition))
        {
            return false;
        }

        return true;
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

        StatsController.Instance.SetFlagValue(string.Format("selected.{0}", selected.Id), 1);
        ConversationController.Instance.ChoiceOptionSelected(selected);
    }
}