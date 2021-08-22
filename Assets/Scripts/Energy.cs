using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Energy : MonoBehaviour
{
    Tween tweening;
    public float moveRenge;
    // Start is called before the first frame update
    void Start()
    {
        tweening = transform.DORotate(new Vector3(0,transform.rotation.y + moveRenge,0),0.5f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
