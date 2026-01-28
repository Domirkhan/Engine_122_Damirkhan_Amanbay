using System;
using UnityEngine;


namespace ZombieIo.EffectsSystem
{
    [CreateAssetMenu(menuName = "Effects/Effects Library")]
    public class EffectsLibrary : ScriptableObject
    {
        [SerializeField] public EffectData[] effectsData;
        [SerializeField] public ProjectileData[] particleDatas;


        public EffectData[] EffectDatas => effectsData;
        public ProjectileData[] ProjectileDatas => particleDatas;
        

        
        [Serializable]
        public class EffectData
        {
            [SerializeField] private EffectType effectType;
            [SerializeField] private ParticleSystem effectPrefab;


            public EffectType EffectType => effectType;
            public ParticleSystem EffectPrefab => effectPrefab;
        }

        [Serializable]
        public class ProjectileData
        {
            [SerializeField] private EffectType effectType;
            [SerializeField] private ProjectileController projectilePrefab;
            

            public EffectType EffectType => effectType;
            public ProjectileController ProjectilePrefab => projectilePrefab;
        }
    }
}