using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{

    [SerializeField]
    private Button[] SpinObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(int buttonIndex)
    {
            SpinObject[buttonIndex].transform.Rotate(0,0,90);
            Debug.Log("button is clicked");
    }
}
