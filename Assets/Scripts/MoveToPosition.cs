using UnityEngine;

public class MoveToPosition : MonoBehaviour{

    [SerializeField] private float moveSpeed;
    
    private Transform _playerTransform;
    private Vector2 _position;
    
    private void Awake(){
        _playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        _position = transform.position;
    }

    private void FixedUpdate(){
        var step = moveSpeed * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, step);
    }
}
