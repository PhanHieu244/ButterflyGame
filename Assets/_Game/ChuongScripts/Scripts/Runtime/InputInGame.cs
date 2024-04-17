using System;
using _Game.ChuongScripts.Scripts.Runtime;
using ChuongCustom;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace _Game
{
    public class InputInGame : Singleton<InputInGame>, IPointerDownHandler, IPointerUpHandler
    {
        private IMouseInput _mouseInput;

        private void Start()
        {
            ToGameInput();
        }

        public void ToGameInput()
        {
            _mouseInput = new GameInput(Camera.main);
        }

        [Button]
        public void ToPowerUpInput()
        {
            _mouseInput = new PowerUpInput(Camera.main);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _mouseInput.OnMouseDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _mouseInput.OnMouseUp();
        }
    }
    
    public interface IMouseInput
    {
        void OnMouseUp();
        void OnMouseDown();
    }

    public class PowerUpInput : IMouseInput
    {
        private Camera _camera;
        private Ball _ballDestroy; 

        public PowerUpInput(Camera camera)
        {
            _camera = camera;
        }

        public void OnMouseUp()
        {
            if (_ballDestroy == null) return;
            Object.DestroyImmediate(_ballDestroy.gameObject);
            InputInGame.Instance.ToGameInput();
            Data.Player.powerUp--;
            DestroyPowerUp.Instance.Done();
            //subtract pu
        }

        public void OnMouseDown()
        {
            _ballDestroy = null;
            Vector3 worldMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            worldMousePosition.z = 0f;
            var hit = Physics2D.Raycast(worldMousePosition, Vector2.zero);
            if (hit.collider == null) return;
            var ball = hit.transform.GetComponent<Ball>();
            if (ball == null) return;
            if (ReferenceEquals(ball, Spawner.Instance.CurrentBall)) return;
            _ballDestroy = ball;
        }
    }

    public class GameInput : IMouseInput
    {
        private Camera _camera;

        public GameInput(Camera camera)
        {
            _camera = camera;
        }

        public void OnMouseUp()
        {
            var x = _camera.ScreenToWorldPoint(Input.mousePosition).x;
            Spawner.Instance.MoveAndDrop(x);
        }

        public void OnMouseDown()
        {
            
        }
    }
}