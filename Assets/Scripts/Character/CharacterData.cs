using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator animator;
    [SerializeField] private WeaponData _starterWeaponData;
    [SerializeField] private int scoreCost;
    
    
    public float speed;
    public int baseHealth;
    public float baseDamage;


    public Transform CharacterTransform => _characterTransform;
    public Animator Animator => animator;
    public CharacterController CharacterController => _characterController;
    public WeaponData StarterWeaponData => _starterWeaponData;
    public int ScoreCost => scoreCost;
}
