using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private Text txtQuestName;
    [SerializeField]
    private Text txtProcess;
    [SerializeField]
    private Image imgProcess;
    [SerializeField]
    private Text txtGoldRewarded;
    [SerializeField]
    private GameObject objGoldRewarded;
    [SerializeField]
    private Text txtGemRewarded;
    [SerializeField]
    private GameObject objGemRewarded;
    [SerializeField]
    private Text txtClaim;
    [SerializeField]
    private Button btnClaim;

    private int questID;
    private int maxNumber;
    private int gold;
    private int gem;

    public int QuestID => questID;

    public void SetData(string questName, int questID, int curNumber, int maxNumber, int goldRewarded, int gemRewarded)
    {
        if (PlayerPrefs_Manager.GetClaimed(Constant.Quest + questID))
        {
            txtClaim.text = "Claimed";
            txtClaim.color = Color.white;
            btnClaim.interactable = false;
            txtQuestName.text = questName + $"({maxNumber}/{maxNumber})";
            txtProcess.text = maxNumber + "/" + maxNumber;
            if (goldRewarded > 0)
            {
                txtGoldRewarded.text = goldRewarded.ToString();
                objGoldRewarded.SetActive(true);
            }
            if (gemRewarded > 0)
            {
                txtGemRewarded.text = gemRewarded.ToString();
                objGemRewarded.SetActive(true);
            }
            gameObject.SetActive(true);
            return;
        }

        this.questID = questID;
        this.maxNumber = maxNumber;
        gold = goldRewarded;
        gem = gemRewarded;

        txtQuestName.text = questName + $"({curNumber}/{maxNumber})";
        txtProcess.text = curNumber + "/" + maxNumber; 
        //imgProcess.fillAmount = Mathf.Clamp(curNumber/maxNumber, 0f, 1f);
        if (goldRewarded > 0)
        {
            txtGoldRewarded.text = goldRewarded.ToString();
            objGoldRewarded.SetActive(true);
        }
        if (gemRewarded > 0)
        {
            txtGemRewarded.text = gemRewarded.ToString();
            objGemRewarded.SetActive(true);
        }

        int value = PlayerPrefs_Manager.GetQuest(Constant.Quest + questID);
        if (value >= maxNumber)
            btnClaim.interactable = true;
        else
            btnClaim.interactable = false;

        gameObject.SetActive(true);
    }

    public void OpenQuest()
    {
        btnClaim.interactable = true;
    }

    public void ClaimClicked()
    {
        txtClaim.text = "Claimed";
        txtClaim.color = Color.white;

        btnClaim.interactable = false;

        int curGold = PlayerPrefs_Manager.Get_Gold();
        PlayerPrefs_Manager.Set_Gold(curGold + gold);

        int curGem = PlayerPrefs_Manager.Get_Gem();
        PlayerPrefs_Manager.Set_Gem(curGem + gem);

        PlayerPrefs_Manager.SetClaimed(Constant.Quest + questID, true);

        ((CanvasMainMenu)UIManager.Ins.GetUI(UIID.UICMainMenu)).Set_Reload_Gold_Gem_Title();

        SoundManager.Ins.PlayFx(FxID.click);

        EventController.DAILYGIFT(Constant.Quest + questID);
    }
}
