using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAbility : MonoBehaviour
{
    [SerializeField] string PAName;
    [SerializeField, TextArea(3, 10)] string PAInfo;
    [SerializeField] int exHP;
    [SerializeField] int exATK;
    protected Character character;
    protected Character.CharacterStatus characterStatus;
    protected BattleManager battleManager;
    public void Init(Character chara,BattleManager bm)
    {
        character = chara;
        characterStatus = character.GetCharacterStatus();
        battleManager = bm;

        characterStatus.HP += exHP;
        characterStatus.ATK += exATK;
    }

    public virtual void OnBattleStart() { }
    public virtual void OnAttack(int DMG, bool missed) { }
    public virtual void OnAttacked(int DMG, bool missed) { }
    public virtual void OnDamaged(int DMG, bool byOpponent) { }
    public virtual void OnHealed(int healedValue) { }
    public virtual void OnAppliedStE(BattleManager.StEParams applied) { }

    public string GetPAName() { return PAName; }
    public string GetInfo()
    {
        string s = "";
        if (exHP > 0) { s += string.Format("‘Ì—Í+{0}\n", exHP); }
        if (exATK > 0) { s += string.Format("UŒ‚—Í+{0}\n", exATK); }
        if (PAInfo != "") { s += PAInfo; }
        return s;
    }
}
