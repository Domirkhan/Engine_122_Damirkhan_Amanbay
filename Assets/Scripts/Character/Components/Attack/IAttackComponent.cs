using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackComponent
{
    public float Damage { get; }

    // Изменяем Initialize для приема Character
    public void Initialize(Character character);

    public void MakeDamage(Character target);
}
