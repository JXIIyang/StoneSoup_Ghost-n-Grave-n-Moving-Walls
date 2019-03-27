using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeagueCappyPlayer : Player
{
    public GameObject possessed;
    public GameObject playerState;
    public GameObject cappyPrefab;

    void Awake()
    {
        _instance = this;
        addTag(TileTags.Player);
    }

    void Update()
    {
        if (GameManager.instance.gameIsOver)
        {
            return;
        }

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
            int numObjects = Physics2D.OverlapPointNonAlloc(possessed.transform.position + Vector3.up * 2, _maybeColliderResults);
            bool instantiated = false;
            GameObject newPlayer = null;
            if (_maybeColliderResults.Length == 0)
            {
                newPlayer = Instantiate(playerState, possessed.transform.position + Vector3.up * 2, Quaternion.identity, transform.parent.parent);
                instantiated = true;
            }
            else
            {
                numObjects = Physics2D.OverlapPointNonAlloc(possessed.transform.position + Vector3.right * 2, _maybeColliderResults);
                if (_maybeColliderResults.Length == 0)
                {
                    newPlayer = Instantiate(playerState, possessed.transform.position + Vector3.right * 2, Quaternion.identity, transform.parent.parent);
                    instantiated = true;
                }
                else
                {
                    numObjects = Physics2D.OverlapPointNonAlloc(possessed.transform.position + Vector3.down * 2, _maybeColliderResults);
                    if (_maybeColliderResults.Length == 0)
                    {
                        newPlayer = Instantiate(playerState, possessed.transform.position + Vector3.down * 2, Quaternion.identity, transform.parent.parent);
                        instantiated = true;
                    }
                    else
                    {
                        numObjects = Physics2D.OverlapPointNonAlloc(possessed.transform.position + Vector3.left * 2, _maybeColliderResults);
                        if (_maybeColliderResults.Length == 0)
                        {
                            newPlayer = Instantiate(playerState, possessed.transform.position + Vector3.left * 2, Quaternion.identity, transform.parent.parent);
                            instantiated = true;
                        }
                    }
                }
            }
            if (instantiated && newPlayer != null)
            {
                Instantiate(cappyPrefab).GetComponent<Tile>().pickUp(newPlayer.GetComponent<Tile>());
                Destroy(gameObject);
            }
        }
        transform.parent = possessed.transform;
        updateSpriteSorting();
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
        possessed.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        possessed.GetComponent<Tile>().addForce(attemptToMoveDir * moveSpeed * 200);
        
    }

    public void UpdatePlayer()
    {
        _instance = this;
    }
}
