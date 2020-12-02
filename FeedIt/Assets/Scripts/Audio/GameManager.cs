using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      //Starts bacground Audio
      FindObjectOfType<AudioManager>().PlayBacgroundSound();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
