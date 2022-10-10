using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ar_Manager : MonoBehaviour
{
    public static Ar_Manager ins;
    public List<Enemy_Ar> list_Enemy_Ar;
    public int index_Enemy_Current;
    public bool is_Move_Complete;
    [Header("State Machine")]
    private IState<Ar_Manager> currentState;
    //
    public Transform tf_Target_Enemy;
    public Transform tf_Target_Player;
    public float time_Enemy_Move;
    // Start is called before the first frame update
    void Start()
    {
        if (ins == null)
        {
            ins = this;
        }
        //ChangeState(new State_Idle());
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentState != null)
        //{
        //    currentState.OnExecute(this);
        //}
    }

    //public void ChangeState(IState<Ar_Manager> state)
    //{
    //    if (currentState != null)
    //    {
    //        currentState.OnExit(this);
    //    }

    //    currentState = state;

    //    if (currentState != null)
    //    {
    //        currentState.OnEnter(this);
    //    }
    //}

    //public void Set_Move_Enemy()
    //{
    //    list_Enemy_Ar[index_Enemy_Current].tf_Enemy_Ar.DOMove(tf_Target_Enemy.position, time_Enemy_Move).OnComplete(() =>
    //        list_Enemy_Ar[index_Enemy_Current].Set_Attack()
    //    );
    //}

    


}
/*
 










Player: đi ra 1 điểm 
    Enemy: đi ra 1 điểm 
        Fight
            Player win
                Enemy + 1: đi ra 1 điểm 
                    Fight
                        Player win
                            Enemy + 2: đi ra 1 điểm 
                                Fight
                                    .................................
                                    .................................
                                    .................................
                                    .................................
                                    .................................
                                    .................................
                        Player Lose
                            

            Player Lose
                Canvas Lose

    
    
*/