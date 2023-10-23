using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray,out hitInfo,20))
        {
            var mesh = hitInfo.collider.GetComponent<MeshFilter>().sharedMesh;
            var index = mesh.triangles[hitInfo.triangleIndex * 3]; //to get the first vertex of a triangle 
            var point = mesh.vertices[index];
            var material = hitInfo.collider.GetComponent<MeshRenderer>().material;
            var anim = hitInfo.collider.GetComponent<Animation>();
            hitInfo.collider.GetComponent<Scratches>().OnImpact(point);
            material.SetVector("_ImpactPosition", point);
            if (anim.isPlaying)
                anim.Stop();
            anim.Play();


        }
    }

}
