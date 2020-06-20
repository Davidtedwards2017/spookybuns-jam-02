using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Module : MonoBehaviour {

    private Animator _Animator;
    public Animator Animator
    {
        get
        {
            if(_Animator == null)
            {
                _Animator = GetComponent<Animator>();
            }

            return _Animator;
        }
    }

    private bool _Active;

    public bool Active
    {
        get { return _Active; }
        set
        {
            if (_Active == value) return;

            _Active = value;

            if (_Active)
            {
                OnActivated();
            }
            else
            {
                OnDeactivated();
            }
        }
    }

    public virtual void SetActive(bool active)
    {
        Active = active;
    }

    protected virtual void OnActivated()
    {
    }

    protected virtual void OnDeactivated()
    {
    }

    //public Sequence AnimateIn()
    //{
    //    Sequence sequence = DOTween.Sequence();
    //
    //    var canvasGroup = GetComponent<CanvasGroup>();
    //    if (canvasGroup != null)
    //    {
    //        sequence.Append(canvasGroup.DOFade(1f, .3f));
    //    }
    //
    //    return sequence;
    //}
    //
    //public Sequence AnimateOut()
    //{
    //    Sequence sequence = DOTween.Sequence();
    //
    //    var canvasGroup = GetComponent<CanvasGroup>();
    //    if (canvasGroup != null)
    //    {
    //        sequence.Append(canvasGroup.DOFade(0f, .3f));
    //    }
    //    return sequence;
    //}

    public void SetPosition(Vector2 position)
    {
        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = position;
    }
}
