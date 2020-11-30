using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundTest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 horizontal = new Vector3(Input.GetAxis("horizontal"),0.0f,0.0f);
       // transform.position = transform.position + horizontal * Time.deltaTime;
       if(transform.position.x < 0){
           this.gameObject.transform.position = new Vector3(15.0f,4.86f,89f);
       }
       
       transform.Translate(-0.1f,0.0f,0.0f);
    }
}
