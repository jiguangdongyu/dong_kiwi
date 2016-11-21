using UnityEngine;
using System.Collections;

/// <summary>
/// 放大进入
/// </summary>
public class GridCeilScaleIn : GridCeilTweenBase
{
    public override void StartTween(float duration = 0.2f, float ceilTweenInterval = 0.2f)
    {
        base.StartTween(duration, ceilTweenInterval);

        if (gridTran == null)
            return;

        if (gridTran.childCount > 0)
        {
            HideAllCeils();
            ItemsTweenStep1();
        }
    }

    private void HideAllCeils()
    {
        for (int idx = 0; idx < gridTran.childCount; idx++)
        {
            gridTran.GetChild(idx).gameObject.SetActive(false);
        }
    }

    private void ItemsTweenStep1()
    {
        ceilIndex++;

        Transform curChild = gridTran.GetChild(ceilIndex);
        curChild.gameObject.SetActive(true);
        UIWidget wdgt = curChild.gameObject.GetComponent<UIWidget>();

        if (wdgt != null)
        {
            Vector3 tempScale = wdgt.transform.localScale;
            wdgt.transform.localScale = Vector3.zero;
            TweenScale ta = TweenScale.Begin(curChild.gameObject, this.tweenDuration, tempScale);
        }

        if (ceilIndex < gridTran.childCount - 1)
        {
            StartCoroutine(DelayCallFun.DoDelay(() =>
            {
                ItemsTweenStep1();
            }, this.ceilTweenInterval));
        }
    }
}
