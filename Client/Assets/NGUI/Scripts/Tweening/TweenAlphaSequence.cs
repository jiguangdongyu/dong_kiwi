using UnityEngine;

public class TweenAlphaSequence : MonoBehaviour
{
    public GameObject[] tweenObjs;
    public float objTweenDuration = 0.2f;
    public float objTweenInterval = 0.06f;

    public bool bEnableStart = true;

    public void OnEnable()
    {
        if (!bEnableStart)
            return;

        BeginSequence();
    }

    private void BeginSequence()
    {
        for (int i = 0; i < tweenObjs.Length; i++)
        {
            if (tweenObjs[i] != null && tweenObjs[i].activeSelf)
            {
                UIWidget wd = tweenObjs[i].transform.GetComponent<UIWidget>();
                if (wd == null)
                {
                    continue;
                }
                wd.alpha = 0.01f;
                TweenAlpha ta = TweenAlpha.Begin(tweenObjs[i], objTweenDuration, 1, objTweenInterval * i);
            }
        }
    }
}
