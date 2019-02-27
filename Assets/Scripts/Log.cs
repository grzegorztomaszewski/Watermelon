using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField]
    public GameObject log;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnLog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
        public void spawnLog()
    {
        Instantiate(log);
        Debug.Log("testowo");
    }
}
