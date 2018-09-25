using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform isoCamera;
    public Text distanceText;
    public float walkSpeed = 6f;
    //public float runSpeed = 11f;
    public float acceleration = 20f;
    public float friction = 8f;
    public float airFrictionFactor = 50f;
    public float jumpSpeed = 14f;
    public float gravity = 5f;
    public float tarSlowFactor = .75f;
    public Vector3 velocity;
    public int CurrentDistance = 0;
    public Transform Knight;
    public GameObject LeftBoot;
    public GameObject RightBoot;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Chest;
    public GameObject Head;
    public GameObject Sword;
    public BoxCollider box;
    public ParticleSystem tarDebuff;

    private CharacterController charCtrl;
    private Animator anim;
    private Vector3 startPosition;
    private float moveSpeed;
    private float kRotY;
    private bool locked = false;
    private bool alive = true;
    private Game_Manager gm;

    void Start()
    {
        isoCamera = GameObject.Find("isoCamera").transform;
        distanceText = GameObject.Find("distance_text").GetComponent<Text>(); ;

        gm = GameObject.Find("GameManager").GetComponent<Game_Manager>();

        charCtrl = GetComponent<CharacterController>();

        anim = GetComponent<Animator>();

        velocity = Vector3.zero;

        moveSpeed = walkSpeed;

        startPosition = transform.position;

        tarDebuff.Stop();

        gm.disableEscapeMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            locked = !locked;
        }
        if (locked)
        {
            gm.enableEscapeMenu();
            applyGravity();
            charCtrl.Move(velocity * Time.deltaTime);
            applyFriction(1.0f);
        }
        else
        {
            if (alive)
            {
                gm.disableEscapeMenu();

                Vector3 dir;
                if (charCtrl.isGrounded)
                {
                    if (Input.anyKey == false)
                    {
                        anim.SetBool("Moving", false);
                    }
                    else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                    {
                        anim.SetBool("Moving", true);
                    }
                    rotateKnight();
                    if (Input.GetKey(KeyCode.W))
                    {
                        dir = isoCamera.transform.forward;
                        dir.y = 0;
                        Accelerate(dir, acceleration);
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        dir = -isoCamera.transform.right;
                        dir.y = 0;
                        Accelerate(dir, acceleration);
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        dir = -isoCamera.transform.forward;
                        dir.y = 0;
                        Accelerate(dir, acceleration);
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        dir = isoCamera.transform.right;
                        dir.y = 0;
                        Accelerate(dir, acceleration);
                    }
                    if (Input.GetKey(KeyCode.Space))
                    {
                        velocity.y = jumpSpeed;
                    }
                }
                else
                {
                    applyGravity();
                    anim.SetBool("Moving", false);
                    if (Input.GetKey(KeyCode.W))
                    {
                        dir = isoCamera.transform.forward;
                        dir.y = 0;
                        Accelerate(dir, acceleration * (1 / airFrictionFactor));
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        dir = -isoCamera.transform.right;
                        dir.y = 0;
                        Accelerate(dir, acceleration * (1 / airFrictionFactor));
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        dir = -isoCamera.transform.forward;
                        dir.y = 0;
                        Accelerate(dir, acceleration * (1 / airFrictionFactor));
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        dir = isoCamera.transform.right;
                        dir.y = 0;
                        Accelerate(dir, acceleration * (1 / airFrictionFactor));
                    }
                }

                charCtrl.Move(velocity * Time.deltaTime);

                applyFriction(1.0f);

                moveSpeed = walkSpeed;

                // Update the UI with the current distance
                CurrentDistance = (int)(transform.position.x - startPosition.x);
                if (CurrentDistance < 0)
                    CurrentDistance = 0;
                distanceText.text = "Distance: " + CurrentDistance.ToString() + " m";

                // if (Input.GetKeyDown(KeyCode.T))
                // {
                //     TriggerDeath();
                // }
            }
            else
            {
                gm.enableEscapeMenu();
                applyGravity();
                applyFriction(1.0f);
                charCtrl.Move(velocity * Time.deltaTime);
            }
        }

    }

    private void Accelerate(Vector3 dir, float accel)
    {
        float addspeed;
        float accelspeed;
        float currentspeed;

        currentspeed = Vector3.Dot(velocity, dir);
        addspeed = moveSpeed - currentspeed;
        if (addspeed <= 0)
        {
            return;
        }
        else
        {
            accelspeed = accel * Time.deltaTime * moveSpeed;
            if (accelspeed > addspeed)
            {
                accelspeed = addspeed;
            }
            velocity += accelspeed * dir;
        }
    }

    private void applyFriction(float t)
    {
        Vector3 vec = velocity;
        float speed;
        float newspeed;
        float control;
        float drop;

        vec.y = 0.0f;
        speed = vec.magnitude;
        drop = 0.0f;

        if (charCtrl.isGrounded)
        {
            control = speed < friction ? friction : speed;
            drop = control * friction * Time.deltaTime * t;
        }

        newspeed = speed - drop;
        if (newspeed < 0)
            newspeed = 0;
        if (speed > 0)
            newspeed /= speed;

        velocity.x *= newspeed;
        velocity.z *= newspeed;
    }

    private void applyGravity()
    {
        float accelspeed;
        accelspeed = gravity * Time.deltaTime * 11;
        velocity += accelspeed * Vector3.down;
    }

    private void rotateKnight()
    {
        if (Input.GetKey(KeyCode.W))
        {
            kRotY = 45;
        }
        if (Input.GetKey(KeyCode.D))
        {
            kRotY = 135;
        }
        if (Input.GetKey(KeyCode.S))
        {
            kRotY = 225;
        }
        if (Input.GetKey(KeyCode.A))
        {
            kRotY = 315;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            kRotY = 0;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            kRotY = 270;
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            kRotY = 180;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            kRotY = 90;
        }
        Knight.rotation = Quaternion.Euler(0, kRotY, 0);
    }

    public void TriggerDeath()
    {
        Debug.Log("Player Death occured.");

        if (alive)
        {
            gm.playerDied();
            gm.addCoins(CurrentDistance);
            gm.enableEscapeMenu();
        }

        alive = false;

        Destroy(charCtrl);
        charCtrl.enabled = false;
        anim.enabled = false;
        box.enabled = false;

        LeftBoot.GetComponent<Rigidbody>().isKinematic = false;
        RightBoot.GetComponent<Rigidbody>().isKinematic = false;
        LeftHand.GetComponent<Rigidbody>().isKinematic = false;
        RightHand.GetComponent<Rigidbody>().isKinematic = false;
        Head.GetComponent<Rigidbody>().isKinematic = false;
        Chest.GetComponent<Rigidbody>().isKinematic = false;
        Sword.GetComponent<Rigidbody>().isKinematic = false;
    }

    internal float getSpeed()
    {
        return moveSpeed;
    }

    internal void setSpeed(float n)
    {
        moveSpeed = n;
    }

    internal int getDistance()
    {
        return CurrentDistance;
    }

    internal bool isAlive()
    {
        return alive;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12) // If it's tar
        {
            walkSpeed = tarSlowFactor * walkSpeed;
            if (walkSpeed <= 4)
                walkSpeed = 4;
            //Debug.Log("changed move speed");
            tarDebuff.Play();
        }
    }
}
