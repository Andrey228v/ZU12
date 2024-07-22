using System.Collections.Generic;
using UnityEngine;

public class PositionChanger : MonoBehaviour
{
    [SerializeField] private Transform _placePoints;
    [SerializeField] private float _speed;

    private List<Transform> _places;
    private int _placePointIndex = 0;
    private Transform _nextPlace;

    private void Start()
    {
        _places = new List<Transform>();

        for (int i = 0; i < _placePoints.childCount; i++)
        {
            _places.Add(_placePoints.GetChild(i).transform);
        }

        _nextPlace = _places[_placePointIndex];
    }

    private void Update()
    {
        if (transform.position == _nextPlace.position)
            ChangePoint();

        transform.position = Vector3.MoveTowards(transform.position, _nextPlace.position, _speed * Time.deltaTime);
    }

    private void ChangePoint()
    {
        _placePointIndex = ++_placePointIndex % _places.Count;

        Vector3 nextPointPlace = _places[_placePointIndex].transform.position;
        transform.forward = nextPointPlace - transform.position;

        _nextPlace = _places[_placePointIndex];
    }
}
