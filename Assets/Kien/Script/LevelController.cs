using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject lockObj, unlockObj;
    public GameObject[] manhs;

    SkeletonAnimation animVisibleInsideMask;

    SkeletonAnimation animNormal;

    PaintToSpriteMaskController correctObj;

    int currentStep;
    public List<StepOfLevel> stepOfLevel = new List<StepOfLevel>();

    public void SetStep()
    {
        for (int i = 0; i < stepOfLevel.Count; i++)
        {
            stepOfLevel[i].gameObject.SetActive(false);
        }
        if (currentStep == 0)
        {
            animVisibleInsideMask = stepOfLevel[currentStep].animVisibleInsideMask;
            animNormal = stepOfLevel[currentStep].animNormal;
            correctObj = stepOfLevel[currentStep].correctObj;
            stepOfLevel[currentStep].gameObject.SetActive(true);
            StartCoroutine(IELevel());
            DataParam.begin = true;
        }
        else if (currentStep == stepOfLevel.Count)
        {
            animVisibleInsideMask = stepOfLevel[currentStep - 1].animVisibleInsideMask;
            animNormal = stepOfLevel[currentStep - 1].animNormal;
            correctObj = stepOfLevel[currentStep - 1].correctObj;

            correctObj.isDelete = false;
            correctObj.gameObject.SetActive(false);
            animNormal.gameObject.SetActive(false);
            animVisibleInsideMask.maskInteraction = SpriteMaskInteraction.None;

            stepOfLevel[currentStep - 1].gameObject.SetActive(true);
            animVisibleInsideMask.AnimationState.SetAnimation(0, "win", true);
            DataParam.begin = false;
        }
        else
        {
            animVisibleInsideMask = stepOfLevel[currentStep].animVisibleInsideMask;
            animNormal = stepOfLevel[currentStep].animNormal;
            correctObj = stepOfLevel[currentStep].correctObj;
            stepOfLevel[currentStep].gameObject.SetActive(true);
            GameController.instance.ShowPopUpWatchAds();
            //  StartCoroutine(IELevel());
            DataParam.begin = false;
        }
  


    }
    public void CallIELevel()
    {
        StartCoroutine(IELevel());
    }

    public void DisplayLockOrUnlock()
    {
        if (Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].unlock)
        {
            if (Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].takeSprite.FindAll(x => x == true).Count == Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].takeSprite.Count)
            {
                lockObj.SetActive(false);
                unlockObj.SetActive(true);
                SetStep();
            }
            else
            {
                for (int i = 0; i < Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].takeSprite.Count; i++)
                {
                    manhs[i].SetActive(Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].takeSprite[i]);
                }
                lockObj.SetActive(true);
                unlockObj.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].takeSprite.Count; i++)
            {
                manhs[i].SetActive(Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].takeSprite[i]);
            }
            lockObj.SetActive(true);
            unlockObj.SetActive(false);
        }

    }

    private void Start()
    {
        currentStep = Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].currentStep;
        DisplayLockOrUnlock();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!unlockObj.activeSelf)
            return;
        correctObj.isDelete = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!unlockObj.activeSelf)
            return;
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
        GameController.instance.ShowPopUpResult();
    }

    public void Win()
    {
        correctObj.isDelete = false;
        correctObj.gameObject.SetActive(false);
        animNormal.gameObject.SetActive(false);
        animVisibleInsideMask.maskInteraction = SpriteMaskInteraction.None;

         // currentStep++;

        if (currentStep == stepOfLevel.Count - 1)
        {
            PlayAnimWin();

        }
        else
        {
            //   stepOfLevel[currentStep].gameObject.SetActive(false);

            GameController.instance.ShowPopUpWatchAds();
            Debug.LogError("======= hien thi popup");
        }
        currentStep++;
        Datacontroller.instance.saveData.saveDelete.infoSaveDelete[DataParam.currentLevel].currentStep = currentStep;
        Debug.LogError("========= xoa het");

    }
    public void Load()
    {
        stepOfLevel.Clear();
        for (int i = 0; i < unlockObj.transform.childCount; i++)
        {
            StepOfLevel _stepOfLevel = unlockObj.transform.GetChild(i).GetComponent<StepOfLevel>();
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
