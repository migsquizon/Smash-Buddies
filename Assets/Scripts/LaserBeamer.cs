using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamer : MonoBehaviour

{
    public float atkRange;
    private LineRenderer _linerenderer;
    public Vector3 LaunchOffset;

    // Start is called before the first frame update
    void Start()
    {
        _linerenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 endPos = LaunchOffset + (transform.right *atkRange);
        _linerenderer.SetPosition(0,endPos);
    }
}
