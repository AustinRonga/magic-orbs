using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class DistanceDetector : MonoBehaviour
{
    public List<GameObject> MagicOrbs;
    public Transform SphereOrigin;
    private double RawDistanceSubtotal;
    private double CalculationDistance;
    private double maxDistance = 27.8;
    public float limitation = 1;
    public float initialSpeed;
    private OrbControl SelectedOrb;

    // Start is called before the first frame update

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        float speed = initialSpeed / 100;
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
            orb.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.red, Color.green, ((float)CalculationDistance - 10) / (float)maxDistance);

        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if(SelectedOrb)
            {
                OrbControl ocx = SelectedOrb.GetComponent<OrbControl>();
                ocx.HighlightDisable();
            }
            SelectedOrb = hitInfo.transform.GetComponent<OrbControl>();
            OrbControl oc = SelectedOrb.GetComponent<OrbControl>();
            oc.HighlightSelected();
        }

       

        if (SelectedOrb)
        {
            OrbControl oc = SelectedOrb.GetComponent<OrbControl>();
            oc.HighlightSelected();
            //SerializedObject haloComponent = new SerializedObject(SelectedOrb.GetComponent("Halo"));
            //haloComponent.FindProperty("m_Color").colorValue = Color.white;

        }

        if (SelectedOrb && Input.GetKey(KeyCode.W))
        {
            var position = SelectedOrb.transform.position;
            position += SelectedOrb.transform.forward * speed;
            SelectedOrb.transform.position = position;
        }
        if (SelectedOrb && Input.GetKey(KeyCode.A))
        {
            var position = SelectedOrb.transform.position;
            position -= SelectedOrb.transform.right * speed;
            SelectedOrb.transform.position = position;
        }
        if (SelectedOrb && Input.GetKey(KeyCode.S))
        {
            var position = SelectedOrb.transform.position;
            position -= SelectedOrb.transform.forward * speed;
            SelectedOrb.transform.position = position;
        }
        if (SelectedOrb && Input.GetKey(KeyCode.D))
        {
            var position = SelectedOrb.transform.position;
            position += SelectedOrb.transform.right * speed;
            SelectedOrb.transform.position = position;
        }
        if (SelectedOrb && Input.GetKey(KeyCode.Space))
        {
            var position = SelectedOrb.transform.position;
            position += SelectedOrb.transform.up * speed;
            SelectedOrb.transform.position = position;
        }
        if (SelectedOrb && Input.GetKey(KeyCode.X))
        {
            var position = SelectedOrb.transform.position;
            position -= SelectedOrb.transform.up * speed;
            SelectedOrb.transform.position = position;
        }
    }
}
