using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBomb : MonoBehaviour
{
    private bool _bombRun;
    private float _yInput;
    private float _playerDir;
    [SerializeField] private float _throwForce = 30;

    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _hitSize = 0.75f;
    private float _groundDeltaX;
    private float _groundDeltaY;
    private float _xOffset;
    private float _yOffset;

    [SerializeField] private GameObject pfBombSplash;
    private GameObject pfCloneSplash;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        _bombRun = true;
        Destroy(GameObject.Find("PaintSplash(Clone)"));

        // animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        if (_bombRun == true)
        {
            _yInput = Input.GetAxisRaw("Vertical");
            if (_yInput < 0)
            {
                _rigidBody.AddForce(new Vector2(0, -1), ForceMode2D.Impulse);
                _bombRun = false;
            }
            else if (_yInput > 0)
            {
                _rigidBody.AddForce(new Vector2(0, _throwForce * 1.5f), ForceMode2D.Impulse);
                _bombRun = false;
            }
            else
            {
                _playerDir = GameObject.Find("Gloomy").transform.localScale.x;
                // _playerDir = _player.transform.localScale.x;
                _rigidBody.AddForce(new Vector2(_playerDir * _throwForce, _throwForce), ForceMode2D.Impulse);
                _bombRun = false;
            }
        }

        Vector2 up = new Vector2(0, _hitSize);
        Vector2 down = new Vector2(0, -1 * _hitSize);
        Vector2 left = new Vector2(-1 * _hitSize, 0);
        Vector2 right = new Vector2(_hitSize, 0);

        RaycastHit2D _yHitUp = Physics2D.Raycast(transform.position, Vector2.up, _hitSize, _ground);
        RaycastHit2D _yHitDown = Physics2D.Raycast(transform.position, Vector2.down, _hitSize, _ground);
        RaycastHit2D _xHitLeft = Physics2D.Raycast(transform.position, Vector2.left, _hitSize, _ground);
        RaycastHit2D _xHitRight = Physics2D.Raycast(transform.position, Vector2.right, _hitSize, _ground);
        RaycastHit2D _groundHitDir = _yHitDown;

        Vector2 _currentPos = transform.position;
        if (_collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (_yHitUp.collider != null)
            {
                _groundHitDir = _yHitUp;
                _groundDeltaY = _currentPos.y - _yHitUp.point.y;
                _yOffset = _currentPos.y - _groundDeltaY;
            }
            else if (_yHitDown.collider != null)
            {
                _groundHitDir = _yHitDown;
                _groundDeltaY = _currentPos.y - _yHitDown.point.y;
                _yOffset = _currentPos.y - _groundDeltaY;
            }
            if (_xHitLeft.collider != null)
            {
                _groundHitDir = _xHitLeft;
                _groundDeltaX = _currentPos.x - _xHitLeft.point.x;
                _xOffset = _currentPos.x - _groundDeltaX;
            }
            else if (_xHitRight.collider != null)
            {
                _groundHitDir = _xHitRight;
                _groundDeltaX = _currentPos.x - _xHitRight.point.x;
                _xOffset = _currentPos.x - _groundDeltaX;
            }

            Destroy(gameObject);
            if (_yHitUp.collider != null || _yHitDown.collider != null)
            {
                pfCloneSplash = Instantiate(pfBombSplash, new Vector2(_currentPos.x, _yOffset), Quaternion.FromToRotation(up, _groundHitDir.normal));
            }
            else if (_xHitLeft.collider != null || _xHitRight.collider != null)
            {
                pfCloneSplash = Instantiate(pfBombSplash, new Vector2(_xOffset, _currentPos.y), Quaternion.FromToRotation(up, _groundHitDir.normal));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Ball Here");
        if (other.collider.tag == "Enemy")
        {
            float[] attackDetails = new float[2];
            attackDetails[0] = 1;
            attackDetails[1] = this.GetComponent<Transform>().transform.position.x;
            other.collider.SendMessageUpwards("Damage", attackDetails);
        }
    }
}