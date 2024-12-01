using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GenerateLevel level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col){
        if (col.CompareTag("Player")){
            level.CreateChunk();
            col.gameObject.GetComponent<Player>().Accelerate();
        }
    }
}
