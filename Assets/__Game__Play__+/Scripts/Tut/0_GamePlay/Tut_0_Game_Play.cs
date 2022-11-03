using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_0_Game_Play : Singleton<Tut_0_Game_Play>
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject objHand;

    private Enum_Anim_0 enum_anim_0;
    private Enum_Anim_0 lastAnim;
    private int index = 0;


    private void Start()
    {
        lastAnim = Enum_Anim_0.animLevel1_0;
    }
    private void Update()
    {
        SetCheck_State();
    }
    public void SetCheck_State()
    {
        int ii = Drag_Drop_Manager.Instance.index_Step_Tut_0_use;
        if (ii == -1 || ii == 2)
            SetChange_State(Enum_Anim_0.anim_03);

    }
    public void SetChange_State(Enum_Anim_0 new_State)
    {
        if (new_State != enum_anim_0)
        {
            if (new_State == Enum_Anim_0.anim_01)
                Set_Tut_0_1();
            
            else if (new_State == Enum_Anim_0.anim_03)
                Set_Tut_0_3_Off();
        }
    }
    public void Set_Tut()
    {
        anim.ResetTrigger(Constant.Trigger_Tut_0_3);

        int level = PlayerPrefs_Manager.Get_Index_Level_Normal();
        if (level == 0)
        {
            anim.SetTrigger(Constant.Trigger_Tut_0_2);
            enum_anim_0 = Enum_Anim_0.anim_02;

            return;
        }

        if (lastAnim == Enum_Anim_0.animLevel1_0)
        {
            enum_anim_0 = Enum_Anim_0.animLevel1_1;
            lastAnim = Enum_Anim_0.animLevel1_1;
            anim.SetTrigger(Constant.AnimLevel1_1);
        }
        else if (lastAnim == Enum_Anim_0.animLevel1_1)
        {
            enum_anim_0 = Enum_Anim_0.animLevel1_2;
            lastAnim = Enum_Anim_0.animLevel1_2;
            anim.SetTrigger(Constant.AnimLevel1_2);
        }
        else if (lastAnim == Enum_Anim_0.animLevel1_2)
        {
            enum_anim_0 = Enum_Anim_0.animLevel1_4;
            lastAnim = Enum_Anim_0.animLevel1_4;
            StartCoroutine(WaitResetPos());
        }
    }

    private IEnumerator WaitResetPos()
    {
        anim.SetTrigger(Constant.AnimLevel1_4);
        objHand.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        objHand.SetActive(true);
    }

    public void Set_Tut_0_1()
    {
        if (index > 0)
        {
            Set_Tut_0_3_Off();
            return;
        }
        enum_anim_0 = Enum_Anim_0.anim_01;
        lastAnim = Enum_Anim_0.anim_01;
        anim.SetTrigger(Constant.Trigger_Tut_0_1);
    }

    public void Set_Tut_0_3_Off()
    {
        enum_anim_0 = Enum_Anim_0.anim_03;
        anim.SetTrigger(Constant.Trigger_Tut_0_3);

        int level = PlayerPrefs_Manager.Get_Index_Level_Normal();
        if (index == 1 && level == 0)
            gameObject.SetActive(false);
        
        else if (index == 3 && level == 1)
            gameObject.SetActive(false);

        index++;
    }
}
