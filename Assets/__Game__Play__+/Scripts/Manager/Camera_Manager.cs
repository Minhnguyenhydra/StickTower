using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EZCameraShake;
using Com.LuisPedroFonseca.ProCamera2D;
[DefaultExecutionOrder(1)]
public class Camera_Manager : Singleton<Camera_Manager>
{

    public Camera cam;
    public Transform tf_Cam;
    public List<Transform> list_Pos_Cam_move;//list các điểm camera di chuyển khi 1 nhà đỏ đổi màu xanh
    [Header("Điền số nhà Enemy vào đây")]
    public int soNha_Enemy_Trong_Level;
    public Vector3 pos_Start_Cam;
    //
    public Transform tf_FloorBoss;
    ProCamera2DShake procamShake;
    ScrollBG bg;
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //  CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
    //        procamShake.Shake(0);
    //    }
    //}
    public void ShakeCam()
    {
        procamShake.Shake(0);
    }
    GameObject bgOld;
    private void Awake()
    {
        bg = Instantiate(Resources.Load<ScrollBG>("BG/BG" + PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge()));

        bg.SetUp(PlayerPrefs_Manager.Get_Key_1GamPlay_Or_2Area_Or_3Challenge());
        bgOld = GameObject.Find("==Obj==Canvas_BG");
        if (bgOld != null)
            bgOld.SetActive(false);
        //  bg.transform.parent = cam.transform;
    }
    private void Start()
    {
        cam.gameObject.AddComponent<ProCamera2D>();
        procamShake = cam.gameObject.AddComponent<ProCamera2DShake>();

        ShakePreset newShakePreset = new ShakePreset();
        newShakePreset.Strength = new Vector3(2, 2, 0);
        newShakePreset.Duration = 0.2f;
        newShakePreset.Vibrato = 3;
        newShakePreset.Smoothness = 0.1f;
        newShakePreset.Randomness = 0.1f;
        newShakePreset.UseRandomInitialAngle = true;
        newShakePreset.Rotation = Vector3.zero;
        newShakePreset.IgnoreTimeScale = false;
        procamShake.ShakePresets.Add(newShakePreset);
        //soNha_Enemy_Trong_Level = Constant.Get_Type_Castle_By_Level(PlayerPrefs_Manager.Get_Index_Level_Normal());
        pos_Start_Cam = transform.position;
        if (soNha_Enemy_Trong_Level > 1)
        {

            tf_Cam.DOMoveX(list_Pos_Cam_move[soNha_Enemy_Trong_Level - 2].position.x, Constant.Time_Cam_Move).OnComplete(
                () => tf_Cam.DOMoveX(pos_Start_Cam.x, Constant.Time_Cam_Move).OnComplete(
                    () =>
                    {
                        ((CanvasGamePlay)UIManager.Ins.GetUI(UIID.UICGamePlay)).Set_Active_Parrent_Castle_This_Level();


                    }
                    )
                );
        }

        else
            ((CanvasGamePlay)UIManager.Ins.GetUI(UIID.UICGamePlay)).Set_Active_Parrent_Castle_This_Level();

        cam = Camera.main;
        tf_Cam = cam.transform;
    }
    public void Move_Cam(int _index_Move)
    {
        //Debug.Log("====");
        tf_Cam.DOMoveX(list_Pos_Cam_move[_index_Move].position.x, Constant.Time_Cam_Move);
        //StartCoroutine(IE_Delay_Move_Cam_Next_House(_index_Move));
    }
    IEnumerator IE_Delay_Move_Cam_Next_House(int _index_Move)
    {
        yield return Cache.GetWFS(Constant.Time_Cam_Delay_Move_Next_House);



        tf_Cam.DOMoveX(list_Pos_Cam_move[_index_Move].position.x, Constant.Time_Cam_Move);
    }
    public void SetMove_To_Floor_Boss(Transform tff)
    {
        Vector3 pos = new Vector3(tff.position.x, tff.position.y, 0) + new Vector3(0, 0, -2);
        tf_Cam.DOMove(pos, 0.5f);
        cam.DOOrthoSize(8.5f, 0.5f);
    }
    [ContextMenu("TEEE")]
    public void Teeee()
    {
        SetMove_To_Floor_Boss(tf_FloorBoss);
        cam.DOOrthoSize(7, 0.5f);
    }
}
