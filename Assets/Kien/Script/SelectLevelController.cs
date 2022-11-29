using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class SelectLevelController : PopUpProperties
{
    public ScrollRect sc;
    public List<BouderSelectLevel> bouderSelectLevels = new List<BouderSelectLevel>();
    public GameObject btnUnlock,btnSelect,btnBack;
    public void DisableBtn()
    {
        btnSelect.SetActive(false);
        btnBack.SetActive(false);
    }    
    public void BtnUnlock()
    {
        Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].unlock = true;
        bouderSelectLevels[DataParam.currentLevel].Display();
        GameController.instance.levelController.DisplayLockOrUnlock();
        btnUnlock.SetActive(false);
        Debug.LogError("===== click unlock");
    }    
    public override void OpenMe()
    {
        base.OpenMe();
        sc.verticalNormalizedPosition = 1;
        btnUnlock.SetActive(false);
        btnSelect.SetActive(false);
    }
    public override void CloseMe()
    {
        //  base.CloseMe();
        sc.gameObject.SetActive(false);
        btnSelect.SetActive(true);
        btnBack.SetActive(true);

        if (Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].unlock)
        {
            btnUnlock.SetActive(false);
            DataParam.canDelete = true;
        }
        else
        {
            btnUnlock.SetActive(true);
            DataParam.canDelete = false;
        }
    }
   public void Display()
    {
        for(int i = 0; i < bouderSelectLevels.Count; i++)
        {
            bouderSelectLevels[i].Display();
        }
    }
    public void BtnBack()
    {
        GameController.instance.BackToLoading();

      //  UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }   
    public void BtnSelect()
    {
        if (sc.gameObject.activeSelf)
            return;
        btnUnlock.SetActive(false);
        sc.gameObject.SetActive(true);
        btnSelect.SetActive(false);
        btnBack.SetActive(true);
        sc.verticalNormalizedPosition = 1;
        if (GameController.instance.levelController != null)
        {
            GameController.instance.levelController.gameObject.SetActive(false);
        }
    }    
    public void Load()
    {
        bouderSelectLevels.Clear();
        for (int i = 0; i < sc.transform.GetChild(0).childCount; i++)
        {
            BouderSelectLevel _bouderSelectLevel = sc.transform.GetChild(0).GetChild(i).GetComponent<BouderSelectLevel>();
            if (_bouderSelectLevel != null)
            {
                _bouderSelectLevel.mySelectLevel = this;
                _bouderSelectLevel.index = i;
                bouderSelectLevels.Add(_bouderSelectLevel);
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(SelectLevelController))]
public class SelectLevelControllerEditor : Editor
{
    public bool isCheck;
    private SelectLevelController myScript;

    private void OnSceneGUI()
    {
        myScript = (SelectLevelController)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (myScript == null)
            myScript = (SelectLevelController)target;
        GUIStyle SectionNameStyle = new GUIStyle();
        SectionNameStyle.fontSize = 16;
        SectionNameStyle.normal.textColor = Color.blue;

        if (myScript == null) return;

        EditorGUILayout.LabelField("----------\t----------\t----------\t----------\t----------", SectionNameStyle);
        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            if (GUILayout.Button("Load", GUILayout.Height(50)))
            {
                isCheck = true;
                myScript.Load();
            }
        }
        EditorGUILayout.EndVertical();
        if (isCheck)
        {
            EditorUtility.SetDirty(myScript);
            isCheck = false;
        }
        //
    }
}
#endif
