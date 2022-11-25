using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelController : MonoBehaviour, IDragHandler, IEndDragHandler
{

    SkeletonAnimation animVisibleInsideMask;

    SkeletonAnimation animNormal;

    PaintToSpriteMaskController correctObj;

    int currentStep;
    public List<StepOfLevel> stepOfLevel = new List<StepOfLevel>();

    void SetStep()
    {
        animVisibleInsideMask = stepOfLevel[currentStep].animVisibleInsideMask;
        animNormal = stepOfLevel[currentStep].animNormal;
        correctObj = stepOfLevel[currentStep].correctObj;
        stepOfLevel[currentStep].gameObject.SetActive(true);
        StartCoroutine(IELevel());
    }



    private void Start()
    {
        currentStep = 0;
        SetStep();

    }

    public void OnDrag(PointerEventData eventData)
    {
        correctObj.isDelete = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        correctObj.isDelete = false;

    }
    public IEnumerator IELevel()
    {
        while (true)
        {
            yield return new WaitUntil(() =>
            {
                return Input.GetMouseButtonUp(0);
            });


            if (correctObj.IsDeleteFinish())
            {
                Win();
                break;
            }
            else
            {
                correctObj.ClearDelete();
                Debug.LogError("=======xoa chua het");
            }
        }

        yield return DataParam.WAITDELETECHECK;

    }

    public void PlayAnimWin()
    {
        animVisibleInsideMask.AnimationState.SetAnimation(0, "win", true);
        GameController.instance.ShowPopUpWin();
    }

    public void Win()
    {
        correctObj.isDelete = false;
        correctObj.gameObject.SetActive(false);
        animNormal.gameObject.SetActive(false);
        animVisibleInsideMask.maskInteraction = SpriteMaskInteraction.None;

        //  currentStep++;

        if (currentStep == stepOfLevel.Count - 1)
        {
            PlayAnimWin();
        }
        else
        {
            stepOfLevel[currentStep].gameObject.SetActive(false);
            currentStep++;
            SetStep();
            GameController.instance.ShowPopUpWatchAds();
            Debug.LogError("======= hien thi popup");
        }
        Debug.LogError("========= xoa het");

    }
    public void Load()
    {
        stepOfLevel.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            StepOfLevel _stepOfLevel = transform.GetChild(i).GetComponent<StepOfLevel>();
            if (_stepOfLevel != null)
            {
                stepOfLevel.Add(_stepOfLevel);
            }
        }
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(LevelController))]
public class LevelEditor : Editor
{
    public bool isCheck;
    private LevelController myScript;

    private void OnSceneGUI()
    {
        myScript = (LevelController)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (myScript == null)
            myScript = (LevelController)target;
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
