using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    // Awake is called before Start()
    private void Awake()
    {
        Debug.Log("First");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Second");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Third");
    }
}
