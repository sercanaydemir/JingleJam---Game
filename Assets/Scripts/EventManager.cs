using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public delegate void Damage(IDamagable damagable);
        public static event Damage OnDamage;

        public void Damaged(IDamagable iDamagable)
        {
            OnDamage?.Invoke(iDamagable);
        }

        public delegate void HitBox();

        public static event HitBox OnHitBoxUpdate;

        public void HitBoxUpdate()
        {
            OnHitBoxUpdate?.Invoke();
        }
    }
}