using UnityEngine;
using ZombieIo.EffectsSystem;
using Vector3 = UnityEngine.Vector3;

public class WeaponAttackComponent : IAttackComponent
{
    private Character character;
    private WeaponData currentWeaponData;
    private float timeBetweenAttack;


    public float Damage => currentWeaponData.WeaponDamage;
    public float AttackRange => currentWeaponData.AttackRange;
    
    private EffectsFactory EffectsFactory =>
        GameManager.Instance.EffectsFactory;
    

    public void MakeAttack()
    {
        if (timeBetweenAttack > 0
            || character.Target == null)
            return;

        float distance = Vector3.Distance(
            character.CharacterData.CharacterTransform.position,
            character.Target.CharacterData.CharacterTransform.position);

        if (distance > currentWeaponData.AttackRange)
            return;

        character.AnimationComponent.SetTrigger("AttackTrigger");
        timeBetweenAttack = currentWeaponData.TimeBetweenAttack;
        
        var projectile = EffectsFactory.GetProjectile(currentWeaponData.ProjectileTypeEffect);
        projectile.transform.position = character.transform.position + character.transform.forward + Vector3.up;
        
        projectile.transform.rotation = character.CharacterData.CharacterTransform.rotation;
        projectile.Initialize(this.character, Damage, 1000, 
            (character.Target.transform.position - character.transform.position).normalized);
    }

    public void OnUpdate()
    {
        if (timeBetweenAttack > 0)
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    public void Initialize(Character character)
    {
        this.character = character;
        currentWeaponData = character.CharacterData.StarterWeaponData;
        timeBetweenAttack = 0;
    }
}
