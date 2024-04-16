using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private void Update()
    {
        Destroy(this.gameObject, 3);
    }
    void ObjectDestroy()
    {
       Destroy(this.gameObject); 
    }
}
