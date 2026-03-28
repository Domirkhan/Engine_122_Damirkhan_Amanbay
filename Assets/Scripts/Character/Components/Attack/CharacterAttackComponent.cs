using UnityEngine;

public class CharacterAttackComponent : IAttackComponent
{
    private Transform characterTransform;
    private float lockDamageTime = 0;
    private float timeBetweenAttacks = 1f;
    private float damage = 5f;
    private bool spawnPlayerProjectileVisual;

    public float Damage => damage;

    public void Initialize(Character character)
    {
        characterTransform = character.Data.CharacterTransform;
        timeBetweenAttacks = character.Data.TimeBetweenAttacks;
        damage = character.Data.AttackDamage;
        spawnPlayerProjectileVisual = character.CharacterType == CharacterType.Player;
    }

    public void MakeDamage(Character target)
    {
        if (target == null)
            return;

        if (!target.LiveComponent.IsAlive)
            return;

        if (Vector3.Distance(target.transform.position, characterTransform.position) > 3)
            return;

        if (lockDamageTime > 0)
        {
            lockDamageTime -= Time.deltaTime;
            return;
        }

        target.LiveComponent.GetDamage(Damage);
        lockDamageTime = timeBetweenAttacks;

        if (spawnPlayerProjectileVisual)
        {
            if (GameAudio.Instance != null)
                GameAudio.Instance.PlayShoot();

            Vector3 from = characterTransform.position + Vector3.up * 0.6f;
            Vector3 to = target.transform.position + Vector3.up * 0.6f;
            AttackProjectileVisual.Spawn(from, to);
        }
    }
}