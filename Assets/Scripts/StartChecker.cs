using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChecker : MonoBehaviour
{
    private MoveObject moveObject;
    void Start()
    {
        moveObject = GetComponent<MoveObject>();
    }

    // Update is called once per frame
    
    
    public void SetInitialSpeed()
    {
        moveObject.moveSpeed = 0.005f;
    }
    
}
