using UnityEngine;

public class ForwardProjectileController : ProjectileController
{
	[SerializeField] private Rigidbody rigidbody;
	
	
	protected override void OnMove()
	{
		rigidbody ??= GetComponent<Rigidbody>();
		rigidbody.velocity = projectileDirection * projectileSpeed * Time.deltaTime;
	}
}
