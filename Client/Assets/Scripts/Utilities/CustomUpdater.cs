using UnityEngine;
using System.Collections;

public class CustomUpdater : MonoBehaviour
{
    public UpdateDelegate UpdateProc;
    private void Update()
    {
        if (this.UpdateProc != null)
        {
            this.UpdateProc(base.gameObject);
        }
    }
    public delegate void UpdateDelegate(GameObject gameObject);
}
