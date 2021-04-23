using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public bool IsGrounded;

    UnityEngine.UI.Text velocityText;
    // Use this for initialization
    void Start()
    {
        setIsGroundOff(); //預設Player在空中
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void move(string direct, float n)
    {
        clampsMoveVelocity(n); //限制Player移動速度
        flipSpriteByDirect(direct); //依據輸入的方向，決定是否要翻轉spriteX軸
        addForceToPlayer(direct); //給予player移動量
    }

    public void jump()
    {
        if (IsGrounded)
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * 320);
    }

    public void fall()
    {
        Vector3 temp3 = GetComponent<Rigidbody2D>().velocity;
        if (temp3.y > 0)
            GetComponent<Rigidbody2D>().velocity = new Vector3(temp3.x, temp3.y * 0.5f, temp3.z);

    }

    void setIsGroundOn()
    {
        IsGrounded = true;
    }
    void setIsGroundOff()
    {
        IsGrounded = false;
    }

    void clampsMoveVelocity(float n)
    {
        n = Mathf.Abs(n);
        Vector3 temp3 = GetComponent<Rigidbody2D>().velocity;
        GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Clamp(temp3.x, -4, 4) * n, temp3.y, temp3.z);
    }
    void flipSpriteByDirect(string d)
    {
        if (d == "left")
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        if (d == "right")
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    Vector3 getFocreByDirect(string d)
    {
        if (d == "left")
        {
            return Vector3.left;
        }
        else
        if (d == "right")
        {
            return Vector3.right;
        }
        else
        {
            return Vector3.zero;
        }
    }
    void addForceToPlayer(string direct)
    {
        Vector3 vectorDirect = getFocreByDirect(direct);
        if (IsGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(vectorDirect * 12);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(vectorDirect * 6);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            Bounds myBounds = GetComponent<Collider2D>().bounds;
            Bounds blockBounds = collision.transform.GetComponent<Collider2D>().bounds;
            if (!blockBounds.Intersects(myBounds))
            {
                setIsGroundOn();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            setIsGroundOff();
        }
    }
}