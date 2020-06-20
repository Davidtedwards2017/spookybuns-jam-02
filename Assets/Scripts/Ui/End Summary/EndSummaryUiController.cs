using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using System.Linq;

public class EndSummaryUiController : UiController<EndSummaryUiController>
{
    private bool _ProceedFromSummary = false;
    public ReincarnationAsset Asset;

    private void Start()
    {
        SetActive(false);
    }

    public IEnumerator StartEndGameSummary(ReincarnationAsset reincarnation)
    {
        _ProceedFromSummary = false;
        Asset = reincarnation;

        SetActive(true);
        yield return new WaitUntil(() => _ProceedFromSummary);
        SetActive(false);
    }

    public void ShouldProceedFromSummary()
    {
        _ProceedFromSummary = true;
    }
}