using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Key_3_Manager : Singleton<Key_3_Manager>
{
    public int number_Key_Have;
    public Transform tf_1_key_01;
    public List<Transform> list_Tf_3key;
    public List<GameObject> list_obj_3key_gold;
    public List<GameObject> list_obj_3key_Gray;
    public List<SpriteRenderer> list_Sprite_To_Fade;
    public GameObject obj_To_Hide_6_Image_Key;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            list_obj_3key_gold[i].SetActive(false);
            list_obj_3key_Gray[i].SetActive(false);
        }
    }
    [ContextMenu("TEST1")]
    private void Set_Config_Active()
    {
        number_Key_Have = PlayerPrefs_Manager.Get_Number_Key_Treasure();
        number_Key_Have = Mathf.Clamp(number_Key_Have, 0, 2);
        for (int i = 0; i < 3; i++)
        {
            list_obj_3key_gold[i].SetActive(false);
            list_obj_3key_Gray[i].SetActive(true);
        }
        if (number_Key_Have == 1)
        {
            list_obj_3key_gold[0].SetActive(true);
        }
        else if (number_Key_Have == 2)
        {
            list_obj_3key_gold[0].SetActive(true);
            list_obj_3key_gold[1].SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform Get_Tf_Key_Go()
    {
        return list_Tf_3key[number_Key_Have];
    }
    
    public void Set_Fade()
    {
        for (int i = 0; i < list_Sprite_To_Fade.Count; i++)
        {
            list_Sprite_To_Fade[i].DOFade(1, 0.5f);
        }
    }
    [ContextMenu("TEST")]
    public void Set_Key_Fly()
    {
        Set_Config_Active();
        obj_To_Hide_6_Image_Key.SetActive(true);
        tf_1_key_01.DOMove(list_Tf_3key[number_Key_Have].position, Constant.Time_Key_Fly).OnComplete(() => {
            tf_1_key_01.DOScale(1, Constant.Time_Key_Fly).OnComplete(() => {
               
                SoundManager.Ins.PlayFx(FxID.collect_coin);
                for (int i = 0; i < list_Sprite_To_Fade.Count; i++)
                {
                    list_Sprite_To_Fade[i].DOFade(0, 1f);
                }
                tf_1_key_01.GetComponent<SpriteRenderer>().DOFade(0, 1f);
            });
        }); 
        ;
    }
    [ContextMenu("TEST--Debug key--")]
    public void Set_sfgdsy()
    {
        //Debug.Log(PlayerPrefs_Manager.Get_Number_Key_Treasure());
    }
    [ContextMenu("TEST--Set_ResetKey")]
    public void Set_ResetKey()
    {
        PlayerPrefs_Manager.Set_Number_Key_Treasure(0);
    }
}
