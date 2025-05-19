using System;
using System.Collections.Generic;
using Services.InputHandler;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Player
{
    public class MovementController: MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;

        private readonly Queue<Vector3> _targets = new();
        
        private ITouchInput _touchInput;
        private bool _isMoving;
        private Vector3 _currentTarget;
        private Slider _slider;
        
        private bool _isInitialized;

        [Inject]
        public void Construct(ITouchInput touchInput)
        {
            _touchInput = touchInput;
        }

        public void Initialize(Slider slider)
        {
            _slider = slider;
            
            _isInitialized = true;
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _touchInput.OnScreenTouched += OnTouch;
        }

        private void Unsubscribe()
        {
            _touchInput.OnScreenTouched -= OnTouch;
        }

        private void OnTouch(Vector3 obj)
        {
            _targets.Enqueue(obj);
        }
        
        private void Update()
        {
            if(!_isInitialized)
                return;
            
            if (!_isMoving && _targets.Count > 0)
            {
                _currentTarget = _targets.Dequeue();
                _isMoving = true;
            }

            if (_isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * _slider.value * Time.deltaTime);

                if (Vector3.Distance(transform.position, _currentTarget) < 0.01f)
                {
                    _isMoving = false;
                }
            }
        }
    }
}