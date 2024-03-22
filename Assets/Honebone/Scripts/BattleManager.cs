using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�퓬�Ɋւ���X�N���v�g
public class BattleManager : MonoBehaviour
{
    [System.Serializable]
    public class Action
    {
        public Character owner;
        public Character target;

        public bool attack;//�U��������
        public int DMG;//�_���[�W��
        public int heal;
        public List<StEParams> applyStE = new List<StEParams>();
        public List<GameObject> removeStE = new List<GameObject>();
    }
    [System.Serializable]
    public class StEParams
    {
        public GameObject StE;//�t�^/���������Ԉُ�
        public int amount;//�t�^/�������鐔
    }
    [SerializeField, Header("������ׂ��炸")]
    Action attack;

    [SerializeField,Header("�ȉ��Q�͐퓬���x�Ɋ֘A")]
    float actionInterval;
    [SerializeField]
    float turnInterval;
    [SerializeField, Header("�G�̃I�u�W�F�N�g�̐e�ƂȂ�\n���̏ꏊ�ɓG������")]
    Transform enemyP;

    List<Action> actionQueue = new List<Action>();

    [SerializeField]//test
    Character player;
    Character enemy;
    bool playerTurn;

    ExpeditionManager expeditionManager;
    private void Start()//test
    {
        expeditionManager = FindObjectOfType<ExpeditionManager>();
    }
    public void StartBattle(GameObject enemyObj)
    {
        var e = Instantiate(enemyObj, enemyP);
        enemy = e.GetComponent<Character>();
        enemy.Init(this);
        enemy.SetOpponent(player);
        player.SetOpponent(enemy);
        Debug.Log("�퓬�J�n");

        //DungeonEffect
        player.OnBattleStart();
        enemy.OnBattleStart();
        StartResolve();
    }
    public void NextTurn()
    {
        
        playerTurn = !playerTurn;
        if (playerTurn)
        {
            Debug.Log(string.Format("{0}�̍U��", player.GetCharacterStatus().charaName));
            Enqueue(player, enemy, attack);
        }
        else
        {
            Debug.Log(string.Format("{0}�̍U��", enemy.GetCharacterStatus().charaName));
            Enqueue(enemy, player, attack);
        }
        StartResolve();
    }
    public void BattleEnd(bool victory)
    {
        playerTurn = false;
        actionQueue.Clear();
        if (victory)//�G�����S�����Ȃ�
        {
            Debug.Log("����");
            Destroy(enemyP.GetChild(0).gameObject);//�G�̃I�u�W�F�N�g��j��
            expeditionManager.NextLayer();
        }
    }

   
    public void Enqueue(Character owner,Character target,Action action)
    {
        action.owner = owner;
        action.target = target;
        actionQueue.Add(action);
    }
    void StartResolve()
    {
        if (actionQueue.Count > 0) { Resolve(); }
        else { NextTurn(); }
    }
    void Resolve()
    {
        if(actionQueue.Count > 0)
        {
            Action action = actionQueue[0];
            if (action.attack) { action.owner.Attack(); }
            if (action.DMG>0) { action.target.Damage(action.DMG, false); }
            if (action.heal > 0) { action.target.Heal(action.heal); }
            foreach(StEParams StEParams in action.applyStE)
            {
                action.target.ApplyStE(StEParams);
            }
            foreach(GameObject removeStE in action.removeStE)
            {
                action.target.RemoveStE(removeStE);
            }
            actionQueue.RemoveAt(0);
            if (!CheckBattleEnd()) { StartCoroutine(ActionInterval()); }
        }
        else
        {
            if (!CheckBattleEnd()) { EndResolve(); }
        }
    }
    bool CheckBattleEnd()
    {
        if (player.GetCharacterStatus().dead)//�s�k
        {
            BattleEnd(false);
            return true;
        }
        if (enemy.GetCharacterStatus().dead)//����
        {
            BattleEnd(true);
            return true;
        }
        return false;
    }
    void EndResolve()
    {
        StartCoroutine(TurnInterval());
    }

    IEnumerator ActionInterval()
    {
        yield return new WaitForSeconds(actionInterval);
        Resolve();
    }
    IEnumerator TurnInterval()
    {
        yield return new WaitForSeconds(turnInterval);
        NextTurn();
    }
}
