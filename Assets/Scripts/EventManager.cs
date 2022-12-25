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

        public delegate void Damage(IDamagable damagable,float value);
        public static event Damage OnDamage;

        public void Damaged(IDamagable iDamagable,float value =0)
        {
            OnDamage?.Invoke(iDamagable,value);
        }

        public delegate void HitBox();

        public static event HitBox OnHitBoxUpdate;

        public void HitBoxUpdate()
        {
            OnHitBoxUpdate?.Invoke();
        }
    }
}