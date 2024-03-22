using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DungeonData")]
public class DungeonData : ScriptableObject
{
    public string dungeonName;
    public GameObject dungeonEffect;
    public Sprite background;
    public int difficulty;
    [TextArea(3, 10)] public string dungeonInfo;
    [Header("0�Ԗڂ��珇�ɐړG")]public List<GameObject> enemies;
    public List<GameObject> rewardItems;
    [SerializeField,Header("�N���A���ɊJ�������_���W����")]
    public List<DungeonData> nextDungeons;
}
