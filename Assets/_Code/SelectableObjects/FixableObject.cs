using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FixableObject : SelectableObject
{
    [Header("Fix Time")] public float fixTime;

    private bool isFixed = false;
    private bool isFixing = false;
    private GameObject fixWindow;
    private Vector3 fixWindowScale;
    private GameObject fixAlarm;

    protected void Start()
    {
        base.Start();
        fixWindow = transform.Find("FixWindow").gameObject;
        fixAlarm = transform.Find("FixAlarm").gameObject;
        fixAlarm.transform.DOScale(Vector3.one, 0.4f).SetLoops(-1, LoopType.Yoyo);
        fixWindowScale = fixWindow.transform.localScale;
    }

    public override void OnAction(PlayerMove player)
    {
        base.OnAction(player);
        fixWindow.SetActive(true);
        fixWindow.transform.DOScaleX(0, fixTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            fixWindow.SetActive(false);
            isFixed = true;
            transform.DOScale(Vector3.zero, 0.5f);
            Destroy(gameObject, 0.5f);
        }).SetId(fixWindow);
        isFixing = true;
    }

    public override void OnFinishAction()
    {
        base.OnFinishAction();
        DOTween.Kill(fixWindow);
        fixWindow.transform.localScale = fixWindowScale;

        if (isFixing)
        {
            fixWindow.SetActive(false);
            isFixing = false;
        }
    }
}