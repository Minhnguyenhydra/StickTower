using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorldCam_Canvas : MonoBehaviour
{
    public Canvas canvas;
    //private void Awake()
    //{
    //    canvas = gameObject.GetComponent<Canvas>();
    //    canvas.renderMode = RenderMode.ScreenSpaceCamera;
    //    canvas.worldCamera = Camera.main;
    //}
    //private void Start()
    //{
    //    canvas = gameObject.GetComponent<Canvas>();
    //    canvas.renderMode = RenderMode.ScreenSpaceCamera;
    //    canvas.worldCamera = Camera.main;
    //    canvas.worldCamera = Camera.main;
    //}
    private void OnEnable()
    {
        canvas = gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
    }
}
