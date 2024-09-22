
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject aimLaser;
    public bool enableInput = true;
    public int Speed;
    public int rotationSpeed;
    public float gravity = -9.81f;
    private Vector3 velocity;

    private int RunningSpeed;
    private Rigidbody rb;
    private CharacterController cc;
    private Animator animator;
    private Camera cam;
    private bool isWeaponPrepared;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        aimLaser.SetActive(false);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableInput)
            PlayerMove();

    }
    private void PlayerMove()
    {
        float GetXaxis = Input.GetAxis("Horizontal");
        float GetYaxis = Input.GetAxis("Vertical");
        float Running = Input.GetAxisRaw("Fire3");


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (Player.instance.playerState != PlayerStates.TALKING)
            {
                // Vector3 Movement = transform.right * GetXaxis + transform.forward * GetYaxis;
                Vector3 Movement = new Vector3(GetXaxis, 0, GetYaxis);
                Movement.Normalize();
                if (Movement != Vector3.zero && !isWeaponPrepared)
                {
                    Quaternion newRot = Quaternion.LookRotation(Movement, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, rotationSpeed * Time.deltaTime);
                }
                cc.Move(Movement * Speed * Time.deltaTime);
                velocity.y += gravity * Time.deltaTime;
                cc.Move(velocity * Time.deltaTime);

            
                animator.SetFloat("Speed", 1);
                Player.instance.playerState = PlayerStates.MOVING;
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
       
    }
   

    public void AddSpeed(int amount)
    {
        Speed += amount;
    }

    public void PrepareWeapon(bool condition)
    {
        Weapons wep = (Weapons)Inventory.instance.GetEquippedWeapon();
        if (condition == true && wep != null)
        {
            switch (wep.GetWeaponType())
            {
                case WeaponType.Melee:
                    animator.SetBool("Knife aim", condition);
                    break;
                case WeaponType.Ranged:
                    animator.SetFloat("Speed", 0);
                    animator.SetBool("Aim", condition);
                    Speed = 0;
                    aimLaser.SetActive(true);
                    break;
            }
        
            isWeaponPrepared = true;
            Cursor.visible = false;
            Vector3 pos = cam.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 95;
            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
        }
        else
        {
            animator.SetBool("Knife aim", false);
            animator.SetBool("Aim", false);
            aimLaser.SetActive(false);
            isWeaponPrepared = false;
            Cursor.visible = true;
            Speed = 10;
        }
    }

    public void PlayAttackAnim(int x)
    {
        if (x == 1)
            animator.Play("Stabbing");
        if (x == 0)
            animator.Play("Shooting");
    }
} 
