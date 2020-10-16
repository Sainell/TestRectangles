using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace TestRectangle
{
    public sealed class Rectangle : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        #region Fields

        public static Action<Rectangle> CreateLineEvent;
        public static Action<GameObject> SetLineEndPointEvent;

        private float _distance = 20f;
        private LineRenderer _line;
        private GameObject _selectedRectangle;
        private Rectangle _rectangle;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _rectangle = GetComponent<Rectangle>();
            _line = GetComponent<LineRenderer>();
            _line.startWidth = 0.3f;
            _line.endWidth = 0.3f;
            _line.enabled = false;

        }

        private void Update()
        {
            if (_selectedRectangle != null)
            {
                _line.SetPosition(0, transform.position);
                _line.SetPosition(1, _selectedRectangle.transform.position);
                _line.enabled = true;
            }
            else
            {
                _line.enabled = false;
            }
        }

        #endregion


        #region Methods

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = GetPosition();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.clickCount > 1 & eventData.button == PointerEventData.InputButton.Left)
            {
                LeftDoubleClick();
            }

            if (eventData.button == PointerEventData.InputButton.Middle)
            {
                MiddleClick();
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                RightClick();
            }
        }

        private void LeftDoubleClick()
        {
            Destroy(gameObject);
        }

        private void RightClick()
        {
            SetLineEndPointEvent(gameObject);         
        }
        
        private void MiddleClick()
        {
            CreateLineEvent?.Invoke(_rectangle);
        }
           
        private Vector3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var position = ray.origin + ray.direction * _distance;
            return position;
        }

        public void SetSelectedRectangle(GameObject rectangle)
        {
            _selectedRectangle = rectangle;
        }

        #endregion
    }
}