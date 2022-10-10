using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Health_Bar : MonoBehaviour
{
    ///public int health;
    public TextMeshProUGUI txt_Health;
    public Transform tf_Health_Bar;
    private void Start()
    {
        tf_Health_Bar = transform;
    }
    public void Set_Step_By_Step_Health(int _score, int target, float transitionTime)
    {
        Tween t = DOTween.To(() => _score, x => _score = x, target, transitionTime).OnUpdate(() => txt_Health.SetText(_score.ToString("N0")));
    }
    public void Set_Health_Imedetly(int _health)
    {
        txt_Health.SetText(_health.ToString("N0"));
    }
    public void Set_Damage_Sword_Imedetly(int _health)
    {
        txt_Health.SetText("+"+_health.ToString("N0"));
    }
    public void Set_X2Damage_Imedetly(int _Xhealth)
    {
        txt_Health.SetText("X"+_Xhealth.ToString("N0"));
    }
    public void Set_Hide_Health_Bar()
    {
        this.gameObject.SetActive(false);
    }
}
