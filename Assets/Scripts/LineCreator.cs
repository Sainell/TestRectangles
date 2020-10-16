using UnityEngine;


namespace TestRectangle
{
    public sealed class LineCreator : MonoBehaviour
    {
        #region Fields

        private Rectangle _currentRectangle;


        #endregion


        #region UnityMethods

        private void Awake()
        {
            Rectangle.CreateLineEvent += SetCurrentLine;
            Rectangle.SetLineEndPointEvent += SetLineEndPoint;
        }

        private void OnDisable()
        {
            Rectangle.CreateLineEvent -= SetCurrentLine;
            Rectangle.SetLineEndPointEvent -= SetLineEndPoint;
        }


        #endregion


        #region Methods

        private void SetCurrentLine(Rectangle currentRectangle)
        {
            _currentRectangle = currentRectangle;
        }

        private void SetLineEndPoint(GameObject rectangle)
        {
            if (_currentRectangle != null)
            {
                _currentRectangle.SetSelectedRectangle(rectangle);
            }
        }

        #endregion
    }
}