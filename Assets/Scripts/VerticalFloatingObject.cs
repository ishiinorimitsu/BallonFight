using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VerticalFloatingObject : MonoBehaviour
{
    public float moveTime;
    public float moveRenge;
    Tweener tweener;
    // Start is called before the first frame update
    void Start()
    {
        tweener = transform.DOMoveY(transform.position.y-moveRenge,moveTime).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        tweener.Kill();
    }
}
