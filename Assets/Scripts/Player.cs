using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
    : MonoBehaviour
{
   

    Rigidbody rigid;
    
    
    public int moveSpeed = 2;
    public float rotateSpeed = 0.4f;


    private float _turnVel;
    private float _currentAngle;


    private InputControls _controls;

    private float _throttle;
    private float _steering;

    public int hp = 3;

    public static Player player;

    public bool mortal;

    void Awake()
    {
        
        rigid = GetComponent<Rigidbody>();

        _controls = new InputControls();

        _controls.BoatControls.Trottle.performed += context => _throttle = context.ReadValue<float>();
        _controls.BoatControls.Trottle.canceled += context => _throttle = 0f;

        _controls.BoatControls.Steering.performed += context => _steering = context.ReadValue<float>();
        _controls.BoatControls.Steering.canceled += context => _steering = 0f;

        mortal = true;
    }

    private void Update()
    {
       /* if (Input.GetButtonDown("Jump") && (isJump >= 0))
        {
            isJump--;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }

       
        if (Input.GetKey(KeyCode.W) )
        {
            Accelerate();
        }
        if (Input.GetKey(KeyCode.A))
        {
            Turn(1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Turn(-1f);
        }
        */

    }
    void FixedUpdate()
    {
      

        TurnAcc(_throttle, _steering);
        

    }

    public void OnEnable()
    {
        
        _controls.BoatControls.Enable();
    }

    public void OnDisable()
    {

        _controls.BoatControls.Disable();
    }

   

    public void Accelerate(float modifier)
    {

        modifier = Mathf.Clamp(modifier, -1f, 1f); // clamp for reasonable values
        var forward = rigid.transform.forward;

        forward.y = 0f;

        forward.Normalize();

        rigid.AddForce(moveSpeed* forward*modifier, ForceMode.Acceleration); // add force forward based on input and horsepower

        rigid.AddRelativeTorque(-Vector3.right * modifier, ForceMode.Acceleration);

    }

    public void Turn(float modifier)
    {
        
        //rigid.AddTorque(  new Vector3(0,1,0) * modifier * 100  , ForceMode.VelocityChange);

        modifier = Mathf.Clamp(modifier, -1f, 1f); // clamp for reasonable values
        rigid.AddRelativeTorque(new Vector3(0f, 5f, -5f * 0.5f) * modifier, ForceMode.Acceleration); // add torque based on input and torque amount
        
        
        _currentAngle = Mathf.SmoothDampAngle(_currentAngle,
            60f * -modifier,
            ref _turnVel,
            0.5f,
            10f,
            Time.fixedTime);
        rigid.transform.localEulerAngles = new Vector3(0f, _currentAngle, 0f);
        
    }

    public void TurnAcc(float mod1, float mod2)
    {

        mod1 = Mathf.Clamp(mod1, -1f, 1f); // clamp for reasonable values
        mod2 = Mathf.Clamp(mod2, -1f, 1f); // clamp for reasonable values
        var forward = rigid.transform.forward;

        forward.y = 0f;

        forward.Normalize();

        rigid.AddForce(moveSpeed * forward * mod1, ForceMode.Acceleration); // add force forward based on input and horsepower
        rigid.AddTorque( mod2 * (new Vector3(0,1,0) * rotateSpeed ) , ForceMode.VelocityChange);

        //rigid.AddRelativeTorque(-Vector3.right * mod1, ForceMode.Acceleration);


    }

    public IEnumerator BeImmortal(int sec)
    {

        mortal = false;
        yield return new WaitForSeconds(sec);
        mortal = true;
        
    }


    public void Damage(int dmg)
    {

        hp -= dmg;

    }


}
