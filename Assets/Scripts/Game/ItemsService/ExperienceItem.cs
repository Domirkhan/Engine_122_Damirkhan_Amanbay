using UnityEngine;

namespace ZombieIo.Items
{
	public class ExperienceItem : Item
	{
		[Space] [SerializeField]
		private int addedExperience;
		[SerializeField]
		private ParticleSystem fireParticle;
		
		private ItemsService itemsService;


		public override void Initialize(Vector3 position)
		{
			base.Initialize(position);
			fireParticle.Play();
		}
		
		protected override void FlyToTargetComplete()
		{
			fireParticle.Stop();
			GameManager.Instance.SessionExperienceManager.Experience += addedExperience;
			GameManager.Instance.ItemsService.RemoveItem(this);
		}
	}
}