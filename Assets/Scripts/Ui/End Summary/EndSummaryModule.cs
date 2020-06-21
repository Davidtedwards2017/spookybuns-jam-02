using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;

public class EndSummaryModule : Module
{
    public Button ProceedButton;
    public Text TextArea;
    public Image Image;

    private void Start()
    {
        ProceedButton.onClick.AddListener(ProceedButtonSelected);
    }

    protected override void OnActivated()
    {
        Initialize(EndSummaryUiController.Instance.Asset);
        Animator.SetBool("visible", true);
        ProceedButton.animator.SetBool("visible", true);
        base.OnActivated();
    }
       
    protected override void OnDeactivated()
    {
        Animator.SetBool("visible", false);
        base.OnDeactivated();
    }

    public void Initialize(ReincarnationAsset asset)
    {
        TextArea.text = asset.Description;
        Image.sprite = asset.Sprite;
        StartCoroutine(UpdateLayoutGroups());
    }

    public void ProceedButtonSelected()
    {
        EndSummaryUiController.Instance.ShouldProceedFromSummary();
    }

    private IEnumerator UpdateLayoutGroups()
    {
        var layoutGroup = GetComponentInChildren<LayoutGroup>();
        layoutGroup.enabled = false;
        yield return new WaitForFixedUpdate();
        layoutGroup.enabled = true;
    }
}
