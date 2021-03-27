using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleStart : MonoBehaviour
{
    public void changeTitleScene(string scenename){
        Application.LoadLevel(scenename);
    }
}
