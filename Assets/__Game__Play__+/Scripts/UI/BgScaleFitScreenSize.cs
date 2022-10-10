using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum Screne_Size { Ngang, Doc }
[DefaultExecutionOrder(5)]
public class BgScaleFitScreenSize : MonoBehaviour
{
    public Camera cam;
    public Transform trans_cam;
    public Screne_Size _screne_Size;
    
    public RectTransform rect_Hand_Doc;
    
    //public Vector3 vect_Hand_Doc;
    private void Start()
    {
        cam = Camera.main;
        trans_cam = cam.transform;
        checkScreen();
    }
    private void Update()
    {

    }
   
    
    public void checkScreen()
    {
        if (Screen.width == 1536 && Screen.height == 2048)//ipad 9.7 Portrait dọc
        {
            _screne_Size = Screne_Size.Doc;
            cam.orthographicSize = 75;
        }
        else if (Screen.width == 2048 && Screen.height == 1536)// ipad 9.7 Landscaspe ngang
        {
            _screne_Size = Screne_Size.Ngang;
            cam.orthographicSize = 75;
        }
        else if (Screen.height > Screen.width)//dọc
        {
            _screne_Size = Screne_Size.Doc;
            cam.orthographicSize = 75;
        }
        else if (Screen.height < Screen.width)//ngang
        {
            _screne_Size = Screne_Size.Ngang;
            cam.orthographicSize = 75;
        }
    }
    
}
