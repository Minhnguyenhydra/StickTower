using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class SelectLevelController : PopUpProperties
{
    public ScrollRect sc;
    public List<BouderSelectLevel> bouderSelectLevels = new List<BouderSelectLevel>();
    public override void OpenMe()
    {
        base.OpenMe();
        sc.verticalNormalizedPosition = 1;
        Display();
    }
    public override void CloseMe()
    {
        base.CloseMe();
        DataParam.canDelete = true;
        GameController.instance.LoadLevel();
    }
    void Display()
    {
        for(int i = 0; i < bouderSelectLevels.Count; i++)
        {
            bouderSelectLevels[i].Display();
        }
    }
    public void BtnBack()
    {
        Application.LoadLevel("Loading");
    }   
    public void BtnClose()
    {
        base.CloseMe();
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
