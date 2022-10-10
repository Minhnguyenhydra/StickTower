using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Scene_Manager_Q 
{
    public static void Load_Scene(string _name_Scene)
    {
        SceneManager.LoadScene(_name_Scene, LoadSceneMode.Single);
    }
}

/*

1 1 quái nhỏ
1 1 BOSS

1 2 Gai cuu cc




















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


-đổi màu mai
-cái Floor được đánh dấu là Last
    khi đã đánh hết index1
        Tự động đến cuối cùng
            Trong mọi state 
                Check index cuối cùng
                    point cuối cùng của Floor cuối cùng sẽ có tf_điểm để đến
                        Player sẽ đến đó lúc cuối
                            domove.onComlete >> anim mở hòm hoặc state
                             chỉ cẩn domove rồi check State tiếp theo giống lúc thả chuột
                                bật anim thắng thua
                                Firework                
                                     load Scene thắng, thua       

SceneManager___dont destroyOnload

CanvasFader
thanh điểm chạy
Bug: kéo đc vào Floor đầu tiên của Player

Chuyển tiếp giữa các nhà
Point có gắn: Script  giải cứu CC

































*/