using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Health_Bar_Boss_Manager : Singleton<Health_Bar_Boss_Manager>
{
    public Image img_Health_Bar_Boss;
    public bool _is_2_thanh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set_Fill_Health_Bar_Boss(bool _is_Player_Win,float _time_fill)
    {
        if (_is_Player_Win)
        {
            if (!_is_2_thanh)
            {
                img_Health_Bar_Boss.DOFillAmount(1, _time_fill);

            }
            else
            {
                img_Health_Bar_Boss.DOFillAmount(0.515f, _time_fill/2).OnComplete(()=> { 
                    img_Health_Bar_Boss.DOFillAmount(0.515f, 0.7f).OnComplete(()=> {
                        img_Health_Bar_Boss.DOFillAmount(1, _time_fill / 2 - 0.7f);
                    });
                    
                });
            }
        }
        else
        {
            img_Health_Bar_Boss.DOFillAmount(0.75f, _time_fill);
        }
    }
    public void Set_Destroy_Health_Bar_Boss()
    {
        Destroy(this.gameObject);
    }
}
