using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodePreview : MonoBehaviour
{
    public TextMeshProUGUI code;

    public void SetBubbleJava(){
        code.text = "void bubbleSort(int arr[]) { \n   int n = arr.length; \n   for (int i = 0; i < n-1; i++) \n      for (int j = 0; j < n-i-1; j++) \n         if (arr[j] > arr[j+1]){ \n\n            // swap arr[j+1] and arr[j] \n            int temp = arr[j]; \n            arr[j] = arr[j+1]; \n            arr[j+1] = temp; \n}";    
    }

    public void SetBubblePython(){
        code.text = "def bubbleSort(arr): \n   n = len(arr) \n\n   # Traverse through all array elements \n   for i in range(n): \n\n      # Last i elements are already in place \n      for j in range(0, n-i-1): \n\n         #traverse the array from 0 to n-i-1 \n         # Swap if the element found is greater than the next element \n         if arr[j] > arr[j+i]: \n            arr[j], arr[j+1] = arr[j+1], arr[j]";
    }

    public void setBubbleCSharp(){
        code.text = "static void bubbleSort(int []arr){ \n   int n = arr.length; \n\n   for (int i = 0; i < n - 1; i++) \n      for (int j = 0; j < n - i - 1; j++) \n         if (arr[j] > arr[j + 1]) { \n\n            // swap temp and arr[i] \n            int temp = arr[j]; \n            arr[j] = arr[j + 1]; \n            arr[j + 1] = temp; \n         } \n}";
    }
}
