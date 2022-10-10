using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sword_Ads_TopLeft : Singleton<Sword_Ads_TopLeft>
{
    //UNDONE: khi bật Canvas GamePlay... xét nếu màn có chức năng xem ads nhận kiếm mới bật button ADs Sword
    //Tạo 1 Prefabs kiếm để ở scene
    //Tạo 1 obj có tf là Mid thả và tf dưới
    //Gắn vào button Ads Sword:  Set_Go_To_Herro() ở dưới khi bấm vào
    [Header("--Kiếm này ở world space ... bay lên điểm trên top .. tới ảnh mấy thành và bay xuống tay Hero.. Hero thay kiếm--Từng size màn hình thay đổi tf_Sword_Ads_Go_Mid để trung với ảnh canvas tòa thành")]
    public Transform tf_this_Sword;
    //Chưa dùng đến:      public Transform tf_Sword_Ads_Start;//để set điểm lúc đầu đứng tùy vào Size màn hình
    public Transform tf_Sword_Ads_Go_Mid;//điểm trên tòa thành giữa màn hình
    [Header("Điền Id vào đây.. từ 1 đến 4")]
    [Tooltip("Id .. từ 1 đến 4, ")]
    public int id_Sword;
    //Player thêm tf gần tay để kiếm này bay vào
    private void Awake()
    {
        //tf_this_Sword = transform;
        tf_this_Sword.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("TEST")]
    public void Set_Go_To_Herro()
    {
        tf_this_Sword.gameObject.SetActive(true);
        tf_this_Sword.DOMove(tf_Sword_Ads_Go_Mid.position, Constant.Time_Sword_ADs_Go_To_Mid).OnComplete(
            ()=>
            {
                tf_this_Sword.DOMove(Player.ins.tf_Point_Sword_ADs_Go.position, Constant.Time_Sword_ADs_Go_To_Mid).OnComplete(()=>
                {
                    string name_Skin = Constant.Get_Skin_Name_By_Id_Sword(id_Sword);
                    Player.ins.Set_Skin(name_Skin);
                    Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.health, Player.ins.health + 2, 1);// +2 damge
                    Player.ins.Set_Add_Health(2);//X2 damge
                    //bật anim ở Hero nhận đc dame
                    Player.ins.Set_Anim_TakeSword();
                    Destroy(this.gameObject);
                }
                );
            }
            );
    }
}
/*
 
mỗi scene có:    01 Obj....... chứa 3 ảnh chìa khóa xám

Lưu chỉ số: Key nhận được ở mỗi màn

Khi nhặt được key sẽ gọi đến :    Key_3_item.ins.Showkey
    Load chỉ số mà Key vàng sẽ bay đến trong 3 cái ảnh Key trên đầu màn hình
    Key bay đến chỉ số trống của key


3 key
    level  4:  








 */