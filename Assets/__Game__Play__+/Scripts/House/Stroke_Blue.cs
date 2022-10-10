using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stroke_Blue : MonoBehaviour
{
    public Transform tf_stroke_Blue;
    public GameObject obj_stroke_Blue;
    //
    public GameObject obj_stroke_OutSide;
    public GameObject obj_stroke_Center;
    //
    public SpriteRenderer sprite_Out;
    public SpriteRenderer sprite_Center;
    private void Awake()
    {
        tf_stroke_Blue = transform;
        obj_stroke_Blue = gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Dùng sorting order bật sẵn anime các ảnh, sẽ làm cho các ô sáng đều nhau ko bị ô nhanh ô chậm
    public void Set_Light_Out_Side()
    {
        sprite_Out.sortingOrder = Constant.SortingOrder_Show;
        sprite_Center.sortingOrder = Constant.SortingOrder_Hide;
    }
    public void Set_Light_Center()
    {
        
        sprite_Out.sortingOrder = Constant.SortingOrder_Hide;
        sprite_Center.sortingOrder = Constant.SortingOrder_Show;
    }
    public void Set_Light_off()
    {
        sprite_Out.sortingOrder = Constant.SortingOrder_Hide;
        sprite_Center.sortingOrder = Constant.SortingOrder_Hide;
    }
}
