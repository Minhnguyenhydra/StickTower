using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class House_Fix_Lv_26 : Singleton<House_Fix_Lv_26>
{
    public Transform tf_Mai_Nha_Xanh_Fix;//cho sụp xuống lần đầu tiên khi chuyển sang nhà mới vì n đang lỗi
    // Start is called before the first frame update
    public bool first_Fix;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fix_Mai_Nha_Xanh_1Lan()
    {
        if (!first_Fix)
        {
            first_Fix = true;
            
        }
    }

}
