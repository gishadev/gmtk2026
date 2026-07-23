using System.Collections.Generic;
using gishadev.gmtk.Core;
using gishadev.walkingSimulator.MovementManager.Modules;
using UnityEngine;
using VContainer;

namespace gishadev.walkingSimulator.MovementManager
{
    [RequireComponent(typeof(CharacterController))]
    public class KinematicCharacterMovement : MonoBehaviour
    {
        [SerializeField] private Transform groundCheckerPoint;
        [SerializeField] private bool jumpModule;
        [SerializeField] private bool crouchModule;
        [SerializeField] private bool sprintModule;

        [Inject] private CharacterMovementDataSO _characterMovementDataSO;
        [Inject] private IPlayerInputService _playerInputService;

        private readonly List<IMovementModule> _movementModules = new();
        private KinematicPlayerMovementContext _context;

        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _context = new KinematicPlayerMovementContext(_characterController, transform, _playerInputService,
                _characterMovementDataSO);

            _movementModules.Add(new KinematicWalkModule(_context, groundCheckerPoint));
            _movementModules.Add(new MovingPlatformModule(_context, groundCheckerPoint));

            if (crouchModule)
                _movementModules.Add(new CrouchMovementModule(_context));
            if (jumpModule)
                _movementModules.Add(new KinematicJumpModule(_context));
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