using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{

    public enum FollowType
    {
        MoveTowards,
        Lerp
    }

    public FollowType Type = FollowType.MoveTowards;
    public PathDefinition Path;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;

    private IEnumerator<Transform> _currentPoints;

    public void Start()
    {
        if (Path == null)
        {
            Debug.LogError("Path cannot be null", gameObject);
            return;
        }
        _currentPoints = Path.GetPathEnumerator();
        _currentPoints.MoveNext();

        if (_currentPoints.Current == null)
            return;

        transform.position = _currentPoints.Current.position;
    }



    // Update is called once per frame
    void Update()
    {
        if (_currentPoints == null || _currentPoints.Current == null)
            return;

        if (Type == FollowType.MoveTowards)
            transform.position = Vector3.MoveTowards(transform.position, _currentPoints.Current.position, Time.deltaTime * Speed);
        else if (Type == FollowType.Lerp)
            transform.position = Vector3.Lerp(transform.position, _currentPoints.Current.position, Time.deltaTime * Speed);

        float distanceSquared = (transform.position - _currentPoints.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
            _currentPoints.MoveNext();
    }
}
