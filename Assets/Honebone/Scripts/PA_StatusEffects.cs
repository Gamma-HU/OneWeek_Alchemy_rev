using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StatusEffects : PassiveAbility
{
    protected int stack;
    public void StEInit(int s)
    {
        stack = s;
        OnInit();
    }
    public void AddStack(int add)
    {
        if (add < 0) { Debug.Log("error：増加するスタック数が負の数になっています"); }
        stack += add;
        OnAddStack(add);
    }
    public void ConsumeStack()
    {
        stack--;
        if (stack == 0)
        {
            character.DisableStE(this);
            DisableStE();
        }
    }
    public void DisableStE()
    {
        AtTheEnd();
        Destroy(gameObject);
    }
    public virtual void OnInit() { }
    public virtual void OnAddStack(int add) { }
    public virtual void AtTheEnd() { }
}
