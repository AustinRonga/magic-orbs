using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using UnityEngine;

public class OrbControl : MonoBehaviour
{
    public Behaviour halo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }
    public void HighlightSelected()
    {
        halo.enabled = true;
    }
    public void HighlightDisable()
    {
        halo.enabled = false;
    }
}
