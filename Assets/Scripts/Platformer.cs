using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// turn these comments into trello
// need Effect [i.e. Buffs] and Cooldown Managers #fy 01-15
// fix player grounding checking to fit the actual player character # fy 01-15
// give a good pass over scope && lvls of abstraction l8r #fy 01-15

public class Platformer : MonoBehaviour
{
    // physical components
    public Rigidbody2D _rigidbody;
    [SerializeField] Transform _groundBumper;

    // stats
    [SerializeField] float _baseSpeed;
    [SerializeField] public float _maxSpeed;
    [SerializeField] int _baseJumps;
    [SerializeField] int _jumpsRemaining;
    [SerializeField] float _jumpMultiplier;
    [SerializeField] float _jumpCooldown;
    [SerializeField] float _leapDistance;
    [SerializeField] float _distanceToMidair;
    float _jumpBegan;
    [SerializeField] float _lastTimeGrounded;
    [SerializeField] float _groundedForgivenessTime;

    // Debug Variables
    public bool pressingRight = false;
    public bool pressingLeft = false;
    public bool pressingJump = false;

    // Is Functions
    public bool IsGrounded
    {
        get
        {
            Collider2D groundPing = Physics2D.OverlapCircle(_groundBumper.position, _distanceToMidair, groundLayer);
            return groundPing != null ? true : false;
        }
    }
    public bool IsFalling
    {
        get
        {
            return _rigidbody.velocity.y < 0;
        }
    }
    public bool IsRising
    {
        get
        {
            return _rigidbody.velocity.y > 0;
        }
    }
    public bool HasGoodFooting
    {
        get
        {
            return IsGrounded || Time.time - _lastTimeGrounded <= _groundedForgivenessTime;
        }
        set {}
    }
    bool _jumpOffCooldown
    {
        get
        {
            return Time.time - _jumpBegan > _jumpCooldown;
        }
        set {}
    }
    public bool CanJump
    {
        get
        {
            return this.HasGoodFooting || (_jumpsRemaining > 0 && _jumpOffCooldown);
        }
    }

    // external/world
    [SerializeField] LayerMask groundLayer;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _jumpsRemaining = _baseJumps;
    }

    // called once per frame
    void Update()
    {
        if (IsGrounded)
        {
            _jumpsRemaining = _baseJumps;
            _lastTimeGrounded = Time.time;
        }
        Move();
    }

    void Move()
    {
        //Debug.Log($"x movement before applying move: {rigidbody.velocity.x}");
        float startingXMovement = _rigidbody.velocity.x;
        
        float xDirection = pressingRight == false ? (pressingLeft == false ? Input.GetAxisRaw("Horizontal") : -1) : 1;
        //Debug.Log($"x direction: {xDirection}");
        float xDesiredMovement = xDirection * _baseSpeed;
        //Debug.Log($"xDesiredMovement: {xDesiredMovement}");
        float xMovementWithMomentum = xDesiredMovement + startingXMovement;
        //Debug.Log($"xMovementWithMomentum: {xMovementWithMomentum}");

        //move towards a static value, in case player is slowing to a stop or has been pushed
        float xMovement = 0;
        if (xMovementWithMomentum > _maxSpeed)
        {
            //Debug.Log($"moving too fast to the right, applying drag");
            xMovement = xMovementWithMomentum - _baseSpeed;
        }
        else if (xMovementWithMomentum < -1*_maxSpeed)
        {
            //Debug.Log($"moving too fast to the left, applying drag");
            xMovement = xMovementWithMomentum + _baseSpeed;
        }
        else
        {
            xMovement = xMovementWithMomentum;
        }

        _rigidbody.velocity = new Vector2(xMovement, _rigidbody.velocity.y);
        //Debug.Log($"x movement after applying move: {rigidbody.velocity}");

        Jump();
    }

    void Jump()
    {
        //Debug.Log($"jump function entered. pressing jump debug bool: {pressingJump}");
        bool tryingToJump = pressingJump == false ? Input.GetKeyDown(KeyCode.Space) : true;
        //Debug.Log($"trying to jump: {tryingToJump}");

        if (CanJump && tryingToJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _leapDistance);
            _jumpBegan = Time.time;
            _jumpsRemaining--;
        }
        if (IsFalling) _rigidbody.velocity += Physics2D.gravity * Time.deltaTime;
        else if (IsRising && !tryingToJump) _rigidbody.velocity += Vector2.up * Physics2D.gravity * Time.deltaTime * _jumpMultiplier;
    }
}
