using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Numerics;




public class UIManager : Singleton<UIManager>
{
    private Dictionary<UIID, UICanvas> UICanvas = new Dictionary<UIID, UICanvas>();

    public Transform CanvasParentTF;
    #region Quan Add
    //public List<int> list_Indext_Canvas_MrQuan_Custom;
    public List<UICanvas> listUICanvas_MrQuan_Custom;
    #endregion

    #region Canvas

    public bool IsOpenedUI(UIID ID)
    {
        return UICanvas.ContainsKey(ID) && UICanvas[ID] != null && UICanvas[ID].gameObject.activeInHierarchy;
    }

    public UICanvas GetUI(UIID ID)
    {
        //
        if (!UICanvas.ContainsKey(ID) || UICanvas[ID] == null)
        {
            //Quân add: tạo sẵn 1 list canvas ở UIMnager, kéo thả UIcanvas vào, ở đây kiểm tra nếu list ko có thì sinh ra như dưới đây, nếu có chỉ cần UICanvas[ID] = List<UIcanvas>[i]
            bool isCustomIDCanvas_MrQuan = false;
            for (int i = 0; i < listUICanvas_MrQuan_Custom.Count; i++)
            {
                if (listUICanvas_MrQuan_Custom[i].uIID_Of_Canvas == ID)
                {
                    UICanvas[ID] = listUICanvas_MrQuan_Custom[i];
                    isCustomIDCanvas_MrQuan = true;
                    break;
                }
            }
            if (!isCustomIDCanvas_MrQuan)
            {
                UICanvas canvas = Instantiate(Resources.Load<UICanvas>("UI/" + ID.ToString()), CanvasParentTF);
                UICanvas[ID] = canvas;
            }
            //Mr Link
            //UICanvas canvas = Instantiate(Resources.Load<UICanvas>("UI/" + ID.ToString()), CanvasParentTF);
            //UICanvas[ID] = canvas;
        }

        return UICanvas[ID];
    } 
    
    public T GetUI<T>(UIID ID) where T : UICanvas
    {
        return GetUI(ID) as T;
    }

    public UICanvas OpenUI(UIID ID)
    {
        UICanvas canvas = GetUI(ID);

        canvas.Setup();
        canvas.Open();

        return canvas;
    }  
    
    public T OpenUI<T>(UIID ID) where T : UICanvas
    {
        return OpenUI(ID) as T;
    }

    public bool IsOpened(UIID ID)
    {
        return UICanvas.ContainsKey(ID) && UICanvas[ID] != null;
    }

    #endregion

    #region Back Button

    private Dictionary<UICanvas, UnityAction> BackActionEvents = new Dictionary<UICanvas, UnityAction>();
    private List<UICanvas> backCanvas = new List<UICanvas>();
    UICanvas BackTopUI {
        get
        {
            UICanvas canvas = null;
            if (backCanvas.Count > 0)
            {
                canvas = backCanvas[backCanvas.Count - 1];
            }

            return canvas;
        }
    }


    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Escape) && BackTopUI != null)
        {
            BackActionEvents[BackTopUI]?.Invoke();
        }
    }

    public void PushBackAction(UICanvas canvas, UnityAction action)
    {
        if (!BackActionEvents.ContainsKey(canvas))
        {
            BackActionEvents.Add(canvas, action);
        }
    }

    public void AddBackUI(UICanvas canvas)
    {
        if (!backCanvas.Contains(canvas))
        {
            backCanvas.Add(canvas);
        }
    }

    public void RemoveBackUI(UICanvas canvas)
    {
        backCanvas.Remove(canvas);
    }

    /// <summary>
    /// CLear backey when comeback index UI canvas
    /// </summary>
    public void ClearBackKey()
    {
        backCanvas.Clear();
    }

    #endregion

    public void CloseUI(UIID ID)
    {
        if (IsOpened(ID))
        {
            GetUI(ID).Close();
        }
    }

}
