using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Character
{
    public class RagdollController : MonoBehaviour
    {
        private List<Rigidbody> rbs = new List<Rigidbody>();
        private List<Collider> _colliders = new List<Collider>();

        private Animator _animator;

        private void Awake()
        {
            rbs = GetComponentsInChildren<Rigidbody>().ToList();
            _colliders = GetComponentsInChildren<Collider>().ToList();
            _animator = GetComponent<Animator>();
            DeactivateRagdoll();
        }

        public void DeactivateRagdoll()
        {
            foreach (var rb in rbs)
            {
                rb.isKinematic = true;
            }
            
            foreach (var c in _colliders)
            {
                c.enabled = false;
            }

            _animator.enabled = true;
        }

        public void ActivateRagdoll()
        {
            foreach (var rb in rbs)
            {
                rb.isKinematic = false;
            }
            
            foreach (var c in _colliders)
            {
                c.enabled = true;
            }

            _animator.enabled = false;
        }
    }
}