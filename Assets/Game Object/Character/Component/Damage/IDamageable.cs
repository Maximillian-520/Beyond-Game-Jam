using System;
using UnityEngine;

public interface IDamageable
{
    public void ReceiveDamage(int damageAmount);
    public void ReceiveHeal(int healAmount);
}
