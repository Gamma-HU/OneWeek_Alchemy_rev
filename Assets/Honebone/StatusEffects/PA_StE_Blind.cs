using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StE_Blind : PA_StatusEffects
{
    public override void OnInit()
    {
        characterStatus.blind++;
    }
    public override void OnAttack(int DMG, bool missed)
    {
        ConsumeStack();
    }
    public override void AtTheEnd()
    {
        characterStatus.blind--;
    }
    
}
