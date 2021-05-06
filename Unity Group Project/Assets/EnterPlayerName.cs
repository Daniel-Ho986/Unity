using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterPlayerName : MonoBehaviour
{

    public InputField input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PersistentData.Instance.SetName(input.text);
    }
}
