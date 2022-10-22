using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[DefaultExecutionOrder(1)]
public class Drag_Drop_Manager : Singleton_Q<Drag_Drop_Manager>
{
    #region Khai báo biến
    public Camera cam;
    public Transform tf_Cam;
    [Header("Chỉ House Reward mới dùng")]
    public bool isFistGo_SenconHose = false;
    [Header("------Not Need Asign--To view------")]
    public bool isHold_Mouse;
     public bool isChose_Player;
     Vector3 pos_WorldMousePosition;
     public Floor floor_Raycast_To;
     public Floor floor_Raycast_Before;
     public List<House_Build> list_House_Build;
    //
    private IState<Drag_Drop_Manager> currentState;
    //Stoke Light Blue
     public Floor floor_Light_Before;//để check khi di chuyển giữa 2 Floor khác nhau
     public Floor floor_Light_Current;
     public Floor floor_Light_First_Chose;//Floor đầu tiên mà player đang đứng sau mỗi lần di chuyển
    //
     public bool is_Raycast_To_Floor = false;
     public bool is_Raycast_To_First_Floor = false;//Floor đầu tiên mà player đang đứng sau mỗi lần di chuyển
    //
     public bool is_Sencond_Raycast_To_First_Floor = false;//khi raycast và First Floor lần thứ 2 thì tắt xanh ở center Floor trước đi
     public int index_Pos_Cam_Move_In_List = 0;
     public int index_House_Chiem_Duoc = 0;
    //
    public bool isFistConfig;
    public int number_House;
    //
    public int index_Step_Tut_0_use;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        index_Step_Tut_0_use = 0;
        cam = Camera.main;
        tf_Cam = Camera.main.gameObject.GetComponent<Transform>();
        //ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFistConfig)
        {
            isFistConfig = true;
            number_House = list_House_Build.Count - 1;
        }
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        if (Player.ins != null)
        {
            if (!Player.ins.is_Block_Raycas)
            {
                Set_Check_Mouse_Hold();
                Set_Check_Light_Downt_Up_Click();
                //Debug.Log(floor_Light_First_Chose.gameObject.GetInstanceID());
                Set_Check_Follow_Player();
            }
        }
        
    }

    #region Lấy vị trí chuột quy ra World Space
    public Vector3 Get_World_Point_Z_0_Mose()
    {
        pos_WorldMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        pos_WorldMousePosition.z = 0;

        return pos_WorldMousePosition;
    }
    #endregion
    #region cho Player di chuyển theo liền trỏ chuột trên màn hình
    public void Set_Check_Follow_Player()
    {
        if (isChose_Player)
        {
            if (isHold_Mouse)
            {
                Get_World_Point_Z_0_Mose();
                if (Player.ins != null)
                {
                    Player.ins.tf_Player.position = Vector3.Lerp(Player.ins.tf_Player.position, pos_WorldMousePosition, Time.deltaTime * Constant.Player_Speed_Follow);
                }

            }
        }
    }
    #endregion
    #region Kiểm tra giữ chuột và thả chuột ra
    public void Set_Check_Mouse_Hold()
    {
        #region Kiểm tra nhấn chuột, Bắn Racast chỉ với các Colider 3D
        if (Input.GetMouseButtonDown(0))
        {
            Set_Check_Raycast_Colider3D_Player();

            isHold_Mouse = true;

            //if (Tut_0_Game_Play.Ins != null)
            //{
            //    Tut_0_Game_Play.Ins.SetChange_State(Enum_Anim_0.anim_03);
            //}
            index_Step_Tut_0_use = -1;
            if (index_Step_Tut_0_use == 1)
            {
                index_Step_Tut_0_use = 2;
            }
        }
        #endregion
        #region Kiểm tra khi thả chuột
        if (Input.GetMouseButtonUp(0))
        {
            Player.ins.floor_stay.Get_stroke_Blue().Set_Light_off();
            #region Lấy các Floor bị tia Raycast bắn vào
            Set_Check_Raycast_Floor();

            #endregion
            //Nếu đang giữ Player--nhả chuột sẽ thả về chỗ đứng lúc trước gần nhất
            if (isChose_Player)
            {
                Player.ins.Set_Scale_Normal();
                if (floor_Raycast_To != null)
                {
                    if (floor_Raycast_To.house_Build_Of_This.houseType != Enum_TypeHouse.player && !floor_Raycast_To.isLocking)
                    {
                        //Nếu là Normal Floor thì cho đánh luônCheck Floor Before - After có trùng nhau và có bị xụp xuống***********
                        bool isDelayPlayer_attack_By_Downt = false;

                        if (floor_Raycast_Before == null)
                        {
                            floor_Raycast_Before = floor_Raycast_To;
                        }
                        else if (floor_Raycast_Before == floor_Raycast_To)
                        {
                            //trùng cái cũ

                        }
                        #region Nếu Raycast vào Floor mới
                        else if (floor_Raycast_Before != floor_Raycast_To)
                        {
                            //khác cái cũ mà rỗng ko chứa enemy hoặc chap nào thì...
                            if (floor_Raycast_Before.isReady_Downt && !floor_Raycast_Before.is_Floor_Last_Of_house)
                            {
                                //Nếu là Cây Floor thì cho đánh luôn
                                isDelayPlayer_attack_By_Downt = true;

                                //Cho nhà Player cao lên, nếu nhà enemy ko phải Cây có lá
                                //Chọn sẵn houseType từ Herachi, search từ house ở Hirarechy rồi điền từng nhà cho nhanh
                                if (floor_Raycast_To.house_Build_Of_This.houseType == Enum_TypeHouse.enemy_Normal) 
                                {
                                    floor_Raycast_Before.house_Build_Of_This.Set_Floor_Fall_Downt(floor_Raycast_Before);

                                    
                                    Player.ins.house_Build_Of_Player.Set_Floor_Up_Top();
                                }
                                else if (floor_Raycast_To.house_Build_Of_This.houseType == Enum_TypeHouse.enemy_Reward)
                                {


                                    //Quan
                                    if (floor_Raycast_Before.house_Build_Of_This.houseType != Enum_TypeHouse.player)
                                    {
                                        floor_Raycast_Before.house_Build_Of_This.Set_Floor_Fall_Downt(floor_Raycast_Before);

                                    }
                                    if (floor_Raycast_Before.house_Build_Of_This.number_Floor_Remain > 0)
                                    {
                                        if (floor_Light_Current.house_Build_Of_This.indext_House == 2)
                                        {
                                            if (!isFistGo_SenconHose)
                                            {
                                                isFistGo_SenconHose = true;

                                            }
                                            else
                                            {
                                                Player.ins.house_Build_Of_Player.Set_Floor_Up_Top();
                                            }
                                        }
                                        else
                                        {
                                            Player.ins.house_Build_Of_Player.Set_Floor_Up_Top();
                                            
                                        }
                                    }
                                }

                                else
                                {
                                    //Nếu là cây xanh thì chỉ Remove Floor rỗng sau khi rồi Player đi, ko để Floor sụp xuống
                                    //Chỉ hiện BG xanh ở các Floor có trong list khi di chuột vào
                                    floor_Raycast_Before.house_Build_Of_This.Set_Only_Remove_Floor_From_list(floor_Raycast_Before);
                                }
                                floor_Raycast_Before = floor_Raycast_To;
                            }
                        }
                        #endregion
                        //************************************************************************

                        //Merge được
                        bool isFind_First_Point_Empty = false;//Điểm gần bên phải nhất
                        if (!isFind_First_Point_Empty)
                        {
                            Vector3 vec_pos_Can_Place_In_Floor = Vector3.zero;
                            //vì thứ tự trong list ở ngoài Hearachy bị xếp ngược từ dưới lên trên là 0 > count- 1..  for (int i = floor_Raycast_To.list_Point_In_Floor.Count - 1; i >= 0 ; i--)
                            if (floor_Raycast_To.house_Build_Of_This.houseType != Enum_TypeHouse.enemy_Reward)
                            {
                                for (int i = 0; i < floor_Raycast_To.list_Point_In_Floor.Count; i++)
                                {
                                    ////// && !floor_Raycast_To.is_Floor_Off_Tree_Empty
                                    ///
                                    #region  Nếu Raycast vào Floor Normal, đặt Player vào ô đó
                                    if (floor_Raycast_To.house_Build_Of_This.houseType == Enum_TypeHouse.enemy_Normal)
                                    {
                                        if (floor_Raycast_To.list_Point_In_Floor[i].isEmpty && !isFind_First_Point_Empty)
                                        {
                                            isFind_First_Point_Empty = true;

                                            Player.ins.tf_Player.SetParent(floor_Raycast_To.list_Point_In_Floor[i].tf_Point_In_Floor);
                                            vec_pos_Can_Place_In_Floor = floor_Raycast_To.list_Point_In_Floor[i].tf_Point_In_Floor.position;
                                            Player.ins.Set_Floor_Indext_Point(floor_Raycast_To, i);
                                            Player.ins.Set_Block_Colider_Player();
                                            //Nếu có tâng nào đang sụp xuống thì Delay attack player
                                            Set_Check_State_Player_Do_Next(floor_Raycast_To, i, isDelayPlayer_attack_By_Downt);
                                            //SetParent vào điểm gắn để đi xuống cùng Floor nếu Floor sụp xuống

                                            Player.ins.tf_Player.localPosition = Vector3.zero;
                                            //Player.ins.Set_Pos_Old(vec_pos_Can_Place_In_Floor);
                                            Player.ins.Set_Pos_Old(Player.ins.tf_Player.localPosition);
                                            //Debug.Log("1");
                                            if (index_Step_Tut_0_use != 2)
                                            {
                                                index_Step_Tut_0_use = 1;

                                            }
                                        }
                                    }
                                    


                                    //FIX
                                    if (floor_Raycast_To.house_Build_Of_This.houseType == Enum_TypeHouse.player)
                                    {
                                        vec_pos_Can_Place_In_Floor = floor_Raycast_To.list_Point_In_Floor[1].tf_Point_In_Floor.position;
                                        //Player.ins.Set_Pos_Old(vec_pos_Can_Place_In_Floor);
                                        Player.ins.Set_Fix_Pos_Player();


                                    }






                                    #endregion

                                    #region Nếu Raycast vào Floor Tree
                                    else if (floor_Raycast_To.house_Build_Of_This.houseType == Enum_TypeHouse.enemy_Tree)
                                    {

                                        if (!floor_Raycast_To.is_Floor_Off_Tree_Empty)
                                        {
                                            if (floor_Raycast_To.list_Point_In_Floor[i].isEmpty && !isFind_First_Point_Empty)
                                            {
                                                isFind_First_Point_Empty = true;

                                                Player.ins.tf_Player.SetParent(floor_Raycast_To.list_Point_In_Floor[i].tf_Point_In_Floor);
                                                vec_pos_Can_Place_In_Floor = floor_Raycast_To.list_Point_In_Floor[i].tf_Point_In_Floor.position;
                                                Player.ins.Set_Floor_Indext_Point(floor_Raycast_To, i);
                                                Player.ins.Set_Block_Colider_Player();
                                                //Nếu có tâng nào đang sụp xuống thì Delay attack player
                                                Set_Check_State_Player_Do_Next(floor_Raycast_To, i, isDelayPlayer_attack_By_Downt);
                                                //SetParent vào điểm gắn để đi xuống cùng Floor nếu Floor sụp xuống
                                                Player.ins.tf_Player.localPosition = Vector3.zero;
                                                //Player.ins.Set_Pos_Old(Player.ins.tf_Player.position);
                                                Player.ins.Set_Fix_Pos_Player();
                                                //Debug.Log("99");
                                            }

                                        }
                                        else
                                        {
                                            Set_Pos_Old_Player();//
                                        }
                                    }
                                    #endregion
                                    
                                }
                            }
                            #region Nếu Raycast vào Floor Reward
                            else if (floor_Raycast_To.house_Build_Of_This.houseType == Enum_TypeHouse.enemy_Reward)
                            {
                                //Player.ins.Set_Block_Colider_Player();
                                Player.ins.tf_Player.SetParent(floor_Raycast_To.list_Point_In_Floor[0].tf_Point_In_Floor);
                                vec_pos_Can_Place_In_Floor = floor_Raycast_To.list_Point_In_Floor[0].tf_Point_In_Floor.position;
                                Player.ins.Set_Floor_Indext_Point(floor_Raycast_To, 0);
                                if (!floor_Raycast_To.isFloorOpenedReward)
                                {
                                    Player.ins.tf_Player.localPosition = Vector3.zero;
                                }
                                else
                                {
                                    Player.ins.tf_Player.position = floor_Raycast_To.list_Point_In_Floor[1].tf_Point_In_Floor.position;
                                }
                                Player.ins.Set_Block_Colider_Player();
                                //Nếu có tâng nào đang sụp xuống thì Delay attack player
                                Set_Check_State_Player_Do_Next(floor_Raycast_To, 0, isDelayPlayer_attack_By_Downt);
                                //SetParent vào điểm gắn để đi xuống cùng Floor nếu Floor sụp xuống
                                //if (!floor_Raycast_To.isFloorOpenedReward)
                                //{
                                //}

                                //Player.ins.Set_Pos_Old(floor_Raycast_To.list_Point_In_Floor[1].tf_Point_In_Floor.position);
                            }
                            #endregion

                        }



                    }
                    else
                    {
                        Set_Pos_Old_Player();//
                    }

                }
                else
                {
                    Set_Pos_Old_Player();
                }

            }
            //
            isChose_Player = false;
            isHold_Mouse = false;
            isHold_Mouse = false;
            floor_Light_First_Chose = null;
        }
        #endregion

    }
    #endregion
    #region Kiểm tra Player đang đứng trước cái gì để set hành động tiếp theo của n
    public void Set_Check_State_Player_Do_Next(Floor _floor, int _indexPoint, bool _isDelayPlayer_attack_By_Downt)
    {
        if (_floor.house_Build_Of_This.houseType != Enum_TypeHouse.enemy_Reward)
        {
            #region Nếu điểm trước mặt n chứa Enemy
            //đánh nhau với ENEMY
            if (_floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point != null)
            {
                if (Player.ins != null)
                {
                    Player.ins.enemy_Hitting = _floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point;
                }


                if (_isDelayPlayer_attack_By_Downt)
                {
                    StartCoroutine(IE_Player_Attack(_floor, _indexPoint));
                }
                else
                {
                    Check_Player_Attack_Or_Die(_floor, _indexPoint);
                }


            }
            #endregion
            #region  Nếu điểm trước mặt n chứa TRAP
            //Dẵm vào TRAP
            else if (_floor.list_Point_In_Floor[_indexPoint - 1].trap_Hit != null)
            {
                Player.ins.Delay_Hit_To_Idle();
                if (Player.ins.Get_Health() > (-_floor.list_Point_In_Floor[_indexPoint - 1].trap_Hit.health))
                {
                    Player.ins.Set_Anim_Hit();

                    //Giảm máu từ từ
                    Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.Get_Health(), Player.ins.Get_Health() + _floor.list_Point_In_Floor[_indexPoint - 1].trap_Hit.Get_Health(), Constant.Time_Player_Attack_idle);
                    Player.ins.Set_Add_Health(_floor.list_Point_In_Floor[_indexPoint - 1].trap_Hit.health);

                    _floor.list_Point_In_Floor[_indexPoint - 1].trap_Hit.Set_Destroy_This_Trap();
                    _floor.list_Point_In_Floor[_indexPoint - 1].ReSet_Enemy_Attach();

                    StartCoroutine(IE_Delay_Go_End_Trap_Priciple());
                }
                else
                {
                    Player.ins.Set_Anim_Die();
                }
            }
            #endregion
            #region  Nếu điểm trước mặt n chứa X2 X3 damege
            //X2 damge
            else if (_floor.list_Point_In_Floor[_indexPoint - 1].x2Damge != null)
            {
                
                Player.ins.Set_Un_Block_Colider_Player();
                    //Giảm máu từ từ
                    Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.Get_Health(), Player.ins.Get_Health() * _floor.list_Point_In_Floor[_indexPoint - 1].x2Damge.Get_Health(), Constant.Time_Player_Attack_idle);
                    Player.ins.Set_Add_Health((_floor.list_Point_In_Floor[_indexPoint - 1].x2Damge.health - 1)* Player.ins.Get_Health());

                    _floor.list_Point_In_Floor[_indexPoint - 1].x2Damge.Set_Destroy_This_Trap();
                    _floor.list_Point_In_Floor[_indexPoint - 1].ReSet_X2Attach();
                Player.ins.Set_Anim_TakeSword();
                    Player.ins.Set_Check_Reach_Last_Point_Level();
                floor_Raycast_Before = floor_Raycast_To;
            }
            #endregion
            #region  Nếu điểm trước mặt n chứa KIẾM tăng damge
            //Nhặt KIẾM
            else if (_floor.list_Point_In_Floor[_indexPoint - 1].sword_Attack_This_Point != null)
            {
                Player.ins.Set_Un_Block_Colider_Player();
                //UNDO: gắn KIẾM vào Player................

                Player.ins.Set_Anim_TakeSword();

                //Giảm máu từ từ
                Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.Get_Health(), Player.ins.Get_Health() + _floor.list_Point_In_Floor[_indexPoint - 1].sword_Attack_This_Point.Get_Health(), Constant.Time_Player_Attack_idle);
                Player.ins.Set_Add_Health(_floor.list_Point_In_Floor[_indexPoint - 1].sword_Attack_This_Point.health);
                //
                string skin_name = _floor.list_Point_In_Floor[_indexPoint - 1].sword_Attack_This_Point.Get_Skin_Name_By_Id_Sword();
                Player.ins.Set_Skin(skin_name);
                //
                _floor.list_Point_In_Floor[_indexPoint - 1].sword_Attack_This_Point.Set_Destroy_This_Sword();

                _floor.list_Point_In_Floor[_indexPoint - 1].ReSet_Enemy_Attach();
                if (_floor.is_Floor_Last_Of_house && _indexPoint == 1)
                {
                    //Set Victory
                    if (!_floor.is_Floor_Last_Of_Level)
                    {
                        if (_floor.is_Floor_Last_Of_house)
                        {
                            StartCoroutine(IE_Delay_Take_House(_floor, _indexPoint));
                        }

                    }
                    else
                    {
                        //Victory Level
                        //TOTEST: chạy win bên setAttack Player
                        ///////StartCoroutine(IE_Delay_Win(_floor, _indexPoint));
                        ///
                    }
                }
                Player.ins.Set_Check_Reach_Last_Point_Level();

                floor_Raycast_Before = floor_Raycast_To;
            }
            #endregion
            #region  Set Empty điểm Player đang đứng + Set Floor có thể sụp xuống
            //--- điểm Player đang đứng trên 1 Floor, nhưng Player ko bao h đến đc điểm cuối cùng của 1 Floor, vì điểm đó có lính, Boss hoặc Rương,...
            if (_indexPoint > 1)
            {
                _floor.list_Point_In_Floor[_indexPoint - 1].isEmpty = true;
            }

            //Nếu Player đang đứng là ngay trước điểm cuối của 1 Floor, có nghĩa là Floor này sẵn sàng sụp xuống nếu Player đc kéo đến Floor khác
            else if (_indexPoint - 1 == 0)
            {
                _floor.Set_isReady_Downt();
            }
            #endregion
        }
        else
        {
            if (_isDelayPlayer_attack_By_Downt)
            {
                StartCoroutine(IE_Delay_Go_End_Floor_Lv_Full_Reward(_floor));
            }
            else
            {
                if (!_floor.isFloorOpenedReward)
                {
                    _floor.isFloorOpenedReward = true;
                    Player.ins.Set_Take_Reward(_floor);
                }
                else
                {
                    Player.ins.tf_Player.position = _floor.list_Point_In_Floor[1].tf_Point_In_Floor.position;
                }
                
            }
            _floor.Set_isReady_Downt();
        }

    }
    //Quan
    IEnumerator IE_Delay_Go_End_Trap_Priciple()
    {
        yield return Cache.GetWFS(1);
        Player.ins.Set_Check_Reach_Last_Point_Level();
    }

    IEnumerator IE_Delay_Go_End_Floor_Lv_Full_Reward(Floor _floor)
    {
        yield return Cache.GetWFS(Constant.Floor_Time_Fall_Downt);
        if (!_floor.isFloorOpenedReward)
        {
            _floor.isFloorOpenedReward = true;
            Player.ins.Set_Take_Reward(_floor);
        }
        else
        {
            Player.ins.tf_Player.position = _floor.list_Point_In_Floor[1].tf_Point_In_Floor.position;
        }
        
    }
    #endregion
    #region Delay Mái nhà chuyển xanh, di chuyển camera sang nhà mới
    public void Set_Delay_Take_House(Floor _floor, int _indexPoint)
    {
        //Set_Take_New_House(_floor);
        StartCoroutine(IE_Delay_Take_House(_floor, _indexPoint));
    }
    IEnumerator IE_Delay_Take_House(Floor _floor, int _indexPoint)
    {
        //ko dùng đến biến _indexPoint
        
        yield return Cache.GetWFS(Constant.Time_Delay_move_to_next_house );
        Set_Take_New_House(_floor);
        

        //Nếu điểm camera di chuyển chưa tới điểm cuối của các tòa nhà nếu Level chứa nhiều tào nhà
        //if (index_Pos_Cam_Move_In_List < number_House)
        //{
        //    Camera_Manager.Ins.Move_Cam(index_Pos_Cam_Move_In_List);
        //    index_Pos_Cam_Move_In_List++;
        //}
    }
    #region Đổi màu mái nhà
    public void Set_Take_New_House(Floor _floor)
    {
        //_floor.Set_Floor_To_Floor_Of_Player();
        ////_floor.house_Build_Of_This.Set_Mai_Xanh();
        //_floor.house_Build_Of_This.Set_This_To_Team_Player();
        //~~~~
        if (index_Pos_Cam_Move_In_List < number_House)
        {
            Camera_Manager.Ins.Move_Cam(index_Pos_Cam_Move_In_List);
            index_Pos_Cam_Move_In_List++;
        }


        ((CanvasGamePlay)UIManager.Ins.GetUI(UIID.UICGamePlay)).Set_Active_Castle_Each_Time_Chiem_Duoc(index_House_Chiem_Duoc);
        index_House_Chiem_Duoc++;
    }
    #endregion

    #endregion 
    #region Set Delay hành động Attack của Player, nếu Floor đang sụp xuống
    IEnumerator IE_Player_Attack(Floor _floor, int _indexPoint)
    {
        //yield return new WaitForSeconds(Constant.Floor_Time_Fall_Downt);
        yield return Cache.GetWFS(Constant.Floor_Time_Fall_Downt);
        Check_Player_Attack_Or_Die(_floor, _indexPoint);
        _floor.list_Point_In_Floor[_indexPoint - 1].ReSet_Enemy_Attach();
    }

    #region Check Player Attack Or Die
    public void Check_Player_Attack_Or_Die(Floor _floor, int _indexPoint)
    {
        //Nếu Enemy sắp đánh đang ko bật anim chít
        if (!_floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point.isDieing)
        {

            //Đánh dấu n đang bật anim chít để ko bị đánh tiếp
            _floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point.Set_BoolIsDieing();
            if (!_floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point.isBoss_UNTIL)
            {

                #region Nếu Máu Player nhiều hơn sẽ thắng..bật Player THẮNG...Enemy THUA
                if (Player.ins.Get_Health() > _floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point.Get_Health())
                {
                    #region Kích hoạt Player tấn công
                    Player.ins.Set_Attack();
                    #endregion

                    //Cho Health Bar chạy tăng máu
                    Player.ins.health_Bar.Set_Step_By_Step_Health(Player.ins.Get_Health(), Player.ins.Get_Health() + _floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point.Get_Health(), Constant.Time_Player_Attack_idle);

                    //Sau khi set Health Bar mới được gắn Máu thật tăng lên cho Player
                    Player.ins.Set_Add_Health(_floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point.Get_Health());

                    #region Kích hoạt Enemy bị đánh Die
                    //Kích hoạt Enemy bị đánh Die
                    _floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point.Set_Anim_Die();
                    #endregion

                    //Check Last_Of_house, và tầng cuối cùng đánh bại, tầng này được đánh dấu sắn bằng biến bool và được GD tính toán các máu các Enemy sao cho tất cả các trường hợp thì tâng này phải là tầng cuối cùng Player mới đến được, nếu không sẽ thua ở các lần đặt khác
                    if (_floor.is_Floor_Last_Of_house && _indexPoint == 1)
                    {
                        //Set Victory
                        if (!_floor.is_Floor_Last_Of_Level)
                        {
                            //StartCoroutine(IE_Delay_Take_House(_floor, _indexPoint));

                        }
                        else
                        {
                            //Victory Level
                            //TOTEST: chạy win bên setAttack Player
                            ///////StartCoroutine(IE_Delay_Win(_floor, _indexPoint));
                            ///
                        }
                    }
                }
                #endregion
                #region  Nếu Máu Player ÍT hơn sẽ thua..bật Player THUA...Enemy THẮNG
                else
                {
                    Player.ins.Set_Anim_Die();
                    _floor.list_Point_In_Floor[_indexPoint - 1].enemy_Attack_This_Point.Set_Attack();
                }
                #endregion

            }
            else//Show camera đánh Boss
            {
                Player.ins.is_Block_Raycas = true;
                UIManager.Ins.OpenUI(UIID.UICFight_Boss);
                Camera_Manager.Ins.SetMove_To_Floor_Boss(_floor.tf_Floor);
            }
        }

        floor_Raycast_Before = floor_Raycast_To;
    }
    #endregion

    #endregion

    #region Raycast 1 tia từ camera qua chuột, Lấy các Floor bị tia Raycast bắn vào
    public void Set_Check_Raycast_Floor()
    {
        //**** lấy từ Raycast
        RaycastHit[] hits = new RaycastHit[6];//số phần tử bằng với hàm Get_Raycast()
        hits = Get_Raycast();
        for (int i = 0; i < hits.Length; i++)
        {
            //Debug.Log(hits[i].collider.gameObject.name);
            if (hits[i].collider != null)
            {
                //Floor
                if (Cache.Get_Floor_Script_From_Colider(hits[i].collider) != null)
                {
                    floor_Raycast_To = Cache.Get_Floor_Script_From_Colider(hits[i].collider);
                    return;
                }
            }
        }
        floor_Raycast_To = null;
    }
    #endregion
    #region  Raycast 1 tia từ camera qua chuột lúc Click, Lấy Player và Floor Player đang đứng bị tia Raycast bắn vào
    public void Set_Check_Raycast_Colider3D_Player()
    {
        //**** lấy từ Raycast
        RaycastHit[] hits = new RaycastHit[6];//số phần tử bằng với hàm Get_Raycast()
        hits = Get_Raycast();
        for (int i = 0; i < hits.Length; i++)
        {
            //Debug.Log(hits[i].collider.gameObject.name);
            if (hits[i].collider != null)
            {
                if (Cache.Get_Colider3D_Player_Script_From_Colider(hits[i].collider) != null)
                {
                    Player.ins.Set_Scale_Bigger();
                    isChose_Player = true;
                }
                //Floor
                if (Cache.Get_Floor_Script_From_Colider(hits[i].collider) != null)
                {
                    //khởi tạo Floor mỗi lần chuột đi qua
                    floor_Raycast_To = Cache.Get_Floor_Script_From_Colider(hits[i].collider);

                    //Light Blue ở các Floor
                    floor_Light_First_Chose = Cache.Get_Floor_Script_From_Colider(hits[i].collider);
                }
            }
        }

    }
    #endregion
    #region Refresh lại vị trí Player đứng trên Floor, để khi kéo Player ra ngoài mà ko trúng Floor nào sẽ quay lại vị trí này
    public void Set_Pos_Old_Player()//set vị trí cũ sau thả chuột
    {
        Player.ins.Set_Un_Block_Colider_Player();
        Player.ins.Set_vi_tri_cu();
    }
    #endregion
    #region Lấy mọi colider mà 1 tia Racast từ camera bắn qua chuột, cho vào Array có 6 phần tử
    public RaycastHit[] Get_Raycast()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = new RaycastHit[6];
        //nếu raycast vào gameObject
        int layerMask = 1 << 0;//TODO: dịch bit, (chon giá trị có thể Raycast của Layer index là 6) = 1 ở trong danh sách các layer (layerMask), (các layer còn lại trong layerMask có giá trị Raycast) = 0
        Physics.RaycastNonAlloc(ray, hits, 1000, layerMask);
        return hits;
    }
    #endregion


    #region Nếu Click vào Player thì bật sáng các Floor khác ngoại trừ Floor Player đang đứng
    public void Set_Check_Light_Downt_Up_Click()
    {
        #region Nếu Click vào Player thì bật sáng các Floor khác ngoại trừ Floor Player đang đứng
        //Hàm này chạy sau khi đã xác định được click vào Player
        if (Input.GetMouseButtonDown(0))
        {
            if (isChose_Player)
            {
                Set_Active_Stroke();
            }
        }
        #endregion
        #region Tắt Khung xanh khi thả chuột ra
        if (Input.GetMouseButtonUp(0))
        {
            Set_DeActive_Stroke();
            //Thả chuột ra reset lại floor_Light
            floor_Light_Current = null;
            floor_Light_Before = null;
        }
        #endregion
        if (isChose_Player)
        {
            Set_Check_Raycast_Floor_Light_Blue();
        }
    }

    #region Bật các khung xanh ở các Floor, ngoại trừ Floor Player đang đứng
    //active các khung xanh ở các Floor
    public void Set_Active_Stroke()
    {
        for (int i = 0; i < list_House_Build.Count; i++)
        {
            //active các floor của Enemy trừ Floor chưa Player lúc bắt đầu click vào
            //Nếu là Normal housse
            if (list_House_Build[i].houseType == Enum_TypeHouse.enemy_Normal)
            {
                //Debug.Log(list_House_Build[i].list_Floor.Count);
                for (int j = 0; j < list_House_Build[i].list_Floor.Count; j++)
                {
                    list_House_Build[i].list_Floor[j].Get_stroke_Blue().Set_Light_Out_Side();
                }
                Player.ins.floor_stay.Get_stroke_Blue().Set_Light_Out_Side();
            }
            //Nếu là Tree House
            else if (list_House_Build[i].houseType == Enum_TypeHouse.enemy_Tree)
            {
                //Debug.Log(list_House_Build[i].list_Floor.Count);
                for (int j = 0; j < list_House_Build[i].list_Floor.Count; j++)
                {
                    if ( !list_House_Build[i].list_Floor[j].Get_isEmpty_Floor_Tree() && !list_House_Build[i].list_Floor[j].isLocking)
                    {

                        list_House_Build[i].list_Floor[j].Get_stroke_Blue().Set_Light_Out_Side();
                    }
                    else
                    {
                        list_House_Build[i].list_Floor[j].Get_stroke_Blue().Set_Light_off();
                    }
                }
                Player.ins.floor_stay.Get_stroke_Blue().Set_Light_Out_Side();
            }
        }
    }

    #endregion
    #region  Tăt các khung xanh ở các Floor Enemy
    public void Set_DeActive_Stroke()
    {
        //Xét các nhà ở lần đầu tiên kéo Playr từ nhà n sang nhà khác
        for (int i = 0; i < list_House_Build.Count; i++)
        {
            //xét floor của từng nhà
            for (int j = 0; j < list_House_Build[i].list_Floor.Count; j++)
            {

                if (list_House_Build[i].houseType != Enum_TypeHouse.player)
                {
                    list_House_Build[i].list_Floor[j].Get_stroke_Blue().Set_Light_off();
                }
            }
        }
        
    }
    #endregion


    public void Set_Check_Raycast_Floor_Light_Blue()
    {
        //**** lấy từ Raycast
        bool _is_Raycast_To_Floor = false;
        bool _is_Raycast_To_Fist_Floor = false;
        RaycastHit[] hits = new RaycastHit[6];//số phần tử bằng với hàm Get_Raycast()
        hits = Get_Raycast();
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null)
            {
                //chỉ trúng 1 Floor nào đó
                if (Cache.Get_Floor_Script_From_Colider(hits[i].collider) != null)
                {
                    _is_Raycast_To_Floor = true;
                    ////chỉ trúng 1 Floor Enemy
                    if (Cache.Get_Floor_Script_From_Colider(hits[i].collider).house_Build_Of_This.houseType != Enum_TypeHouse.player)
                    {
                        ////chỉ trúng 1 Floor Enemy mà khác với Floor Enemy đứng mỗi lần thả chuột
                        if (Cache.Get_Floor_Script_From_Colider(hits[i].collider) != floor_Light_First_Chose)
                        {
                            // vì kéo ra khỏi khu vực Floor của enemy đã set floor_Light_Current,floor_Light_Before = null rồi nên khi kéo vào lần đầu tiên vào floor_Enemy( khác floor lúc đầu tiên bắt đầu click vào player) sẽ cho sáng Center luôn
                            //Lần đâu tiên click chuột vào Floor player đang đứng ở phía nhà Enemy hoặc lần đầu tiên di chuột từ bên ngoài vào Floor Enemy, hoặc lần đầu tiên di chuột từ ô Enemy khác sang Floor mà Player đứng đầu tiên trong lúc playẻ đang được kéo
                            #region 

                            #endregion
                            if (floor_Light_Current == null && floor_Light_Before == null)
                            {
                                floor_Light_Current = Cache.Get_Floor_Script_From_Colider(hits[i].collider);

                                if (!floor_Light_Current.Get_isEmpty_Floor_Tree())
                                {
                                    floor_Light_Current.Get_stroke_Blue().Set_Light_Center();
                                }
                            }
                            floor_Light_Current = Cache.Get_Floor_Script_From_Colider(hits[i].collider);

                            if (floor_Light_Before == null)
                            {
                                floor_Light_Before = Cache.Get_Floor_Script_From_Colider(hits[i].collider);
                            }
                            else
                            {
                                if (!floor_Light_Before.Get_isEmpty_Floor_Tree())
                                {
                                    
                                    if (floor_Light_Current != floor_Light_Before)
                                    {
                                        //Debug.Log(floor_Light_Current.gameObject.name);
                                        if (!floor_Light_Current.Get_isEmpty_Floor_Tree())
                                        {
                                            floor_Light_Current.Get_stroke_Blue().Set_Light_Center();
                                        }
                                        if (floor_Light_Before != floor_Light_First_Chose)
                                        {
                                            
                                            floor_Light_Before.Get_stroke_Blue().Set_Light_Out_Side();

                                        }
                                    }
                                    
                                    floor_Light_Before = Cache.Get_Floor_Script_From_Colider(hits[i].collider);
                                }
                            }
                        }
                        if (Cache.Get_Floor_Script_From_Colider(hits[i].collider) == floor_Light_First_Chose)
                        {
                            _is_Raycast_To_Fist_Floor = true;
                        }
                        
                    }
                }
            }
        }
        is_Raycast_To_Floor = _is_Raycast_To_Floor;
        is_Raycast_To_First_Floor = _is_Raycast_To_Fist_Floor;
        //
        #region bật chỉ sáng viền các Floor nếu kéo Player ra ngoài các tòa nhà
        if (!is_Raycast_To_Floor)
        {
            Reset_floor_Light();
        }
        else if (is_Raycast_To_First_Floor)
        {
            Reset_floor_Light();
        }
        #endregion

    }
    public void Reset_floor_Light()
    {
        if (floor_Light_Before != null)
        {
            if (!floor_Light_Before.Get_isEmpty_Floor_Tree())
            {
                
                floor_Light_Before.Get_stroke_Blue().Set_Light_Out_Side();
            }
        }
        floor_Light_Current = null;
        floor_Light_Before = null;
    }
    #endregion

    #region State Machine---Not Use-------------
    public void ChangeState(IState<Drag_Drop_Manager> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    #endregion
}
/*


























State
    Hold
        


Player
    Colider
        Raycast 
           Hold >>> x Anim 1.2 

Drag_Drop_Manager
    Box:  colider,
        Box_Player
            1 ô có thể chèn từ tầng 1 lên

        Box_Enemy
            Có các điểm để xếp Enemy + điểm
                Các điểm đánh số 




Click chuột
    Raycast 
            Hold 
            Có Vào ô Enemy 
                Xanh lõi ô




            -------------------------------------------
            Có Vào Player 
                
                Hold >>> x Anim 1.2 
                vào ô nào
                    ô có colider
                    Trong ô thì vào điểm nào
                Thả tay
                    Check nếu vào 1 ô địch
                        Có vào ô địch
                            
                        Không vào ô địch

                        về ô ban đầu gần nhất đã đứng
            Không_vào PLYer

Check Raycast Player
    Nếu giữ 
/////////////////////////////////////

Drag_Drop_Manager

    *** Free_State
            Check input mouse + hold vào PLYER
                Check PLYER >>> can_Hold_Player
                    Change state ( Hold_Player_Mouse_State )
            

    *** Hold_Player_Mouse_State
            Cho Player TO ra
            Update vị trí Player theo chuột 
            Check_Raycast_Floor
                
            Check thả chuột
                Nếu CÓ vào ô tag_Floor
                    Tìm index ô trống NHỎ nhất
                        truyền vào Set_Pos_Player---vị trí  này
                            tf_Stay_Now = indexPos_here
                        truyền vào Set_Pos_Player---vị trí  này
                    Tìm index kề Player chứa Enemy = player -1( vì chắc chắn có Enemy mới thả vào đc... nếu ko n đã bị sụp xuống )
                        Check level Player và Enemy
                        Delay
                            Anim_idle:  Player - nearest Enemy
                                0.5s
                                    Anim_attack:  Player - nearest Enemy
                                        1s
                                           Nếu Level Player > Enemy 
                                                Anim_Die:  nearest Enemy
                                                Anim_Idle:  Player
                                                Anim_health: bắn ra
                                                    0.6s
                                                        Check_Reward();
                                                            Nếu CÓ hòm Reward ở cuối
                                                                Chạy đến điểm Reward
                                                                    Anim Player mở
                                                                    Anim Hòm mở
                                                                        0.7s
                                                                            Nếu Đã hết Enemy
                                                                                Change state ( Victory )
                                                                            Nếu CHƯA hết Enemy
                                                                                Change state ( Free_State )
                                                            Nếu KHÔNG có hòm Reward ở cuối
                                                            Change state ( Free_State )
                                           Nếu Level Player < Enemy 
                                                Anim_Die:  Player
                                                Anim_Idle:  nearest Enemy
                                                Anim_health: bắn ra
                                                    Change state ( Free_State )
                Nếu KHÔNG vào ô Enemy
                    Cho Player về vị trí tại thời điểm gần nhất n đứng
                        tf_Stay_Now.position
                            Change state ( Free_State )

/////////////////////////////////////
Player 
    Idle_State
    
    
    Attack_State



    Die_State




    Victory_State




/////////////////////////////////////
Enemy
    Idle_State
    
    
    Attack_State



    Die_State




    Victory_State
/////////////////////////////////////
House
    House_Player
        Mái xanh+mái đỏ>>> On Off
        -----------------------
        List các tầng  list_Floor<>;
            Floor
                list_Enemy<>
                list_Point_Container<>
                bool is_Have_Player
        -----------------------
        Sinh ra 1 tầng tại vị trí gốc--của nhà
        Tăng lên 1 tầng từ dưới lên
            Tầng thêm:
                        DoScale tâng mới (0~1) +              |  trong cùng 1 khoảng thời gian
                        DoMove lên vị trí = 0.5 lần chiều cao |  trong cùng 1 khoảng thời gian
            Các tầng còn lại
                Domove(lên chiều cao của 1 tẩng)              |  trong cùng 1 khoảng thời gian

        -----------------------

        Có các tầng
            Mỗi tầng có các điểm chứa
                Mỗi điểm chứa có Player ~ hoặc Enemy
                Enemy hoặc Player --Chít
                    Remove Enemy hoặc Player khỏi điểm
                        Điểm update Enemy lên tầng-tâng có list_Enemy
                        Floor
                            list_Enemy<>
                            list_Point_Container<>
                
                            Tâng update số tầng lên nhà
                                Nếu nhà chỉ còn 1 tầng và các điểm ko chứa cái gì nữa
                                    Player Win
    House_Enemy
        Function Sụp_Xuống(){}
        Tầng rỗng bị sụp xuống
            các tâng phải có Index
                Nếu 1 tầng sụp xuống                         |  trong cùng 1 khoảng thời gian
                    DoScale nhỏ đi (1~0) +                   |  trong cùng 1 khoảng thời gian
                    DoMove xuống vị trí = 0.5 lần chiều cao  |  trong cùng 1 khoảng thời gian
                        rồi biến mất


                    Các tầng cao hơn( có index > hơn) sẽ
                    House.Floor
                        Domove(xuống chiều cao của 1 tẩng)   |  trong cùng 1 khoảng thời gian



        //if (Physics.RaycastNonAlloc(ray, hits, 1000, layerMask) > 0)
        //{// lưu số Colider ray cast trúng
        //    //Get object with min distance to camera
        //    //Debug.Log("RaycastObjects");
        //    for (int i = 0; i < hits.Length; i++)
        //    {
        //        if (hits[i].collider != null)
        //        {
        //            Debug.Log(hits[i].collider.gameObject.name);
        //        }
        //    }
        //}

/*
                                        Player.ins.tf_Player.DOMove(floor_Raycast_To.list_Point_In_Floor[i].tf_Point_In_Floor.position, Constant.Player_Time_To_Point)
                                            .OnComplete(() =>
                                            {
                                                Vector3 _vec = new Vector3(floor_Raycast_To.list_Point_In_Floor[i].tf_Point_In_Floor.position.x, floor_Raycast_To.list_Point_In_Floor[i].tf_Point_In_Floor.position.y, floor_Raycast_To.list_Point_In_Floor[i].tf_Point_In_Floor.position.z);
                                                //Player.ins.Set_Pos_Old(_vec);

                                            });
                                        
public void Set_Win_Level(Floor _floor)
    {
        _floor.house_Build_Of_This.Set_Mai_Xanh();
        _floor.house_Build_Of_This.Set_This_To_Team_Player();
        //Fire work
        GameObject obj = (GameObject)Instantiate(Resources.Load(Constant.Path_Frefab_fx_win_ball), Player.ins.tf_Player.position, Player.ins.tf_Player.rotation);
        
    }


    //OnEnter

    //OnExecute






    //*********************************************************

    //*********************************************************
    //TOTEST
    /*
    IEnumerator IE_Delay_Win(Floor _floor, int _indexPoint)
    {
        yield return Cache.GetWFS(Constant.Time_Player_Die_attack);
        GameManager.Ins.Set_Win_Level(_floor);
    }
    */
//*********************************************************

/* ko dùng, vì đã gọi ở Game Manager
IEnumerator IE_Delay_Lose(Floor _floor, int _indexPoint)
{
    yield return Cache.GetWFS(Constant.Time_Player_Die_attack);
    GameManager.Ins.Set_Lose_Level(_floor);
}
*/




//*********************************************************


//*********************************************************

