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

    public void setSelectionJava(){
        code.text = "void sort(int arr[]){ \n   int n = arr.length; \n\n   // One by one move boundary of unsorted subarray \n   for (int i = 0; i < n - 1; i++){ \n\n   // Find the minimum element in unsorted array \n   int min_idx = i; \n   for (int j = i + 1; j < n; j++) \n         if (arr[j] < arr[min_idx]) \n            min_idx = j; \n\n      // Swap the found minimum element with the first element \n      int temp = arr[min_idx]; \n      arr[min_idx] = arr[i]; \n      arr[i] = temp; \n   } \n}";
    }

    public void setSelectionPython(){
        code.text = "for i in range(len(A)): \n\n   # Find the minimum element in remaining unsorted array \n   min_idx = i \n   for j in range(i + 1, len(A)): \n      if A[min_idx] > A[j]: \n         min_idx = j \n\n   # Swap the found minimum element with the first element \n   A[i], A[min_idx] = A[min_idx], A[i]"; 
    }

    public void setSelectionCSharp(){
        code.text = "static void sort(int []arr) { \n   int n = arr.length; \n\n   // One by one move boundary of unsorted subarray \n   for (int i = 0; i < n - 1; i++){ \n\n      // Find the minimum element in unsorted array \n      int min_idx = i; \n      for (int j = i + 1; j < n; j++) \n         if (arr[j] < arr[min_idx]) \n            min_idx = j; \n\n      // Swap the found minimum element with the first element \n      int temp = arr[min_idx]; \n      arr[min_idx] = arr[i]; \n      arr[i] = temp; \n   } \n}";
    }

    public void setInsertionJava(){

    }

    public void setInsertionPython(){

    }

    public void setInsertionCSharp(){

    }
}
