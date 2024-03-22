using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetContentElement : MonoBehaviour
{
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private TextMeshProUGUI StageTitleText;
    [SerializeField] private TextMeshProUGUI Difficulty_Text;
    [SerializeField] private TextMeshProUGUI DungeonInfo;

    public void setElement(Sprite image, string text_stageTitle, int text_difficulty, string text_dungeonInfo)
    {
        BackgroundImage.sprite = image;
        StageTitleText.text = text_stageTitle;
        Difficulty_Text.text = text_difficulty.ToString();
        DungeonInfo.text = text_dungeonInfo;
    }
}
