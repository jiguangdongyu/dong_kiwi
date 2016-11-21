using UnityEngine;
using System.Collections;

public abstract class GridCeilTweenBase : MonoBehaviour
{
    protected int ceilIndex = -1;
	protected Transform gridTran;
	public float tweenDuration = 0.2f;
    public float ceilTweenInterval = 0.2f;

    public virtual void StartTween(float duration = 0.2f, float interval = 0.2f)
	{
		gridTran = GetComponent<UIGrid>().transform;
        ceilIndex = -1;
        tweenDuration = duration;
        ceilTweenInterval = interval;
	}
}
