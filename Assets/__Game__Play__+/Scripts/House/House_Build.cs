using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[DefaultExecutionOrder(5)]
public class House_Build : MonoBehaviour
{
    public SpriteRenderer mai;
    public Sprite[] nenSp,maiSp;
    public Enum_TypeHouse houseType;
    public Transform tf_Mai_Nha;//đây là cái Mái Nhà
    public Transform tf_Nen;
    public List<Floor> list_Floor;
    //
    public GameObject obj_Mai_Xanh;
    public GameObject obj_Mai_Do;
    public GameObject obj_Nen_Player;
    public GameObject obj_Nen_Enemy;
    [Header("Số Floor còn lại của 1 nhà.. điền vào đây")]
    [Header("Level Reward")]
    [Tooltip("Số Floor còn lại của 1 nhà.. điền vào đây")]
    public int number_Floor_Remain;
    public bool isHouseLast_Level = false;
    public bool isSenconHouse_LevelReward;
    [Tooltip("Lv 26,chỉ cần điên số 2 vào nhà số 2 vào đây")]
    public int indext_House;

    [Header("------Not Need Asign--To view------")]
    public Transform tf_This_House;
    [Header("------Not Need Asign--To view------")]
    public int index_lv_26=0;
    public bool isGo_ToaNha_2;
    public bool is_Trigger_add_To_4;
    //Load obj_Prefabs_Floor băng Resource.load
    // Start is called before the first frame update
    public int totalEnemy;
    public int numEnemiesKilled;
    public bool isLastHouse;

    void Start()
    {
        tf_This_House = transform;
        
        Drag_Drop_Manager.Instance.list_House_Build.Add(this);

        Enemy[] enemies = GetComponentsInChildren<Enemy>();
        if (enemies != null && enemies.Length > 0)
            totalEnemy = enemies.Length;

        for (int i = 0; i < list_Floor.Count; i++)
        {
            if (list_Floor[i].is_Floor_Last_Of_Level)
            {
                isLastHouse = true;
            }
        }
    }
    private void Awake()
    {
        if (obj_Nen_Enemy == null)
            return;
        if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 1)
        {
            // bg = Instantiate(Resources.Load<ScrollBG>("BG/BG" + PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge()));
            if (PlayerPrefs_Manager.Get_Index_Level_Normal() < 11)
            {
                obj_Nen_Enemy.GetComponent<SpriteRenderer>().sprite = nenSp[1];
                mai.sprite = maiSp[1];
            }
            else
            {
                obj_Nen_Enemy.GetComponent<SpriteRenderer>().sprite = nenSp[0];
                mai.sprite = maiSp[0];
            }
        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 2)
        {
            obj_Nen_Enemy.GetComponent<SpriteRenderer>().sprite = nenSp[0];
            mai.sprite = maiSp[0];
        }
        else if (PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge() == 3)
        {
            obj_Nen_Enemy.GetComponent<SpriteRenderer>().sprite = nenSp[1];
            mai.sprite = maiSp[1];
        }

    }
    // Update is called once per frame
    //void Update()
    //{

    //}
    public void Set_Floor_Fall_Downt(Floor _this_Floor_Downt)
    {
        number_Floor_Remain--;
        for (int i = 0; i < list_Floor.Count; i++)
        {
            if (list_Floor[i] != _this_Floor_Downt)
            {
                if (list_Floor[i].tf_Floor.position.y > _this_Floor_Downt.tf_Floor.position.y)
                {
                    //tịnh tiến tất cả Floor còn lại xuống
                    list_Floor[i].tf_Floor.DOMoveY(list_Floor[i].tf_Floor.position.y + Constant.Floor_Y_Fall_Downt, Constant.Floor_Time_Fall_Downt);
                }
               
                
            }
            else
            {
                //Scale nhỏ Floor này
                //_this_Floor_Downt.tf_Floor.DOMoveY(list_Floor[i].tf_Floor.position.y + Constant.Floor_Y_Fall_Downt, Constant.Floor_Time_Fall_Downt);
                _this_Floor_Downt.tf_Floor.DOScale(Constant.Floor_Vec_Scale_Small, Constant.Floor_Time_Fall_Downt);
            }
        }
        tf_Mai_Nha.DOMoveY(tf_Mai_Nha.position.y + Constant.Floor_Y_Fall_Downt, Constant.Floor_Time_Fall_Downt);
        
    }


    public void Set_Check_Next_House_Or_Victory(Floor _floor)
    {

        //level Mở rương mới dùng
        if (number_Floor_Remain == 1)
        {
            if (isHouseLast_Level)
            {
                
                StartCoroutine(Delay_Win(_floor));
            }
            else
            {
                //Chuyển chuyển thành nhà player và di cam tớ nhà tiếp theo
                _floor.Set_Floor_To_Floor_Of_Player();
                _floor.house_Build_Of_This.Set_Mai_Xanh();
                _floor.house_Build_Of_This.Set_This_To_Team_Player();

                Drag_Drop_Manager.Instance.Set_Delay_Take_House(_floor,0);
            }
        }
    }
     IEnumerator Delay_Win(Floor _floor)
    {
        yield return Cache.GetWFS(0.1f);//Constant.Time_Delay_Fade_Win
        GameManager.Ins.Set_Mai_Xanh_Delay_Win(_floor);
    }
    public void Set_Floor_Up_Top( )
    {
        for (int i = 0; i < list_Floor.Count; i++)
        {
            list_Floor[i].tf_Floor.DOMoveY(list_Floor[i].tf_Floor.position.y - Constant.Floor_Y_Fall_Downt, Constant.Floor_Time_Fall_Downt);

        }
        tf_Mai_Nha.DOMoveY(tf_Mai_Nha.position.y - Constant.Floor_Y_Fall_Downt, Constant.Floor_Time_Fall_Downt);

        //Sinh ra sau rồi mới add vào List để ko bị dâng cao cùng list trước
        GameObject obj_Floor = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_Floor), tf_This_House);
        obj_Floor.GetComponentInChildren<Floor>().Set_Add_On_List_Floor_House();
        obj_Floor.transform.position = tf_Nen.position;
        obj_Floor.transform.DOScale(new Vector3(1, 1, 1), Constant.Floor_Time_Fall_Downt).From(new Vector3(0, 0, 0));
    }
    public void Set_Mai_Xanh( )
    {
        if (houseType != Enum_TypeHouse.enemy_Tree)
        {
            obj_Mai_Xanh.SetActive(true);
            obj_Mai_Do.SetActive(false);
            obj_Nen_Player.SetActive(true);
            obj_Nen_Enemy.SetActive(false);
            for (int i = 0; i < list_Floor.Count; i++)
            {
                if (list_Floor[i] != null)
                {
                    list_Floor[i].Set_Floor_To_Floor_Of_Player();

                }
            }
        }
    }

    //Biến nhà đỏ thành nhà xanh của Player và chọn nhà này để xây tiếp theo
    public void Set_This_To_Team_Player()
    {
        Drag_Drop_Manager.Instance.list_House_Build.Remove(this);
        if (houseType == Enum_TypeHouse.enemy_Reward)
        {
            for (int i = 0; i < list_Floor.Count; i++)
            {
                //TODO: ẩn các hòm khi sang nhà mới
                list_Floor[i].parent_3_Chest.gameObject.SetActive(false);
            }
        }
        houseType = Enum_TypeHouse.player;
        Player.ins.Set_House_To_Up_Top_Player(this);
    }
    public void Set_Only_Remove_Floor_From_list(Floor _Floor)
    {
        //chỉ dùng với Cây xanh
        _Floor.Set_Empty_Floor_Tree();
        list_Floor.Remove(_Floor);
    }

    public bool CanWin()
    {
        return numEnemiesKilled >= totalEnemy;
    }
}
