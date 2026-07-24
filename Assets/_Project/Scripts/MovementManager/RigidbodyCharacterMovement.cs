using System.Collections.Generic;
using gishadev.gmtk.Input;
using gishadev.gmtk.Core;
using gishadev.gmtk.MovementManager.Contexts;
using gishadev.gmtk.MovementManager.Modules;
using gishadev.gmtk.MovementManager.Modules.Rigidbody;
using UnityEngine;
using VContainer;

namespace gishadev.gmtk.MovementManager
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyCharacterMovement : MonoBehaviour
    {
        [SerializeField] private Transform groundCheckerPoint;
        [SerializeField] private bool jumpModule;
        [SerializeField] private bool crouchModule;
        [SerializeField] private bool sprintModule;

        [Inject] private CharacterMovementDataSO _characterMovementDataSO;
        [Inject] private IPlayerInputService _playerInputService;

        private readonly List<IMovementModule> _movementModules = new();
        private RigidbodyPlayerMovementContext _context;

        private Rigidbody _rigidbody;
        private Collider _collider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            _context = new RigidbodyPlayerMovementContext(_rigidbody, _collider, transform, _playerInputService,
                _characterMovementDataSO);

            _movementModules.Add(new RigidbodyWalkModule(_context, groundCheckerPoint));
            _movementModules.Add(new MovingPlatformModule(_context, groundCheckerPoint));

            if (crouchModule)
                _movementModules.Add(new CrouchMovementModule(_context));
            if (jumpModule)
                _movementModules.Add(new RigidbodyJumpModule(_context));
            if (sprintModule)
                _movementModules.Add(new SprintMovementModule(_context));

            foreach (var module in _movementModules) module.Initialize();
        }

        private void OnDestroy()
        {
            foreach (var module in _movementModules) module.Dispose();
        }

        private void Update()
        {
            foreach (var module in _movementModules)
                module.Tick();
        }

        private void FixedUpdate()
        {
            foreach (var module in _movementModules)
                module.FixedTick();
        }
    }
}