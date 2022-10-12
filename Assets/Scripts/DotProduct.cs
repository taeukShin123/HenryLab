using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProduct : MonoBehaviour
{
    [SerializeField]Transform targetA, targetB;
    // Start is called before the first frame update
    void Start()
    {

    }

    void CheckDirection(Vector3 vectorA, Vector3 vectorB)
    {
        if (Vector3.Dot(vectorA, vectorB) < 0)
        {
            print("back! / the angle is " + Vector3.Dot(vectorA, vectorB) * Mathf.Rad2Deg);

        }
        else if((Vector3.Dot(vectorA, vectorB) > 0))
        {
            print("front! / the angle is " + Vector3.Dot(vectorA, vectorB) * Mathf.Rad2Deg);         
        }
        else
            print("same Z axis");
    }

    float CalculateDotProduct(Vector3 vectorA, Vector3 vectorB){
        float magnitudeVectorA = Mathf.Sqrt((vectorA.x * vectorA.x) +(vectorA.y * vectorA.y));
        float cosTheta = vectorA.x / magnitudeVectorA;  
        float work = magnitudeVectorA * vectorB.magnitude * cosTheta;
        print("VectorA 크기: " + magnitudeVectorA + " / VectorB 크기: " + vectorB.magnitude + " / 일(내적): " + work);
        return work;
    }

        // Update is called once per frame
    void Update()
    {
        Vector3 directionA = targetA.position - this.transform.position;
        Vector3 directionB = targetB.position - this.transform.position;
        CalculateDotProduct(directionA, directionB);
        //CheckDirection(transform.forward, targetA.transform.position - transform.position);
    }

    void OnValidate() {


        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = targetA.transform.position - transform.position;

        if (Vector3.Dot(transform.forward, toOther) < 0)
        {
            print("back! / the angle is " + Vector3.Dot(forward, toOther) * Mathf.Rad2Deg);

        }
        else if((Vector3.Dot(transform.forward, toOther) > 0))
        {
            print("front! / the angle is " + Vector3.Dot(forward, toOther) * Mathf.Rad2Deg);         
        }
        else
            print("same Z axis");


        Vector3 crossProduct = Vector3.Cross(transform.forward, toOther); 
        // print(crossProduct);
        if (crossProduct.x < 0)
        {
            print("up");

        }

        if(crossProduct.x > 0)
        {
            print("down");         
        }

        if(crossProduct.y < 0)
        {
            print("left");         
        }

        if(crossProduct.y > 0)
        {
            print("right");         
        }

        //print(Vector3.Cross(transform.forward, toOther));
        //  Vector2 player = new Vector2(0, 0);
        // Vector2 forward = new Vector2(0, 10);
        // Vector2 G = new Vector2(1, 5);
        // Vector2 B = new Vector2(100, 1);
        // Vector2 O = new Vector2(-1, -1);


        // Debug.Log("G is Visible: " + istargetAInSight(player, G, forward)); // True, theta = 11, dotProduct: 0.9805807
        // Debug.Log("B is Visible: " + istargetAInSight(player, B, forward)); // False, theta = 89, dotProduct: 0.009999501
        // Debug.Log("O is Visible: " + istargetAInSight(player, O, forward)); // False, theta = 135, dotProduct: -0.7071068
    }


    // Get the normal to a triangle from the three corner points, a, b and c.
    Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        // Find vectors corresponding to two of the sides of the triangle.
        Vector3 side1 = b - a;
        Vector3 side2 = c - a;

        // Cross the vectors to get a perpendicular vector, then normalize it.
        return Vector3.Cross(side1, side2).normalized;
    }



    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, targetA.transform.position);    
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, targetB.transform.position);    

    }

    int sightAngle = 135;
    bool istargetAInSight(Vector3 player, Vector3 targetA, Vector3 forward)
    {
        Vector2 targetADirection = (targetA - player).normalized;
        float dotProduct = Vector3.Dot(forward.normalized, targetADirection);
        //float angle = Vector3.Angle(forward.normalized, targetADirection);

        // 내적의 값이 > 0 이면 플레이어 앞에있고, < 0이면 뒤에있다.
        Debug.Log("dotProduct: " + dotProduct);
        //Debug.Log("각도: " + angle);

        // targetA과 Player사이의 각도
        float theta = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        Debug.Log("theta: " + theta);
        // 시야각 안에있는지 여부
        return theta <= sightAngle / 2;
    }

}
