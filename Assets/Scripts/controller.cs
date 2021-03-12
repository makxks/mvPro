using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool jumpSuit = true;
    [SerializeField]
    private bool teleSuit = false;
    [SerializeField]
    private bool meleeSuit = false;
    [SerializeField]
    private bool useJumpHold = true;
    [SerializeField]
    float moveSpeed = 1f;
    private float defaultMoveSpeed;
    //make sure jumpForce and moveSpeed can be changed easily through a function as different suits will have different properties
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float forceMod = 200f;
    [SerializeField]
    private bool useForce = true;
    private bool jumped = false;
    private bool jumpReady = true;
    private bool grounded = true;

    [SerializeField]
    private float rememberGroundedFor = 0.2f;
    private float lastTimeGrounded;

    private float primaryRechargeTime = 0;
    private float secondaryRechargeTime = 0;
    //only exists for 1 suit need to make sure that possibility is changeable too
    [SerializeField]
    private int extraJumpCount = 0;
    [SerializeField]
    private int extraJumpsLimit = 1;
    [SerializeField]
    float floatTime;
    [SerializeField]
    private int extraBoostCount = 0;
    [SerializeField]
    private int extraBoostLimit = 2;

    private bool hovering = false;
    private bool hovered = false;

    [SerializeField]
    private float boostTime = 0.4f;
    private float boostTimer;
    [SerializeField]
    private float boostSpeed = 15f;
    private bool boosted = false;
    private bool boosting = false;
    [SerializeField]
    private float smashCooldown = 0;
    [SerializeField]
    private bool smashing = false;
    private bool endingSmash = false;
    private bool smashCD = false;
    private float smashTimer = 0;
    private float currentY = 0;
    private float prevY = 0;

    [SerializeField]
    private bool dashing = false;
    [SerializeField]
    private float dashTime = 0;
    [SerializeField]
    private float dashCooldown = 0;
    private float dashTimer = 0;
    private bool dashCD = false;

    private Rigidbody rb;
    private float timer = 0;

    private Collider[] floor;
    private Collider[] block;

    [SerializeField]
    private GameObject circlePositionIn;
    [SerializeField]
    private LayerMask layer;

    private bool leftMidHit;
    private bool rightMidHit;
    private bool leftTopHit;
    private bool rightTopHit;
    private bool leftBottomHit;
    private bool rightBottomHit;
    private Ray leftMid;
    private Ray rightMid;
    private Ray leftTop;
    private Ray rightTop;
    private Ray leftBottom;
    private Ray rightBottom;

    private AnimationControl animControl;

    private bool facingRight;

    [SerializeField]
    private float teleportCooldown = 3f;
    private float teleportTimer = 0f;
    private bool teleported = false;
    [SerializeField]
    private float teleportDistance = 5f;

    public GameObject teleportMarker;

    [SerializeField]
    private float fadeTime;
    [SerializeField]
    private float fadeWaitTime;
    private float fadeTimer = 0;
    private bool faded;
    private bool fadeWait;

    public GameObject jumpSuitText;
    public GameObject meleeSuitText;
    public GameObject teleSuitText;

    void Start()
    {
        facingRight = true;
        animControl = GetComponent<AnimationControl>();
        rb = GetComponent<Rigidbody>();

        if (jumpSuit)
        {
            jumpForce = 12f;
            moveSpeed = 3f;
            extraJumpCount = 1;
        }
        else if (meleeSuit)
        {
            jumpForce = 6f;
            moveSpeed = 2.5f;
            extraJumpCount = 0;
        }
        else if (teleSuit)
        {
            jumpForce = 9f;
            moveSpeed = 2.75f;
            extraJumpCount = 0;
        }

        if (useForce)
        {
            jumpForce *= 4f;
            moveSpeed /= 2.5f;
        }

        defaultMoveSpeed = moveSpeed;
    }

    private void OnDrawGizmos()
    {
    }

    // Update is called once per frame
    void Update()
    {
        jump();
        jumpRelease();

        if (jumpSuit)
        {
            boost();
            if (boosting)
            {
                boostTimer += Time.deltaTime;
                if (boostTimer > boostTime)
                {
                    endBoost();
                }
            }

            if (hovering)
            {
                timer += Time.deltaTime;
                if (timer > floatTime)
                {
                    stopHover();
                    hovered = true;
                    timer = 0;
                }
            }
        }
        if (meleeSuit)
        {
            smash();
            if (smashing)
            {
                currentY = transform.position.y;
                if(currentY < prevY)
                {
                    StartCoroutine(finishSmash());
                }
                else
                {
                    prevY = currentY;
                }
            }
            if (smashCD)
            {
                smashTimer += Time.deltaTime;
                if(smashTimer > smashCooldown)
                {
                    smashCD = false;
                    smashTimer = 0;
                }
            }
            if (dashing)
            {
                timer += Time.deltaTime;
                if(timer > dashTime)
                {
                    endDash();
                    timer = 0;
                }
            }
            if (dashCD)
            {
                dashTimer += Time.deltaTime;
                if(dashTimer > dashCooldown)
                {
                    dashCD = false;
                    dashTimer = 0;
                }
            }
            dashStrike();
        }
        if (teleSuit)
        {
            teleport();
            teleportMarkerPosition(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            voidFade();
            if (teleported)
            {
                timer += Time.deltaTime;
                if(timer > teleportCooldown)
                {
                    teleported = false;
                    timer = 0;
                }
            }
            if (faded || fadeWait)
            {
                fadeWait = true;
                fadeTimer += Time.deltaTime;
                if(fadeTimer > fadeTime && faded)
                {
                    faded = false; 
                    rb.useGravity = true;
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
                if(fadeTimer > fadeWaitTime)
                {
                    fadeWait = false;
                    fadeTimer = 0;
                }
            }
        }

        checkSideHits();
        checkGrounded();
    }

    private void checkGrounded()
    {
        floor = Physics.OverlapSphere(circlePositionIn.transform.position, 0.1f, layer);
        if (floor.Length > 0)
        {
            resetJump();
            lastTimeGrounded = Time.time;
            if (endingSmash)
            {
                smashing = false;
                endingSmash = false;
            }
        }
        else
        {
            grounded = false;
            jumped = true;
            jumpReady = false;
        }
    }

    private void checkSideHits()
    {
        if (!grounded)
        {
            leftMid = new Ray(new Vector3(transform.position.x, transform.position.y - 0.1f), -transform.right);
            rightMid = new Ray(new Vector3(transform.position.x, transform.position.y - 0.1f), transform.right);
            leftTop = new Ray(new Vector3(transform.position.x, transform.position.y + 0.1f), -transform.right);
            rightTop = new Ray(new Vector3(transform.position.x, transform.position.y + 0.1f), transform.right);
            leftBottom = new Ray(new Vector3(transform.position.x, transform.position.y - 0.3f), -transform.right);
            rightBottom = new Ray(new Vector3(transform.position.x, transform.position.y - 0.3f), transform.right);

            leftMidHit = Physics.Raycast(leftMid, 0.2f, layer);
            rightMidHit = Physics.Raycast(rightMid, 0.2f, layer);
            leftTopHit = Physics.Raycast(leftTop, 0.2f, layer);
            rightTopHit = Physics.Raycast(rightTop, 0.2f, layer);
            leftBottomHit = Physics.Raycast(leftBottom, 0.2f, layer);
            rightBottomHit = Physics.Raycast(rightBottom, 0.2f, layer);
        }
        else
        {
            leftMidHit = false;
            rightMidHit = false;
            leftTopHit = false;
            rightTopHit = false;
            leftBottomHit = false;
            rightBottomHit = false;
        }
    }

    private void FixedUpdate()
    {
        if (
            !boosting && !smashing && !dashing && !faded &&
            (
            (Input.GetAxis("Horizontal") > 0 && ((!rightMidHit && !rightTopHit && !rightBottomHit)) ) ||
            (Input.GetAxis("Horizontal") < 0 && ((!leftMidHit && !leftTopHit && !leftBottomHit)) ) 
            )
        )
        {
            animControl.setWalking(true);
            if (useForce)
            {
                rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y, 0);
            }
            else
            {
                rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * moveSpeed * forceMod, 0, 0));
            }
            if(Input.GetAxis("Horizontal") > 0 && !facingRight)
            {
                flip();
            }
            else if(Input.GetAxis("Horizontal") < 0 && facingRight)
            {
                flip();
            }
        }
        else
        {
            animControl.setWalking(false);
        }
    }

    private void jump()
    {
        if(Input.GetButtonDown("Jump") && !boosting && !smashing) {
            if (grounded || Time.time - lastTimeGrounded <= rememberGroundedFor || (!grounded && extraJumpCount < extraJumpsLimit))
            {
                if (jumped)
                {
                    extraJumpCount++;
                }
                jumped = true;
                if (!useForce)
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
                }
                else
                {
                    if (!grounded)
                    {
                        rb.AddForce(new Vector3(0, jumpForce * forceMod / 1.3f, 0));
                    }
                    else
                    {
                        rb.AddForce(new Vector3(0, jumpForce * forceMod, 0));
                    }
                }
                grounded = false;
            }
            else if (jumpSuit && !hovering && !hovered)
            {
                hover();
            }
        }
    }

    private void jumpRelease()
    {
        if (useJumpHold)
        {
            if (Input.GetButtonUp("Jump") && !boosting && !boosted)
            {
                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, 0);
                }
                if (hovering)
                {
                    stopHover();
                }
            }
        }
    }

    private void boost()
    {
        if(hovering && !boosted && !boosting && Input.GetButtonDown("Fire3"))
        {
            boosting = true;
            extraBoostCount++;
            if (!useForce)
            {
                rb.velocity = new Vector3(Input.GetAxis("Horizontal") * boostSpeed, Input.GetAxis("Vertical") * boostSpeed, 0);
            }
            else
            {
                rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * boostSpeed * forceMod, Input.GetAxis("Vertical") * boostSpeed * forceMod, 0));
            }
        }
    }

    private void smash()
    {
        if (grounded)
        {
            if (Input.GetButtonDown("Fire3") && !smashCD)
            {
                float multiplier = 1;
                if (!facingRight)
                {
                    multiplier = -1;
                }
                smashing = true;
                rb.AddForce(moveSpeed * multiplier * 1400, jumpForce * 80, 0);
                currentY = transform.position.y;
                prevY = currentY;
            }
        }
    }

    private IEnumerator finishSmash()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.33f);
        float multiplier = 1;
        if (!facingRight)
        {
            multiplier = -1;
        }
        rb.AddForce(moveSpeed * multiplier * 100, -jumpForce * 100, 0);
        endingSmash = true;
        smashCD = true;
    }

    private void dashStrike()
    {
        float multiplier = 1;
        if (!facingRight)
        {
            multiplier = -1;
        }
        if (Input.GetButtonDown("Fire2") && !dashCD)
        {
            rb.useGravity = false;
            rb.AddForce(moveSpeed * multiplier * 10000, 0, 0);
            dashing = true;
        }
    }

    private void endDash()
    {
        rb.useGravity = true;
        dashing = false;
        dashCD = true;
    }

    private void endBoost()
    {
        boosting = false;
        if (extraBoostCount == extraBoostLimit)
        {
            boosted = true;
        }
        extraJumpCount = extraJumpsLimit;
        boostTimer = 0;
        stopHover();
    }

    private void hover()
    {
        moveSpeed = defaultMoveSpeed / 4;
        hovering = true;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }

    private void stopHover()
    {
        moveSpeed = defaultMoveSpeed;
        hovering = false;
        rb.useGravity = true;
    }

    private void resetJump()
    {
        if (!jumpReady)
        {
            jumpReady = true;
            hovered = false;
            jumped = false;
            grounded = true;
            hovering = false;
            boosted = false;
            boosting = false;
            moveSpeed = defaultMoveSpeed;
            timer = 0;
            extraJumpCount = 0;
            extraBoostCount = 0;
        }
    }

    private void teleport()
    {
        if(Input.GetButtonDown("Fire3") && !teleported && teleportCheck())
        {
            float xPart = 0;
            float yPart = 0;
            xPart = Input.GetAxis("Horizontal");
            yPart = Input.GetAxis("Vertical");
            if (xPart == 0 && yPart == 0)
            {
                if (facingRight)
                {
                    xPart = 1;
                }
                else
                {
                    xPart = -1;
                }
            }
            transform.position = new Vector3(transform.position.x + xPart * teleportDistance, transform.position.y + yPart * teleportDistance);
            teleported = true;
        }
    }

    private void teleportMarkerPosition(float xInput, float yInput)
    {
        float xPart = 0;
        float yPart = 0;
        xPart = xInput;
        yPart = yInput;
        if (xPart == 0 && yPart == 0)
        {
            if (facingRight)
            {
                xPart = 1;
            }
            else
            {
                xPart = -1;
            }
        }
        teleportMarker.transform.position = new Vector3(transform.position.x + xPart * teleportDistance, transform.position.y + 0.2f + yPart * teleportDistance);
    }

    private bool teleportCheck()
    {
        block = Physics.OverlapSphere(teleportMarker.transform.position, 0.02f, layer);
        if (block.Length > 0)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }

    private void voidFade()
    {
        if(Input.GetButtonDown("Fire2") && !faded && !fadeWait)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            faded = true;
            GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //function to change suit

    //later all the variables related to the suits can be moved into classes inheriting from the "suit" class
    //all shared variables will be held by the main suit object, as well as some basic movement functions
    //all specific functions will be held by the specific suit classes

    public void selectTele()
    {
        teleSuit = true;
        jumpSuit = false;
        meleeSuit = false;

        jumpForce = 9f;
        moveSpeed = 2.75f;
        extraJumpCount = 0;

        if (useForce)
        {
            jumpForce *= 4f;
            moveSpeed /= 2.5f;
        }

        defaultMoveSpeed = moveSpeed;

        teleportMarker.GetComponent<SpriteRenderer>().enabled = true;

        jumpSuitText.SetActive(false);
        meleeSuitText.SetActive(false);
        teleSuitText.SetActive(true);
    }

    public void selectJump()
    {
        teleSuit = false;
        jumpSuit = true;
        meleeSuit = false;

        jumpForce = 12f;
        moveSpeed = 3f;
        extraJumpCount = 1;

        if (useForce)
        {
            jumpForce *= 4f;
            moveSpeed /= 2.5f;
        }

        defaultMoveSpeed = moveSpeed;

        teleportMarker.GetComponent<SpriteRenderer>().enabled = false;

        jumpSuitText.SetActive(true);
        meleeSuitText.SetActive(false);
        teleSuitText.SetActive(false);
    }

    public void selectMelee()
    {
        teleSuit = false;
        jumpSuit = false;
        meleeSuit = true;

        jumpForce = 6f;
        moveSpeed = 2.5f;
        extraJumpCount = 0;

        if (useForce)
        {
            jumpForce *= 4f;
            moveSpeed /= 2.5f;
        }

        defaultMoveSpeed = moveSpeed;

        teleportMarker.GetComponent<SpriteRenderer>().enabled = false;

        jumpSuitText.SetActive(false);
        meleeSuitText.SetActive(true);
        teleSuitText.SetActive(false);
    }

}
