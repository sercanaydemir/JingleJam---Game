using UnityEngine;

namespace Animations
{
    public class AnimationController
    {
        private Animator _animator;

        public AnimationController(Animator animator)
        {
            _animator = animator;
        }

        public void SetFloat(string key, float value)
        {
            _animator.SetFloat(key,value);
        }
        
        public void SetTrigger(string key)
        {
            _animator.SetTrigger(key);
        }
    }
}