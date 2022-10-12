using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour
{
     [SerializeField] float _amplitude = 1;
    [SerializeField] float _frequency = 1;
    [SerializeField] int _angle = 180;
    [SerializeField] int _numberOfPoints = 1;
    [SerializeField] int _numberOfRotation = 1;
    [SerializeField] float _heightBetweenStep = 0.01f;
    [SerializeField] GameObject  _prefab;
    GameObject obj;
     public float interval;
    WaitForSeconds waitTime;   
    GameObject parent;     
    // Start is called before the first frame update
    void Start()
    {
        for(int j = 0 ; j <_numberOfRotation ; j++)
        {
            parent = new GameObject();
            parent.transform.SetParent(this.transform);
            float rotationAngle = j * _angle / _numberOfRotation;

            for(int i = 0 ; i <= _numberOfPoints ; i++){
            
                GameObject obj = Instantiate(_prefab, parent.transform);
                obj.transform.SetParent(parent.transform);
                obj.AddComponent<Move>();
                obj.GetComponent<Move>().interval = _heightBetweenStep * i;
                obj.GetComponent<Move>()._frequency = _heightBetweenStep * i;
                obj.GetComponent<Renderer>().material.color = Random.ColorHSV();

                float angle = i * _angle / _numberOfPoints;
                float x = (angle * Mathf.Deg2Rad) * Mathf.Cos(angle * Mathf.Deg2Rad * _frequency) * _amplitude;
                float y = 0.1f * i;
                float z = (angle * Mathf.Deg2Rad) * Mathf.Sin(angle * Mathf.Deg2Rad * _frequency) * _amplitude;

                obj.transform.position = new Vector3(x, y, z);
            }

            parent.transform.localRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }
}
