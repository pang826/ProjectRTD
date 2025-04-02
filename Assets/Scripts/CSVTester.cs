using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("TestCSVFile"); 

        Debug.Log(data.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
