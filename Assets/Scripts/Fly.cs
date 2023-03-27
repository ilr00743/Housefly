using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Fly : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private RectTransform[] _points;
    [SerializeField] private Image _image;
    [SerializeField] private Screamer _screamer;
    [SerializeField] private float _speed;
    private RectTransform _currentPoint;
    private RectTransform _transform;
    private int _hitCount;
    private bool _canMove;
    public event Action PointReached;

    private void Start()
    {
        _canMove = true;
        _transform = GetComponent<RectTransform>();
        PointReached += ChangePoint;
        _currentPoint = _points[Random.Range(0, _points.Length)];
    }

    private void Update()
    {
        if (!_canMove) return;

        RotateAndMoveToPoint();
    }

    private void RotateAndMoveToPoint()
    {
        var direction = _currentPoint.anchoredPosition - _transform.anchoredPosition;
        _transform.anchoredPosition = Vector2.MoveTowards(_transform.anchoredPosition,
            _currentPoint.anchoredPosition,
            _speed * Time.deltaTime);
        _transform.rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
        
        if (Vector2.Distance(_transform.anchoredPosition, _currentPoint.anchoredPosition) <= 0.3f)
        {
            PointReached.Invoke();
        }
    }

    private void ChangePoint()
    {
        _currentPoint = _points[Random.Range(0, _points.Length)];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Hit();
    }

    private void Hit()
    {
        _hitCount++;
        _speed += 100;
        StartCoroutine(Cooldown());
        
        if (_hitCount == 12)
        {
            _screamer.Enable();
        }
    }

    private IEnumerator Cooldown()
    {
        _canMove = false;
        _image.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        _canMove = true;
        _image.color = Color.white;
    }
}