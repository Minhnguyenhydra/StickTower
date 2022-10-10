using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_0_Game_Play : Singleton<Tut_0_Game_Play>
{
    public Animator anim;
    public Enum_Anim_0 enum_anim_0;
    public int index = 0;
    public bool isfff;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        SetCheck_State();
    }
    public void SetCheck_State()
    {
        int ii = Drag_Drop_Manager.Instance.index_Step_Tut_0_use;

        if (ii == -1)
        {
            SetChange_State(Enum_Anim_0.anim_03);
        }
        if (ii == 1)
        {
            SetChange_State(Enum_Anim_0.anim_02);
        }
        if (ii == 2)
        {
            SetChange_State(Enum_Anim_0.anim_03);
        }
    }
    public void SetChange_State(Enum_Anim_0 new_State)
    {
        if (new_State != enum_anim_0)
        {
            enum_anim_0 = new_State;
            if (new_State == Enum_Anim_0.anim_01)
            {
                Set_Tut_0_1();
            }
            else if (new_State == Enum_Anim_0.anim_02)
            {
                Set_Tut_0_2();
            }
            else if (new_State == Enum_Anim_0.anim_03)
            {
                Set_Tut_0_3_Off();
            }
        }
    }
    public void Set_Tut_0_2()
    {
        anim.ResetTrigger(Constant.Trigger_Tut_0_1);
        anim.SetTrigger(Constant.Trigger_Tut_0_2);
        if (index == 3)
        {
            Destroy(this.gameObject);
        }
        index++;
    }
    public void Set_Tut_0_1()
    {
        anim.SetTrigger(Constant.Trigger_Tut_0_1);
    }
    
    public void Set_Tut_0_3_Off()
    {
        anim.ResetTrigger(Constant.Trigger_Tut_0_1);
        anim.SetTrigger(Constant.Trigger_Tut_0_3);
        index++;
    }

}
