using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeagueCappyPlayer : Player
{
    public Component[] possessed;
    public GameObject playerState;
    public GameObject cappy;
    public Vector3 cappyOffset;

    void Awake()
    {
        _instance = this;
        addTag(TileTags.Player);
    }

    public override void init()
    {
        base.init();
        GetComponent<Collider2D>().isTrigger = false;
        if (GetComponent<BoxCollider2D>() != null)
        {
            GetComponent<BoxCollider2D>().size = .98f * GetComponent<BoxCollider2D>().size;
        }
    }

    void Update()
    {
        if (GameManager.instance.gameIsOver)
        {
            return;
        }

        cappy.transform.position = transform.position + cappyOffset;

        // Update our aim direction
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 toMouse = (mousePosition - (Vector2)transform.position).normalized;
        aimDirection = toMouse;

        // Update our invincibility frame counter.
        if (_iFrameTimer > 0)
        {
            _iFrameTimer -= Time.deltaTime;
            _sprite.enabled = !_sprite.enabled;
            if (_iFrameTimer <= 0)
            {
                _sprite.enabled = true;
            }
        }

        // If we press space, we're unpossessing the object.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int numObjects = Physics2D.OverlapPointNonAlloc(transform.position + Vector3.up * 2, _maybeColliderResults);
            bool instantiated = false;
            if (_maybeColliderResults[0] == null)
            {
                playerState.transform.position = transform.position + Vector3.up * 2;
                playerState.SetActive(true);
                instantiated = true;
            }
            else
            {
                numObjects = Physics2D.OverlapPointNonAlloc(transform.position + Vector3.right * 2, _maybeColliderResults);
                if (_maybeColliderResults[0] == null)
                {
                    playerState.transform.position = transform.position + Vector3.right * 2;
                    playerState.SetActive(true);
                    instantiated = true;
                }
                else
                {
                    numObjects = Physics2D.OverlapPointNonAlloc(transform.position + Vector3.down * 2, _maybeColliderResults);
                    if (_maybeColliderResults[0] == null)
                    {
                        playerState.transform.position = transform.position + Vector3.down * 2;
                        playerState.SetActive(true);
                        instantiated = true;
                    }
                    else
                    {
                        numObjects = Physics2D.OverlapPointNonAlloc(transform.position + Vector3.left * 4, _maybeColliderResults);
                        if (_maybeColliderResults[0] == null)
                        {
                            playerState.transform.position = transform.position + Vector3.left * 4;
                            playerState.SetActive(true);
                            instantiated = true;
                        }
                    }
                }
            }
            if (instantiated)
            {
                playerState.GetComponent<Player>().health = health;
                playerState.transform.parent = transform.parent;
                _instance = playerState.GetComponent<Player>();
                cappy.transform.parent = transform.parent;
                cappy.AddComponent<TeagueCappy>().pickUp(playerState.GetComponent<Tile>());
                gameObject.SetActive(false);
            }
        }
        updateSpriteSorting();
    }

    private void OnDestroy()
    {
        
    }

    void FixedUpdate()
    {
        // Let's move via the keyboard controls

        bool tryToMoveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool tryToMoveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool tryToMoveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        bool tryToMoveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

        Vector2 attemptToMoveDir = Vector2.zero;

        if (tryToMoveUp)
        {
            attemptToMoveDir += Vector2.up;
        }
        else if (tryToMoveDown)
        {
            attemptToMoveDir -= Vector2.up;
        }
        if (tryToMoveRight)
        {
            attemptToMoveDir += Vector2.right;
        }
        else if (tryToMoveLeft)
        {
            attemptToMoveDir -= Vector2.right;
        }
        attemptToMoveDir.Normalize();

        // Finally, here's where we actually move.
        moveViaVelocity(attemptToMoveDir, moveSpeed, moveAcceleration);
    }

    public void UpdatePlayer()
    {
        _instance = this;
    }
}
