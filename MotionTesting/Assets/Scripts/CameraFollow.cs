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

    private void Update()
    {
        _playerPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
        if (_player.transform.localScale.x > 0f){
            _playerPosition = new Vector3(_playerPosition.x + _offset, _playerPosition.y + _offset, _playerPosition.z);
        }
        else{
            _playerPosition = new Vector3(_playerPosition.x - _offset, _playerPosition.y - _offset, _playerPosition.z);
        }
        transform.position = Vector3.Lerp(transform.position, _playerPosition, _offsetSmoothing * Time.deltaTime);
    }
}
