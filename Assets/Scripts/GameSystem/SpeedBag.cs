using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBag : MonoBehaviour
{
    public Spawner spawner;
    public GameObject bagHit;
    public GameObject topTarget; 
    public GameObject bottomTarget;

    private int power = 2400; //0-3000
    private int mode = 1; //1, 2, or 3 shot 
    private int bounceCnt = 0;
    private HingeJoint2D joint;
    private Rigidbody2D rb;
    private JointMotor2D motor;
    private bool hitTarget = false;
    private bool hitZone = false;
    private GameObject lastTarget;
    private bool recording = false;
    private float timeElapsed = 0f;
    private float startTime = 0f;
    private List<float> spawnTimings = new List<float>(); 
    private List<int> spawnSides = new List<int>();
    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<HingeJoint2D>();
        motor = joint.motor;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        //Control recording (for DEBUG purpose for now) 
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (recording)
            {
                //Stop recording and give spawner its shit
                recording = false;
                spawner.giveRecordings(spawnTimings, spawnSides);

                //Clear the last recording 
                //spawnSides.Clear();
                //spawnTimings.Clear();

            } 
            else
            {
                //Start timer
                startTime = timeElapsed;
                recording = !recording;
            }
            
        }
            //Mode Control 
            if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameObject hit = Instantiate(bagHit, bottomTarget.transform.position, bottomTarget.transform.rotation);
        } 
        
           
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject hit = Instantiate(bagHit, topTarget.transform.position, topTarget.transform.rotation);  
        }
        //Rotatation Controls 
        if (Input.GetKeyDown(KeyCode.P))
        {
            joint.useMotor = true;
            motor.motorSpeed = 600;
            joint.motor = motor;
        }
        //Controls: Left we want to hit <y, -x>. Right is <-y, x> 
        int dirXMult = -1;
        int dirYMult = 1;
        if (this.transform.position.x < 0)
        {
            dirXMult = 1; 
            dirYMult = -1;
        }
        Vector3 current = this.transform.position;
        float curX = current.x;
        float curY = current.y;
        float curZ = current.z;
        Vector3 perp = Vector3.up * curY * dirYMult + Vector3.right * curX * dirXMult + Vector3.forward * curZ; 
        Vector3 perp2 = Vector3.up * curY * dirXMult + Vector3.right * curX * dirYMult + Vector3.forward * curZ;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (power < 0)
            {
                power *= -1;
            }
            punch(perp);
        } 

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (power > 0)
            {
                power *= -1;
            }
            punch(perp2);
        } 

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (power > 0)
            {
                power -= 100;
            }
        } 

        if(Input.GetKey(KeyCode.UpArrow))
        {
            if (power <= 3000)
            {
                power += 100;
            }
        } 


    }

    public bool isRecording()
    {
        return recording;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        //Bouncing mechanic
        motor.motorSpeed *= -1;
        joint.motor = motor; 

        //Count the bounces to register sigle, double, and triple hits 
        bounceCnt++;  
        if (bounceCnt == mode)
        {
            joint.useMotor = false;
            bounceCnt = 0;
        }  

        */
        //rb.AddForce(rb.velocity * -1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //For recording tracks 
        if (recording)
        {
            if (collision.gameObject.CompareTag("Zone"))
            {
                //Record when we hit the zone relative to last time we hit the zone
                float delta = timeElapsed - startTime;
                startTime = timeElapsed; 
                spawnTimings.Add(delta); 

                //Need to know if it is left or right 
                if (collision.gameObject.transform.position.x > 0)
                {
                    //Right side 
                    spawnSides.Add(1);
                } 
                else
                {
                    //Left side
                    spawnSides.Add(0);
                }
            }
        }
    }

    public List<float> getRecordedTimings()
    {
        return spawnTimings;
    } 

    public List<int> getRecordedSides()
    {
        return spawnSides;
    }


    public void punch(Vector3 direction)
    {
        //We want to offset the velocity to zero so we have consistent punches 
        Vector3 vel = rb.velocity;
        rb.velocity = Vector3.zero;

        //Actual punch 
        joint.useMotor = true;
        motor.motorSpeed = power * direction.x/direction.x;
        joint.motor = motor;
        Invoke("turnOffMotor", 0.1f);
        //rb.AddForce(direction*power);
    } 

    private void turnOffMotor()
    {
        rb.AddForce(rb.velocity);
        joint.useMotor = false; 
        
        
    }
}
