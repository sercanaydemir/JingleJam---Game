using Animations;
using UnityEngine;

namespace Character
{
    public class AttackState
    {
        private AnimationController _animationController;
        
        public AttackState(AnimationController animationController)
        {
            _animationController = animationController;
        }

        public void MeleeLight()
        {
            _animationController.SetTrigger("melee");
        }
        
        public void MeleeHeavy()
        {
            
        }
        
        
    }
}