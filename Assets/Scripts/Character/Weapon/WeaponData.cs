using UnityEngine;
using ZombieIo.EffectsSystem;

public abstract class WeaponData : ScriptableObject
{
    [SerializeField]
    private float weaponDamage;
    [SerializeField]
    private float timeBetweenAttack;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private EffectType projectileTypeEffect;


    public float WeaponDamage => weaponDamage;
    public float TimeBetweenAttack => timeBetweenAttack;
    public float AttackRange => attackRange;
    public EffectType ProjectileTypeEffect => projectileTypeEffect;
}