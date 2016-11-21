using UnityEngine;
using System.Collections;

/// <summary>
/// 放大淡入
/// </summary>
public class GridCeilScaleFadeIn : GridCeilTweenBase
{
    public override void StartTween(float duration = 0.2f, float interval = 0.2f)
    {
        base.StartTween(duration, interval);
        if (gridTran.childCount > 0)
        {
            for (int i = 0; i < gridTran.childCount; i++)
            {
                Transform child = gridTran.GetChild(i).transform;
                child.gameObject.SetActive(false);
            }
            ItemsTweenScaleFadeIn();
        }
    }

    private void ItemsTweenScaleFadeIn()
    {
        ceilIndex++;

        Transform curChild = gridTran.GetChild(ceilIndex);
        UIWidget wdgt = curChild.gameObject.GetComponent<UIWidget>();
        curChild.gameObject.SetActive(true);

        if (wdgt != null)
        {
            wdgt.alpha = 0.001f;
            wdgt.transform.localScale = new Vector3(1f, 0.0f, 1f);
            TweenAlpha ta = TweenAlpha.Begin(curChild.gameObject, this.tweenDuration, 1f);
            TweenScale ts = TweenScale.Begin(curChild.gameObject, this.tweenDuration, new Vector3(1, 1, 1));
        }

        if (ceilIndex < gridTran.childCount - 1)
        {
            StartCoroutine(DelayCallFun.DoDelay(() =>
            {
                ItemsTweenScaleFadeIn();
            }, this.ceilTweenInterval));
        }
    }
}
