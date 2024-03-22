using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StE_Bleed : PA_StatusEffects
{
    public override void OnDamaged(int DMG, bool byOpponent)
    {
        if (byOpponent)
        {
            BattleManager.Action action = new BattleManager.Action();
            action.DMG = stack;
            battleManager.Enqueue(character, character, action);
        }
    }
}
