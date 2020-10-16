using UnityEngine;


namespace TestRectangle
{
    public sealed class MouseController : MonoBehaviour
    {
        #region Fields

        private const int COLLIDER_OBJECT_SIZE = 10;
        private GameObject _rectangle;
        private float _distance = 20f;
        private Collider[] _collidedObjects;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _rectangle = Resources.Load<GameObject>("Prefabs/Rectangle");
            _collidedObjects = new Collider[COLLIDER_OBJECT_SIZE];
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool isHit = Physics.Raycast(ray, out RaycastHit hit, _distance);
                if (!isHit)
                {
                    var endPoint = ray.origin + ray.direction * _distance;
                    if (CanCreate(endPoint) == 0)
                    {
                        var newRectangle = Instantiate(_rectangle, endPoint, Quaternion.identity);
                        var material = newRectangle.GetComponent<Renderer>().material;
                        material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
                    }
                }
            }
        }

        #endregion


        #region Methods

        private int CanCreate(Vector3 centerPoint)
        {
            var size = _rectangle.transform.localScale / 2;
            var collidersCount = Physics.OverlapBoxNonAlloc(centerPoint, size, _collidedObjects);
            return collidersCount;
        }
        #endregion
    }
}