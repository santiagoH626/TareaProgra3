using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public bool gameStarted = false;
    public Vector3 startVel;
    public float fakeGravity;
    public float maxUpwardSpeed;
    public Vector3 saveSpeed;
    public Vector3 tempSpeed;

    public Vector2 forceMultiplier;
    public float upForceMultiplier;
    public float upSaveForceMultiplier;
    public float upTempForceMultiplier;

    public float downSaveForceMultiplier;
    public float downTempForceMultiplier;
    public Vector2 rotationMultiplier;
    public Rigidbody rb;
    public bool startWithVelocity;
    public float distance;
    public float zpos;

    [Header("Speed Variation")]
    public float targetSpeed;
    public float addSpeed = 1f;
    public float multSpeed = 1.01f;

    public TextMeshProUGUI tmpDistance;
    public TextMeshProUGUI tmpHeight;
    
    public Transform cameraTransform;
    public float levelHeight;

    [Header("Scene References")]
    public GameObject instrucciones;
    public SendInfo sendInfo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (startWithVelocity){
            StartGame();
            //rb.AddForce(startVel);
        }
        
        zpos = transform.position.z;
        sendInfo = GameObject.FindWithTag("Web").GetComponent<SendInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted){
            if (Input.GetKeyDown(KeyCode.Space)){
                gameStarted = true;
                StartGame();
            }
            instrucciones.SetActive(true);
        } else {
            instrucciones.SetActive(false);
        }
        distance += (transform.position.z-zpos);
        zpos = transform.position.z;
        //saveSpeed = rb.velocity;
        float hh = Input.GetAxis("Horizontal");
        float vv = Input.GetAxis("Vertical");

        float vVariation = 0f;
        if (vv>0f){
            if (rb.velocity.y <maxUpwardSpeed){
                vVariation = vv;
            }
            if (transform.position.y > levelHeight){
                vVariation = 0f;
            }
            saveSpeed = new Vector3 (0f,saveSpeed.y+vVariation*upForceMultiplier*Time.deltaTime+fakeGravity*Time.deltaTime, saveSpeed.z);
            tempSpeed = new Vector3 (hh*forceMultiplier.x,0f,0f);

        } else {
            vVariation = vv;
            saveSpeed = new Vector3 (0f,saveSpeed.y+fakeGravity*Time.deltaTime+vVariation*downSaveForceMultiplier, saveSpeed.z);
            tempSpeed = new Vector3 (hh*forceMultiplier.x,vVariation*downTempForceMultiplier,0f);
        }

        



        //Vector3 newVel = new Vector3 (hh * forceMultiplier.x, vv * forceMultiplier.y, 0f);

        rb.velocity = saveSpeed+tempSpeed;

        transform.eulerAngles = new Vector3(vv*rotationMultiplier.x,0f, hh*rotationMultiplier.y);

        int intDist = (int)distance;
        tmpDistance.text = intDist.ToString()  + "m";
        //tmpHeight.text = transform.position.y.ToString();
        ManageAceleration();
    }
    public void OnCollisionEnter(Collision col){
        if (col.gameObject.CompareTag("Wall")){
            sendInfo.EndGame((int)distance);
            cameraTransform.parent = null;
            Destroy(gameObject);
        }
    }
    public void StartGame(){
            rb.velocity = startVel;
            saveSpeed = startVel;
            targetSpeed = startVel.z;
    }
    public void Accelerate(){
        

        targetSpeed = saveSpeed.z*multSpeed+addSpeed;



    }
    public void ManageAceleration(){
        if (saveSpeed.z<targetSpeed){
            saveSpeed.z = Mathf.Lerp(saveSpeed.z,targetSpeed,0.3f);
        }
    }
}
