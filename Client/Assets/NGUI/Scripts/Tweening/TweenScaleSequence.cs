using UnityEngine;

public class TweenScaleSequence : MonoBehaviour
{
    public GameObject[] tweenObjs;
    public float objTweenDuration = 0.2f;
    public float objTweenInterval = 0.06f;
    public Vector3 objTargetScale = Vector3.one;

    public bool bEnableStart = true;
    public bool bOnce = false;
    private bool bPlayed = false;

    private void OnEnable()
    {
        if (!bEnableStart)
            return;

        BeginSequence();
    }

    public void BeginSequence()
    {
        if (bOnce && bPlayed)
            return;

        if (!bPlayed)
            bPlayed = true;

        for (int i = 0; i < tweenObjs.Length; i++)
        {
            if (tweenObjs[i] != null && tweenObjs[i].activeSelf)
            {
                Transform ts = tweenObjs[i].transform.GetComponent<Transform>();
                if (ts == null)
                {
                    continue;
                }
                ts.transform.localScale = Vector3.one * 0.01f;
                TweenScale ta = TweenScale.Begin(tweenObjs[i], objTweenDuration, objTargetScale, objTweenInterval * i);
                ta.animationCurve.AddKey(new Keyframe(0.705f, 1.105f));
            }
        }
    }
}
