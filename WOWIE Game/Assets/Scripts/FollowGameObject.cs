using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    private Transform _target;
    private Camera _cam;
    private RectTransform _rt;

    private RectTransform _canvas;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _rt = (RectTransform) transform;
        _canvas = (RectTransform)_rt.parent;
    }

    private void LateUpdate()
    {
        if (_target == null) return;
        
        _rt.anchoredPosition = WorldToCanvas(_canvas, _target.position, _cam);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    #if UNITY_EDITOR
    [ContextMenu("Test Set Target")]
    public void SetTarget()
    {
        SetTarget(GameObject.FindWithTag("Player").transform);
    }
    #endif
    
    
    private static Vector2 WorldToCanvas(RectTransform canvasRT, Vector3 world_position, Camera camera)
    {

        var viewport_position = camera.WorldToViewportPoint(world_position);


        return new Vector2((viewport_position.x * canvasRT.sizeDelta.x) - (canvasRT.sizeDelta.x * 0.5f),
            (viewport_position.y * canvasRT.sizeDelta.y) - (canvasRT.sizeDelta.y * 0.5f));
    }
}
