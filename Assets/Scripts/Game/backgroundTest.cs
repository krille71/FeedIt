using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundTest : MonoBehaviour
{

    public float backgroundSpeed;
    public float heightOffset;
    public float layer;

    // Start is called before the first frame update
    void Start()
    {
         //Debug.Log(this.gameObject.GetComponent<Renderer>().bounds.size);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 horizontal = new Vector3(Input.GetAxis("horizontal"),0.0f,0.0f);
       // transform.position = transform.position + horizontal * Time.deltaTime;
       if(transform.position.x < -20.0f){
           this.gameObject.transform.position = new Vector3(20.0f,heightOffset, layer);
       }
       
       transform.Translate(-backgroundSpeed,0.0f,0.0f);
    }
}
