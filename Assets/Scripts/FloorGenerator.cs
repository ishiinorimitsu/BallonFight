using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject aerialFloorPrefab;
    [SerializeField]
    private Transform generateTran;
    [Header("¶¬‚Ü‚Å‚Ì‘Ò‹@ŽžŠÔ")]
    public float waitTime;
    private float timer;
    private GameDirector gameDirector;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            timer = 0;
            GenerateFloor();
        }
    }

    private void GenerateFloor()
    {
        GameObject obj = Instantiate(aerialFloorPrefab,generateTran);
        float randomPosY = Random.Range(-4.0f, 4.0f);
        obj.transform.position = new Vector2(obj.transform.position.x,obj.transform.position.y + randomPosY);
        gameDirector.GenerateCount++;
    }

    public void SetUpGenerator(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;
    }
}
