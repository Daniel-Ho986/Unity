using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleStart : MonoBehaviour
{
    public void changeTitleScene(string scenename){
        SceneManager.LoadScene(scenename);
    }
}
