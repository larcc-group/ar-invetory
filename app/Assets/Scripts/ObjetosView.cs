using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosView : MonoBehaviour
{

    public GameObject Item, ItemParent;
    void Start()
    {
        new ObjetosController(Item, ItemParent).Read();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
