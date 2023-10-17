using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    [Header("Bounds")]
    [SerializeField] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    [SerializeField] private Transform _lowerBound;

    private float _height;
    private float _width;

    private void Start()
    {
        _height = Camera.main.orthographicSize;
        _width = _height * Screen.width / Screen.height;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = Vector2.Lerp(transform.position, _target.position, _speed * Time.fixedDeltaTime);
        Vector2 leftLowerCorner = new Vector2(newPosition.x - _width, newPosition.y - _height);
        Vector2 rightUpCorner = new Vector2(newPosition.x + _width, newPosition.y + _height);

        if (leftLowerCorner.x < _leftBound.position.x)
            newPosition.x = transform.position.x;

        if (rightUpCorner.x > _rightBound.position.x)
            newPosition.x = transform.position.x;

        if (leftLowerCorner.y < _lowerBound.position.y)
            newPosition.y = transform.position.y;

        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}