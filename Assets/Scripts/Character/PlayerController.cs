using System;
using Animations;
using Core;
using DefaultNamespace;
using InputSystem;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

namespace Character
{
    public class PlayerController : MonoBehaviour, IHealth,IDamagable
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationDamping;

        [Header("FOV")]
        [SerializeField] private float r;
        [SerializeField] private LayerMask layer;

        private Vector3 _movementVector;
        private InputReader _inputReader;
        private CharacterController _characterController;
        private AnimationController _animationController;
        private RagdollController _ragdollController;
        private PlayerStates _currentState;
        private AttackState _attackState;
        
        private void Awake()
        {
            _inputReader = new InputReader();
            _characterController = GetComponentInChildren<CharacterController>();
            _animationController = new AnimationController(GetComponentInChildren<Animator>());
            _attackState = new AttackState(_animationController);
            _ragdollController = GetComponentInChildren<RagdollController>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(CheckPlayerFov),1,0.25f);
            EventManager.Instance.HealthChanged(Health);
        }

        private void OnEnable()
        {
            EventManager.OnHitBoxUpdate += HitBoxActive;
        }

        private void OnDisable()
        {
            EventManager.OnHitBoxUpdate += HitBoxActive;
        }

        private void FixedUpdate()
        {
            if(_currentState == PlayerStates.Dead) return;
            
            _movementVector = new Vector3(_inputReader.MovementVector.x, 0, _inputReader.MovementVector.y);
            SimpleMovement();
        }

        private void LateUpdate()
        {
            _animationController.SetFloat("speed",_movementVector.magnitude);
        }

        public void CheckPlayerFov()
        {
            Collider[] c = new Collider[1];
            int enemyCount = Physics.OverlapSphereNonAlloc(transform.position, r, c, layer);

            if (enemyCount > 0 && _currentState == PlayerStates.FreeLookMovement)
            {
                _currentState = PlayerStates.Attack;
                CheckState();
            }
            else if(enemyCount == 0 && _currentState == PlayerStates.Attack)
            {
                _currentState = PlayerStates.FreeLookMovement;
                CheckState();
            }
        }
        
        #region Movement

        void SimpleMovement()
        {
            _characterController.SimpleMove(_movementVector*speed*Time.fixedDeltaTime);
            FaceMovementDirection(_movementVector);
        }
        
        void FaceMovementDirection(Vector3 movement)
        {
            if (_inputReader.MovementCanceled) return;
            
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(movement), 
                Time.deltaTime*rotationDamping);

        }
        #endregion

        #region StateMachine
        
        private void CheckState()
        {
            switch (_currentState)
            {
                case PlayerStates.FreeLookMovement:
                    _inputReader.meleeAttackLight -= _attackState.MeleeLight;
                    _animationController.SetTrigger("freeLook");
                    break;
                case PlayerStates.Attack:
                    _inputReader.meleeAttackLight += _attackState.MeleeLight;
                    _animationController.SetTrigger("attack");
                    break;
                case PlayerStates.Dead:
                    _inputReader.meleeAttackLight -= _attackState.MeleeLight;
                    break;
            }
            
            
        }
        #endregion

        #region Editor

        private void OnDrawGizmos()
        {
            if (r > 0)
            {

                Gizmos.color = Color.red;

                Gizmos.DrawWireSphere(transform.position, r);
            }

            if (swordCenter != null)
            {
                Gizmos.color = Color.yellow;
                
                Gizmos.DrawWireCube(swordCenter.position,size);
            }
        }

        #endregion

        [Header("Sword & HitBox")] 
        public Transform swordCenter;

        public LayerMask enemyLayer;
        public Vector3 size;
        public Vector3 rotation;
        void HitBoxActive()
        {
            Collider[] c = Physics.OverlapBox(swordCenter.position, size,Quaternion.Euler(swordCenter.eulerAngles),layer);
            
            if (c.Length > 0)
            {
                IDamagable d = c[0].GetComponent<IDamagable>();
                
                if(d != null)
                    EventManager.Instance.Damaged(d);
            }
        }

        [field: SerializeField] public float Health { get; set; }
        public void ChangeHealth(float value)
        {
            Health -= value;
            EventManager.Instance.HealthChanged(Health);
            if (Health <= 0)
                Dead();
        }

        private void Dead()
        {
            _ragdollController.ActivateRagdoll();
            _currentState = PlayerStates.Dead;
            CheckState();
            
            EventManager.Instance.SetNotifText("GG Boy!");
        }

        public void Damage(IDamagable damagable,float value)
        {
            if (ReferenceEquals(damagable, this))
            {
                ChangeHealth(value);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CenterArea"))
            {
                EventManager.Instance.SetNotifText("Defend The Damacana!");
            }
        }

        public void RegenerateHealth()
        {
            Health = 100;
            
            EventManager.Instance.HealthChanged(Health);
        }
    }
}