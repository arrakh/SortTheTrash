using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPivot : MonoBehaviour
{
    

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(30f);
        Destroy(gameObject);
    }

    private void Update() 
    {
        
    }
}
