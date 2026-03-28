using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int scoreCost;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float timeBetweenAttacks = 0.35f;
    [SerializeField] private float attackDamage = 5f;

    public float DefaultSpeed => speed;
    public int ScoreCost => scoreCost;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController => characterController;

    public float TimeBetweenAttacks => timeBetweenAttacks;
    public float AttackDamage => attackDamage;
}
