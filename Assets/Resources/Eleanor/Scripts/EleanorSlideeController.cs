using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleanorSlideeController : apt283BFSEnemy
{
    // Start is called before the first frame update

    public float CasualTime;
    public float RushTime;
    private bool _rush;
    private bool _chase;    
    private float _timer;
    private bool _follow;


    public override void Start()
    {
        base.Start();
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        
    }
    
    public override void Update()
    {
        _timeSinceLastStep += Time.deltaTime;
        Vector2 targetGlobalPos = Tile.toWorldCoord(_targetGridPos.x, _targetGridPos.y);
        float distanceToTarget = Vector2.Distance(transform.position, targetGlobalPos);
        if (distanceToTarget <= GRID_SNAP_THRESHOLD || _timeSinceLastStep >= 2f) {
            takeStep();
        }
        updateSpriteSorting();
        if (_chase)
        {
            _timer += Time.deltaTime;
            if (!_rush && _timer > CasualTime)
            {
                _timer = 0;
                _rush = true;
            }

            if (_rush && _timer > RushTime)
            {
                _timer = 0;
                _rush = false;
            }
            
        }

    }
    
    
    public override void FixedUpdate()
    {
            Vector2 targetGlobalPos = Tile.toWorldCoord(_targetGridPos.x, _targetGridPos.y);
            if (Vector2.Distance(transform.position, targetGlobalPos) >= 0.1f || _follow)
            {
                // If we're away from our target position, move towards it.
                Vector2 toTargetPos = (targetGlobalPos - (Vector2) transform.position).normalized;
                _chase = true;
                if (_rush)
                {
                    moveViaVelocity(toTargetPos, moveSpeed * 3, moveAcceleration);
                    _anim.SetInteger("MovingStatus", 2);
                }
                else
                {
                    moveViaVelocity(toTargetPos, moveSpeed, moveAcceleration);
                    _anim.SetInteger("MovingStatus", 1);
                }


                // Figure out which direction we're going to face. 
                // Prioritize side and down.
                if (_anim != null)
                {
                    if (toTargetPos.x >= 0)
                    {
                        _sprite.flipX = false;
                    }
                    else
                    {
                        _sprite.flipX = true;
                    }

//                if (Mathf.Abs(toTargetPos.x) > 0 && Mathf.Abs(toTargetPos.x) > Mathf.Abs(toTargetPos.y)) {
//                    _anim.SetInteger("Direction", 1);
//                }
//                else if (toTargetPos.y > 0 && toTargetPos.y > Mathf.Abs(toTargetPos.x)) {
//                    _anim.SetInteger("Direction", 0);
//                }
//                else if (toTargetPos.y < 0 && Mathf.Abs(toTargetPos.y) > Mathf.Abs(toTargetPos.x)) {
//                    _anim.SetInteger("Direction", 2);
//                }
                }
            }
            else
            {
                moveViaVelocity(Vector2.zero, 0, moveAcceleration);
                _timer = 0;
                _rush = false;
                if (_anim != null)
                {
                    _anim.SetInteger("MovingStatus", 0);
                    _chase = false;
                }
            }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Tile maybeTile = other.GetComponent<Tile>();
        if (maybeTile == null) return;
        if (maybeTile.tileName.Contains("Ghost"))
        {
            _follow = true;
        }
       
    }
}
