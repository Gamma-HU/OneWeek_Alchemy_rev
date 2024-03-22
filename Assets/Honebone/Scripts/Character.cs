using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [System.Serializable]
    public class CharacterStatus
    {
        public string charaName;

        [Header("�ő�HP")] public int maxHP;
        [Header("�U����")] public int ATK;

        [Header("�����i�A�G�L�����̓����A��Ԉُ�\n(PassiveAbility���A�^�b�`����Ă���GameObjrct)")] public List<GameObject> passiveAbilities;


        [Header("\n\n=====<�ȉ��̃X�e�[�^�X�͐퓬���ɕω�>=====\n\n")]
        [Header("�U�����̃_���[�W�ʂɉ��Z�E��Z")]
        public int exDMG_int;
        public float exDMG_mul;

        [Header("�G����̔�_���[�W�ʂɉ��Z�E��Z")]
        public int PROT_int;
        public float PROT_mul;

        [Header("��񕜗ʂɏ�Z")] public float RHeal_mul;

        [Header("����HP")] public int HP;

        public bool dead;
        public int blind;

        public float GetHPPercent()
        {
            return HP * 1f / maxHP * 1f;
        }
    }
    [SerializeField]
    CharacterStatus status;

    List<PassiveAbility> passiveAbilities = new List<PassiveAbility>();

    Character opponent;
    CharacterStatus opponetnStatus;
    BattleManager battleManager;

    public void Init(BattleManager bm)
    {
        battleManager = bm;
        status.HP = status.maxHP;
        foreach (GameObject passiveAbility in status.passiveAbilities)//staus�ɂ���p�b�V�u�A�r���e�B����A�X�N���v�g�����𒊏o(�U�������̍ۂ̊ȗ����̂���)
        {
            var p = Instantiate(passiveAbility, transform);
            p.GetComponent<PassiveAbility>().Init(this,battleManager);
            passiveAbilities.Add(p.GetComponent<PassiveAbility>());
        }
    }


    public void SetOpponent(Character chara) //�퓬����̐ݒ�
    {
        opponent = chara;
        opponetnStatus = opponent.GetCharacterStatus();
    }
    public void Attack()
    {
        if (status.blind == 0)
        {
            float fDMG = status.ATK;//��b�_���[�W
            float exDMG_mul = Mathf.Max(0f, 1f + status.exDMG_mul - opponetnStatus.PROT_mul);//�_���[�W�{���␳ = 1 + [���g�̗^�_���[�W���␳] - [����̔�_���[�W���␳] (���ɂ͂Ȃ�Ȃ�)
            int exDMG_int = status.exDMG_int - opponetnStatus.PROT_int;//�_���[�W�����␳ = [���g�̗^�_���[�W�␳] - [����̔�_���[�W�␳]
            fDMG = Mathf.Max(0f, (fDMG * exDMG_mul) + exDMG_int);//�_���[�W = ([��b�_���[�W] * [�_���[�W�{���␳]) + [�_���[�W�����␳]
            int DMG = Mathf.RoundToInt(fDMG);
            opponent.Damage(DMG,true);//�l�̌ܓ����đ����Damage�֐��ɓn��
            OnAttack(DMG, false);
            opponent.OnAttacked(DMG, false);
        }
        else
        {
            OnAttack(0, true);
            opponent.OnAttacked(0, true);
            Debug.Log("Miss");
        }
        //===============================================[[�U�������o]]===================================================
    }
    public void Damage(int DMG,bool byOpponent)
    {
        //===============================================[[���l�\��]]DamageLog(int DMG)===================================================
        status.HP-= DMG;
        Debug.Log(string.Format("{0}��{1}�_���[�W(�c��{2})", status.charaName, DMG, status.HP));
        OnDamaged(DMG, byOpponent);
        if (status.HP <= 0) { Die(); }
    }
    public void Heal(int value)
    {
        float exHeal = Mathf.Max(0f, 1 + status.RHeal_mul);
        int heal = Mathf.RoundToInt(value * exHeal);
        status.HP = Mathf.Min(status.HP + heal, status.maxHP);
        //===============================================[[���l�\��]]HealLog(int value)===================================================
        Debug.Log(string.Format("{0}��{1}��", status.charaName, heal));
        OnHealed(heal);
    }
    public void ApplyStE(BattleManager.StEParams stEParams)
    {
        string StEName = stEParams.StE.GetComponent<PassiveAbility>().GetPAName();
        bool found = false;
        foreach (PassiveAbility passiveAbility in new List<PassiveAbility>(passiveAbilities))
        {
            if (passiveAbility.GetPAName() == StEName)
            {
                found = true;
                passiveAbility.GetComponent<PA_StatusEffects>().AddStack(stEParams.amount);
            }
        }
        if (!found)
        {
            var p = Instantiate(stEParams.StE, transform);
            p.GetComponent<PassiveAbility>().Init(this, battleManager);
            p.GetComponent<PA_StatusEffects>().StEInit(stEParams.amount);
            passiveAbilities.Add(p.GetComponent<PassiveAbility>());
        }
        Debug.Log(string.Format("{0}��{1}��{2}�t�^", status.charaName, StEName, stEParams.amount));
        OnAppliedStE(stEParams);
    }
    public void RemoveStE(GameObject remove)
    {
        string StEName = remove.GetComponent<PassiveAbility>().GetPAName();
        foreach (PassiveAbility passiveAbility in new List<PassiveAbility>(passiveAbilities))
        {
            if (passiveAbility.GetPAName() == StEName)
            {
                passiveAbility.GetComponent<PA_StatusEffects>().DisableStE();
                DisableStE(passiveAbility);
                Debug.Log(string.Format("{0}��{1}������", status.charaName, StEName));
            }
        }
    }
    public void DisableStE(PassiveAbility remove)
    {
        passiveAbilities.Remove(remove);
    }
    void Die()
    {
        //===============================================[[���S�����o]]===================================================
        Debug.Log(string.Format("{0}�͂����ꂽ", status.charaName));
        status.dead = true;
    }

    //-----------------------------------------------------<�ȉ��U������>-----------------------------------------------------

    public void OnBattleStart()
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnBattleStart(); }
    }
    /// <summary>�U�����A�����������Ɋւ�炸�U��</summary>
    public void OnAttack(int DMG, bool missed)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnAttack(DMG, missed); }
    }
    /// <summary>�U�����ꂽ���A�����������Ɋւ�炸�U��</summary>
    public void OnAttacked(int DMG, bool missed)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnAttacked(DMG, missed); }
    }

    /// <summary>��_���[�W���U��</summary>
    public void OnDamaged(int DMG, bool byOpponent)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnDamaged(DMG, byOpponent); }
    }

    /// <summary>��񕜎��U��</summary>
    public void OnHealed(int healedValue)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnHealed(healedValue); }
    }
    /// <summary>��Ԉُ�t�^���ꂽ���U��</summary>
    public void OnAppliedStE(BattleManager.StEParams applied)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnAppliedStE(applied); }
    }

    public CharacterStatus GetCharacterStatus() { return status; }
    public string GetInfo()
    {
        string s = "";
        s += string.Format("�̗́F{0}/{1}", status.HP, status.maxHP);
        s += string.Format("�U���́F{0}",status.ATK);
        //�ePssiveAbility����
        return s;
    }
}
