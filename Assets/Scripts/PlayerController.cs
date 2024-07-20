using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float TorqueAmount = 1f;
    [SerializeField] float SpeedIncreaseRate = 5f; // Speed increase rate per second
    [SerializeField] float SpeedDecreaseRate = 2.5f; // Speed decrease rate per second
    [SerializeField] float BoostAmount = 10f; // Speed boost amount
    [SerializeField] float MaxSpeed = 10f; // Maximum speed limit
    [SerializeField] float MinSpeed = -10f; // Minimum speed limit (for reverse movement)
    
    [SerializeField] float Jump = 2f;
    public Rigidbody2D rg2d;

    public bool isTouchingGround = false; // Flag to track ground contact

    [SerializeField] public ParticleSystem particlesystem; // Reference to the ParticleSystem component

    bool canControl = true;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (canControl)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rg2d.AddTorque(TorqueAmount);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rg2d.AddTorque(-TorqueAmount);
            }
                
            // Boost functionality: Press space to boost if velocity is below a certain threshold
            if (Input.GetKeyDown(KeyCode.V) && rg2d.velocity.x < 5f && isTouchingGround)
            {
                rg2d.velocity = new Vector2(rg2d.velocity.x + BoostAmount, rg2d.velocity.y);
                Debug.Log("Boost applied");
            }

            // Jump Functionality
            if (isTouchingGround)
            {
                if (Input.GetKeyDown(KeyCode.B))
                {
                    rg2d.velocity = new Vector2(rg2d.velocity.x, rg2d.velocity.y + Jump);
                }
            }
        }

        // Get the current rotation angle along the z-axis (in degrees)
        float angle = rg2d.rotation;

        // Check the tilt angle and apply acceleration or deceleration
        if (angle >= -90 && angle < -10&&isTouchingGround)
        {
            rg2d.velocity = new Vector2(rg2d.velocity.x + SpeedIncreaseRate * Time.deltaTime, rg2d.velocity.y);
        }
        else if (angle > 10 && angle <= 90&&isTouchingGround)
        {
            rg2d.velocity = new Vector2(rg2d.velocity.x - SpeedDecreaseRate * Time.deltaTime, rg2d.velocity.y);
            Debug.Log("Decreasing");
        }

        EffectSpeedModifier();

        // Clamp the velocity to the maximum and minimum speed limits
        rg2d.velocity = new Vector2(Mathf.Clamp(rg2d.velocity.x, MinSpeed, MaxSpeed), rg2d.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
            // Play the second particle effect
            particlesystem.Play();
        }
        else if (collision.gameObject.CompareTag("Jumpable"))
        {
            isTouchingGround = true;
            Debug.Log("On railings");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = false;
            // Stop the second particle effect
            particlesystem.Stop();
        }
        else if (collision.gameObject.CompareTag("Jumpable"))
        {
            isTouchingGround = false;
            // Play the second particle effect
        }
    }

    void EffectSpeedModifier()
    {
        var main = particlesystem.main; // Access the MainModule of the particle system

        if (rg2d.velocity.x <= 0)
        {
            main.startSpeed = 0;
        }
        if (((rg2d.velocity.x > 10) && (rg2d.velocity.x < 20))||(rg2d.velocity.x<-15))
        {
            main.startSpeed = 3;
        }
        else if ((rg2d.velocity.x > 20) && (rg2d.velocity.x < 30))
        {
            main.startSpeed = 6;
        }
        else if ((rg2d.velocity.x > 30) && (rg2d.velocity.x < 40))
        {
            main.startSpeed = 8;
        }
        else if (rg2d.velocity.x > 40)
        {
            main.startSpeed = 10;
        }

        Debug.Log(rg2d.velocity.x); // Access the constant value of the startSpeed MinMaxCurve
    }

    public void DisableControls()
    {
        canControl = false;
    }
}
