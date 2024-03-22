using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{

    [SerializeField] [Header("HPカラー（60% ~ 100%）")] Color normalColor;
    [SerializeField] [Header("HPカラー（30% ~ 60%）")] Color damagedColor;
    [SerializeField][Header("HPカラー（0% ~ 30%）")] Color dangerColor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
