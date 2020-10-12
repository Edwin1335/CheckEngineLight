    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject _player;
    [SerializeField]
    private float _offset;
    private Vector3 _playerPosition;
    [SerializeField]
    private float _offsetSmoothing;

    private void Start(){
        Application.targetFrameRate = 300;
    }

    //Attaches camera to player object and moves with them with a slight smoothing
    private void Update()
    {
        //Collects player position with 3D vector, since camera requires a 3D vector (I think?)
        _playerPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);

        //Applies slight camera offset, which can be defined in Unity Inspector with _offset
        if (_player.transform.localScale.x > 0f){
            _playerPosition = new Vector3(_playerPosition.x + _offset, _playerPosition.y + _offset, _playerPosition.z);
        }
        else{
            _playerPosition = new Vector3(_playerPosition.x - _offset, _playerPosition.y - _offset, _playerPosition.z);
        }

        //Makes camera follow player using _playerPosition parameter with a slight smoothing.
        //_offsetSmoothing can be defined in Unity Inspector.
        transform.position = Vector3.Lerp(transform.position, _playerPosition, _offsetSmoothing * Time.deltaTime);
    }
}
