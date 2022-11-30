using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour//obj gắn Script này có Colider
{
    public Sprite[] nenSp;
    public bool isLocking;
    public Transform tf_Floor;
    [Tooltip("Nếu là Level Rewward chỉ cần điền 2 điểm: đầu và điểm cuối vào list này")]
    [Header("Nếu là Level Rewward chỉ cần điền 2 điểm: đầu và điểm cuối vào list này")]
    [Header("Nếu là Level Bình thường cần điền đủ")]
    public List<Point_In_Floor> list_Point_In_Floor;
    public Stroke_Blue stroke_Blue;
    //
    public bool is_Floor_Last_Of_house;//phải đánh dấu biến này ngoài Scene trước ở Floor đã xác định
    public bool is_Floor_Last_Of_Level;//phải đánh dấu biến này ngoài Scene trước ở Floor đã xác định
    //Chỉ dành cho House Tree
    public GameObject obj_Floor_Player_To_Change;
    public GameObject obj_Floor_Enemy;
    
    [Header("------Chỉ Floor cuối cùng mà có 3 điểm trở lên cần điền------")]
    public Transform tf_Point_End_Level;//cần gắn ở ngoài Hearachy
    [Header("------Chỉ Level Full Reward cần điền------")]
    public Parent_3_Chest parent_3_Chest;//cần gắn ở ngoài Hearachy
    [Header("------Not Need Asign--To view------")]
    public bool is_Floor_Off_Tree_Empty = false;
     public bool isReady_Downt;
    [Tooltip("Chỉ Level Full Reward dùng biến này")]
    public bool isFloorOpenedReward;
     public House_Build house_Build_Of_This;
    // Start is called before the first frame update
    void Start()
    {
        isReady_Downt = false;
        Set_Add_On_List_Floor_House();
        Set_Spawn_Strock();
#if UNITY_EDITOR
        if (house_Build_Of_This == null)
        {
            Debug.Log("house_Build_Of_This__ NULL" + this.gameObject.name);
        }
#endif
        // 0 1 2 3 4 5

        if (obj_Floor_Enemy == null)
            return;
        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 1)
        {
            // bg = Instantiate(Resources.Load<ScrollBG>("BG/BG" + PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge()));
            if (PlayerPrefs_Manager.Get_Index_Level_Normal() < 11)
            {
                obj_Floor_Enemy.GetComponent<SpriteRenderer>().sprite = nenSp[Random.Range(3,6)];
            }
            else
            {
                obj_Floor_Enemy.GetComponent<SpriteRenderer>().sprite = nenSp[Random.Range(0, 3)];
            }
        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 2)
        {
            obj_Floor_Enemy.GetComponent<SpriteRenderer>().sprite = nenSp[Random.Range(0, 3)];
        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3)
        {
            obj_Floor_Enemy.GetComponent<SpriteRenderer>().sprite = nenSp[Random.Range(3, 6)];
        }


    }

    public void Set_Open_Reward()
    {
        parent_3_Chest.Set_Open_3_Chest();
    }
    public void Set_Floor_To_Floor_Of_Player()
    {
        if (house_Build_Of_This.houseType != Enum_TypeHouse.enemy_Tree)
        {
            obj_Floor_Player_To_Change.SetActive(true);
            obj_Floor_Enemy.SetActive(false);
        }
    }
    public void Set_Spawn_Strock()
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_Stroke), tf_Floor);
        stroke_Blue = obj.GetComponent<Stroke_Blue>();

        stroke_Blue.tf_stroke_Blue.localPosition = Vector3.zero;
        stroke_Blue.Set_Light_off();
    }

    //Chỉ dành cho House Tree
    public void Set_Empty_Floor_Tree()
    {
        is_Floor_Off_Tree_Empty = true;
    }
    public bool Get_isEmpty_Floor_Tree()
    {
         return is_Floor_Off_Tree_Empty;
    }

    public void Set_isReady_Downt()
    {
        isReady_Downt = true;
    }
    public void Set_Add_On_List_Floor_House()
    {
        if (GetComponentInParent<House_Build>() != null)
        {
            house_Build_Of_This = GetComponentInParent<House_Build>();
            house_Build_Of_This.list_Floor.Add(this);
        }
    }
    public Stroke_Blue Get_stroke_Blue()
    {
        return stroke_Blue;
    }

}
