using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratches : MonoBehaviour
{
    private Vector4[] impacts = new Vector4[3];
    private int targetIndex;
    private Vector4 target;
    [Min(0.3f)]
    public float expandDist = 1;
    public float spreadSpeed = 2;
    public Vector2 hitStrengthRange = new Vector2(0.8f, 2f);
    private Material material;
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

      if (impacts[targetIndex] != target)
        {
            impacts[targetIndex] = Vector4.MoveTowards(impacts[targetIndex], target, Time.deltaTime * spreadSpeed);
            material.SetVector("_Impact" + (targetIndex + 1), impacts[targetIndex]);
        }
    }
    public void OnImpact(Vector3 point)
    {
 

        //Get Target Impact
        var impactDist = (GetVec3(impacts[0]) - point).magnitude;
        targetIndex = 0;
        for(int i=1;i<3;i++)
        {
            var impact = GetVec3(impacts[i]);
            var dist = (impact - point).magnitude;
            if (impact == Vector3.zero)
            {
                if (impactDist > expandDist && impacts[targetIndex] != Vector4.zero)
                    targetIndex = i;
                break;
            }

            if (dist < impactDist)
            {
                impactDist = dist;
                targetIndex = i;
            }
        }
  
        //set only if it's empty vector.zero
        if(impacts[targetIndex]== Vector4.zero)
        SetVect3(point, ref impacts[targetIndex]);
        target = impacts[targetIndex];
        target.w += Random.Range(hitStrengthRange.x, hitStrengthRange.y); ;
        Debug.Log("DD");
    }
   
    private Vector3 GetVec3(Vector4 v) => new Vector3(v.x, v.y, v.z);
    private void SetVect3(Vector3 value ,ref Vector4 vector)
    {
        vector.x = value.x;
        vector.y = value.y;
        vector.z = value.z;
    }
}
