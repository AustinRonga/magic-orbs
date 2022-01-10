using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using UnityEngine;

public class OrbControl : MonoBehaviour
{
    public List<GameObject> MagicOrbs;
    public Transform SphereOrigin;
    private double RawDistanceSubtotal;
    private double CalculationDistance;
    private double maxDistance=19.5;
    public float limitation = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        RawDistanceSubtotal = 0;
        //For each orb in the initial orb list
        foreach (var orb in MagicOrbs)
        {
            float dst = Vector3.Distance(SphereOrigin.position, orb.transform.position);
            if (dst > limitation)
            {
                Vector3 vect = SphereOrigin.position - orb.transform.position;
                vect = vect.normalized;
                vect *= dst - limitation;
                orb.transform.position += vect;
            }
            List<GameObject> CurrentOrb = new List<GameObject> { orb };
            //List<GameObject> SubOrbs = new List<GameObject> { MagicOrbs.Except(CurrentOrb) };
            var SubOrbs = MagicOrbs.Except(CurrentOrb);
            foreach (var second_orb in SubOrbs)
            {
                RawDistanceSubtotal += Math.Pow(Vector3.Distance(orb.transform.position, second_orb.transform.position), 2);
            }
            
        }
        CalculationDistance = Math.Sqrt(RawDistanceSubtotal);
        Debug.Log(CalculationDistance);
        foreach (var orb in MagicOrbs)
        {
            orb.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.red, Color.green, ((float)CalculationDistance-14) / (float)maxDistance);
            
        }


    }
}
