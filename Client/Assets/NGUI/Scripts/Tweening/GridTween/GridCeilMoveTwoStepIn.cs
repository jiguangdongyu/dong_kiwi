using UnityEngine;
using System.Collections;

/// <summary>
/// 移动两步进入↓→
/// </summary>
public class GridCeilMoveTwoStepIn : GridCeilTweenBase
{
    private Vector3 firstPos;

    public override void StartTween(float duration = 0.2f, float interval = 0.2f)
    {
        base.StartTween(duration, interval);
        if (gridTran.childCount > 0)
        {
            Transform firstTrans = gridTran.GetChild(0);
            firstPos = firstTrans.localPosition;

            Vector3 myPos = new Vector3(firstPos.x, firstPos.y + 40, firstPos.z);

            for (int i = 0; i < gridTran.childCount; i++)
            {
                Transform child = gridTran.GetChild(i).transform;

                child.localPosition = myPos;

                child.gameObject.SetActive(false);
            }
            ItemsTweenStep1();
        }
    }

    private void ItemsTweenStep1()
    {
        /*
        ceilIndex++;
        Transform curChild = gridTran.GetChild(ceilIndex);
        //curChild.gameObject.SetActive(true);
        UIWidget wdgt = curChild.gameObject.GetComponent<UIWidget>();
        curChild.gameObject.SetActive(true);

        TweenPosition tp1 = TweenPosition.Begin(curChild.gameObject, this.tweenDuration, firstPos);

        if (wdgt != null)
        {
            wdgt.alpha = 0.001f;
            TweenAlpha ta = TweenAlpha.Begin(curChild.gameObject, this.tweenDuration, 1f);
        }

        if (ceilIndex < gridTran.childCount - 1)
        {
            StartCoroutine(DelayCallFun.DoDelay(() =>
            {
                ItemsTweenStep2();
            }, this.tweenDuration));
        }
         * */
    }

    private void ItemsTweenStep2()
    {
        for (int idx = 0; idx < ceilIndex + 1; idx++)
        {
            Transform oneChild = gridTran.GetChild(idx);
            TweenPosition tp2 = TweenPosition.Begin(oneChild.gameObject, this.tweenDuration, new Vector3(oneChild.localPosition.x + gridTran.gameObject.GetComponent<UIGrid>().cellWidth, oneChild.localPosition.y, oneChild.localPosition.z));
        }
        StartCoroutine(DelayCallFun.DoDelay(() => { ItemsTweenStep1(); }, this.tweenDuration));
    }
}

public class DelayCallFun
{
    public static IEnumerator DoDelay(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}