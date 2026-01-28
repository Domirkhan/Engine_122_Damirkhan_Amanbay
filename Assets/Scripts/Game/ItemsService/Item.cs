using UnityEngine;

namespace ZombieIo.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private ItemsService.ItemClass itemClass;
        [SerializeField] private float maxFlySpeed = 3.0f;
        
        private bool isMovementToPlayer;
        private Transform playerTarget;
        private float currentFlySpeed = 0.0f;
        private float distanceForPickup = 0.0f;
        
        public bool IsActive => gameObject.activeSelf;
        public ItemsService.ItemClass ItemClass => itemClass;
        
        
        public virtual void Initialize(Vector3 position)
        {
            playerTarget = GameManager.Instance.CharacterFactory.PlayerCharacter.transform;
            isMovementToPlayer = false;
            transform.position = position;
            gameObject.SetActive(true);
        }
        
        public void SetDistanceForPick(float distanceForPickup)
        {
            this.distanceForPickup = distanceForPickup;
        }

        public void ActivateFlyingToTarget()
        {
            isMovementToPlayer = true;
            currentFlySpeed = 0.0f;
        }

        public void OnUpdate()
        {
            var distance = Vector3.Distance(playerTarget.position, transform.position);
            
            if (isMovementToPlayer)
            {
                if (currentFlySpeed < maxFlySpeed)
                {
                    currentFlySpeed += Time.deltaTime * maxFlySpeed;
                    if (currentFlySpeed > maxFlySpeed)
                        currentFlySpeed = maxFlySpeed;
                }

                Vector3 direction = (playerTarget.position - transform.position).normalized;
                transform.position += direction * currentFlySpeed * Time.deltaTime;
                if (distance <= 0.2f)
                    FlyToTargetComplete();
            }
            else
            {
                transform.rotation *= Quaternion.Euler(0.0f, 1 * Time.deltaTime, 0.0f);
                if (distance <= distanceForPickup)
                    ActivateFlyingToTarget();
            }
        }
        
        protected abstract void FlyToTargetComplete();
    }
}