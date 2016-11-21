using UnityEngine;
using System.Collections;

public class GridCeilFadeIn : GridCeilTweenBase
{
    public override void StartTween(float duration = 0.2f, float interval = 0.2f)
    {
        base.StartTween(duration, interval);

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
            wdgt.alpha = 0;
            TweenAlpha ta = TweenAlpha.Begin(curChild.gameObject, this.tweenDuration, 1f);
        }

        if (ceilIndex < gridTran.childCount - 1)
        {
            /*
            StartCoroutine(DelayCallFun.DoDelay(() =>
            {
                ItemsTweenStep1();
            }, this.ceilTweenInterval));
             * */
        }
    }
}
