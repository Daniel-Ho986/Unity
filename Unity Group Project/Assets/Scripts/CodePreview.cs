using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodePreview : MonoBehaviour
{
    public TextMeshProUGUI code;

    public void SetJava(){
        code.text = "void bubbleSort(int arr[]) { \n int n = arr.length; \n for (int i = 0; i < n-1; i++) \n for (int j = 0; j < n-i-1; j++) \nif (arr[j] > arr[j+1]){ \n\n // swap arr[j+1] and arr[j] \n int temp = arr[j]; \n arr[j] = arr[j+1]; \n arr[j+1] = temp; \n }";
            
    }
}
