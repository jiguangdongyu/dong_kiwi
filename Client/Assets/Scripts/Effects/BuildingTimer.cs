using UnityEngine;
using System.Collections;

public class BuildingTimer : MonoBehaviour {

    public Material material;

    public float minY = 0;
    public float maxY = 5;
    public float duration = 2;

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Lerp(minY, maxY, Time.time / duration);
        material.SetFloat("_ConstructY", y);
    }
}