using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BgScaleFitScreenSize_CanvasWin : MonoBehaviour
{
    
    public Screne_Size _screne_Size_current;
    public Screne_Size _screne_Size_last;
    
    public RectTransform rect_Table_Level;
    public RectTransform rect_Title_YouWin;
    public RectTransform rect_Roll_bar;
    public RectTransform rect_Claim_ADs;
    public RectTransform rect_Claim_no_ADs;
    public RectTransform rect_NoThank;
    public RectTransform rect_Gold_Gem_Boot;
    //
    public Vector3 vec_rect_Table_Level_720;
    public Vector3 vec_rect_Title_YouWin_720;
    public Vector3 vec_rect_Roll_bar_720;
    public Vector3 vec_rect_Claim_ADs_720;
    public Vector3 vec_rect_Claim_no_ADs_720;
    public Vector3 vec_rect_NoThank_720;
    public Vector3 vec_rect_Gold_Gem_Boot_720;
    //
    public Vector3 vec_rect_Table_Level_1024;
    public Vector3 vec_rect_Title_YouWin_1024;
    public Vector3 vec_rect_Roll_bar_1024;
    public Vector3 vec_rect_Claim_ADs_1024;
    public Vector3 vec_rect_Claim_no_ADs_1024;
    public Vector3 vec_rect_NoThank_1024;
    public Vector3 vec_rect_Gold_Gem_Boot_1024;
    private void Awake()
    {
       
        vec_rect_Table_Level_720 = new Vector3( -2, -284, 0);
        vec_rect_Title_YouWin_720 = new Vector3( -13, 425, 0);
        vec_rect_Roll_bar_720 = new Vector3( 0,-659 , 0);
        vec_rect_Claim_ADs_720 = new Vector3( 0, -833,0 );
        vec_rect_Claim_no_ADs_720 = new Vector3( 0, -833,0 );
        vec_rect_NoThank_720 = new Vector3( 0,-1001 , 0);
        vec_rect_Gold_Gem_Boot_720 = new Vector3( 0,-501 , 0);


        vec_rect_Table_Level_1024 = new Vector3( -2,-270 , 0);
        vec_rect_Title_YouWin_1024 = new Vector3( -13,364 , 0);
        vec_rect_Roll_bar_1024 = new Vector3(0 , -562, 0);
        vec_rect_Claim_ADs_1024 = new Vector3( 0, -730, 0);
        vec_rect_Claim_no_ADs_1024 = new Vector3( 0, -730, 0);
        vec_rect_NoThank_1024 = new Vector3( 0,-880 , 0);
        vec_rect_Gold_Gem_Boot_1024 = new Vector3( 0,-479 , 0);
        _screne_Size_last = Screne_Size.Nothing;
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        checkScreen();
    }
   
    public void Set_Change_Scene_Size()
    {

    }
    public void checkScreen()
    {
        if (Screen.width == 1536 && Screen.height == 2048)//ipad 9.7 Portrait dọc
        {
            _screne_Size_current = Screne_Size.Doc_1024;
            if (_screne_Size_last != _screne_Size_current)
            {
                Set_Doc_1024();
            }
    //cam.orthographicSize = 75;
}
        else if (Screen.width == 2048 && Screen.height == 1536)// ipad 9.7 Landscaspe ngang
        {
            //Ko để màn ngang nên không viết gì vào màn Ngang

        }
        else if (Screen.width == 720 && Screen.height == 1600)// dọc
        {
            _screne_Size_current = Screne_Size.Doc_720;

            if (_screne_Size_last != _screne_Size_current)
            {
                Set_Doc_720();
            }
            
        }
        else if (Screen.width == 1600 && Screen.height == 720)//  ngang
        {
            
            
        }
        else if (Screen.height > Screen.width)//dọc
        {
            _screne_Size_current = Screne_Size.Doc_1024;
            if (_screne_Size_last != _screne_Size_current)
            {
                Set_Doc_1024();
            }
        }
        else if (Screen.height < Screen.width)//ngang
        {
            
            
        }
    }
   public void Set_Doc_720()
    {
        _screne_Size_last = _screne_Size_current;
        rect_Table_Level.anchoredPosition = vec_rect_Table_Level_720;
        rect_Title_YouWin.anchoredPosition = vec_rect_Title_YouWin_720;
        rect_Roll_bar.anchoredPosition = vec_rect_Roll_bar_720;
        rect_Claim_ADs.anchoredPosition = vec_rect_Claim_ADs_720;
        rect_Claim_no_ADs.anchoredPosition = vec_rect_Claim_no_ADs_720;
        rect_NoThank.anchoredPosition = vec_rect_NoThank_720;
        rect_Gold_Gem_Boot.anchoredPosition = vec_rect_Gold_Gem_Boot_720;
    }
   public void Set_Doc_1024()
    {
        _screne_Size_last = _screne_Size_current;
        rect_Table_Level.anchoredPosition = vec_rect_Table_Level_1024;
        rect_Title_YouWin.anchoredPosition = vec_rect_Title_YouWin_1024;
        rect_Roll_bar.anchoredPosition = vec_rect_Roll_bar_1024;
        rect_Claim_ADs.anchoredPosition = vec_rect_Claim_ADs_1024;
        rect_Claim_no_ADs.anchoredPosition = vec_rect_Claim_no_ADs_1024;
        rect_NoThank.anchoredPosition = vec_rect_NoThank_1024;
        rect_Gold_Gem_Boot.anchoredPosition = vec_rect_Gold_Gem_Boot_1024;
    }
}
