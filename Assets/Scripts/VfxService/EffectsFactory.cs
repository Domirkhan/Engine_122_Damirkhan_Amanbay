using System.Collections.Generic;
using UnityEngine;


namespace ZombieIo.EffectsSystem
{
	public class EffectsFactory : MonoBehaviour
	{
		[SerializeField] private EffectsLibrary effectsLibrary;
		
		private Dictionary<EffectType, List<ProjectileController>> projectiles = new Dictionary<EffectType, List<ProjectileController>>();


		public ProjectileController GetProjectile(EffectType effectType)
		{
			if (!projectiles.ContainsKey(effectType))
			{
				projectiles.Add(effectType, new List<ProjectileController>());
				return CreateProjectile(effectType);
			}
			
			ProjectileController projectile = null;
			foreach (var createdProjectile in projectiles[effectType])
			{
				if (createdProjectile.gameObject.activeSelf)
					continue;

				projectile = createdProjectile;
			}
			
			return projectile == null
				? CreateProjectile(effectType)
				: projectile;
		}
		
		private ProjectileController CreateProjectile(EffectType effectType)
		{
			ProjectileController projectile = null;

			foreach (var projectileData in effectsLibrary.ProjectileDatas)
			{
				if (projectileData.EffectType != effectType)
					continue;
				
				projectile = GameObject.Instantiate<ProjectileController>(projectileData.ProjectilePrefab, this.gameObject.transform);
				projectiles[effectType].Add(projectile);
				return projectile;
			}
			
			Debug.LogError($"Unknown projectile type {effectType}");
			return null;
		}
	}
}