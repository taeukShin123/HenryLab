using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigonometric : MonoBehaviour
{
    [SerializeField] int _amplitude = 1;
    [SerializeField] int _frequency = 1;
    [SerializeField] int _angle = 180;
    [SerializeField] int _numberOfPoints = 1;
    [SerializeField] int _numberOfRotation = 1;
    [SerializeField] GameObject  _prefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int j = 0 ; j <_numberOfRotation ; j++)
        {
            GameObject parent = new GameObject();
            parent.transform.SetParent(this.transform);
            float rotationAngle = j * _angle / _numberOfRotation;

            for(int i = 0 ; i <= _numberOfPoints ; i++){
            
                GameObject obj = Instantiate(_prefab, parent.transform);
                obj.transform.SetParent(parent.transform);
                obj.AddComponent<Move>();
                obj.GetComponent<Move>().interval = 0.1f * i;
                obj.GetComponent<Move>()._frequency = 0.1f * i;
                obj.GetComponent<Renderer>().material.color = Random.ColorHSV();

                float angle = i * _angle / _numberOfPoints;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _amplitude;
                float y = Mathf.Sin(angle * Mathf.Deg2Rad) * _amplitude;
                float z = transform.position.z;

                obj.transform.position = new Vector3(x, y, z);
            }

            parent.transform.localRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Mathf.Cos(Time.time * _frequency) * _amplitude;
        //float y = Mathf.Sin(Time.time * _frequency) * _amplitude;
        //float z = transform.position.z;

        //transform.position = new Vector3(x, y, z);
    }

   /* void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Random.ColorHSV();

        for(int i = 0 ; i < this.transform.childCount ; i++)
        {
            Transform child = this.transform.GetChild(i);
            
            for(int j = 0 ; j < child.childCount ; j++)
            {
                Transform childChild = child.GetChild(j);

               Gizmos.DrawSphere(childChild.position, .05f);
            }
        }
    }*/
}

public class Move  : MonoBehaviour
{
    [SerializeField] int _amplitude = 1;
    public float _frequency = 1;
    public float interval;
    WaitForSeconds waitTime;

    void Start()
    {
        waitTime = new WaitForSeconds(interval);
        StartCoroutine(CoSinWaveY());
    }

  
    IEnumerator CoSinWaveY()
    {
        yield return waitTime;

        while(true)
        {
            yield return null;

            float x = _frequency + Mathf.Cos(Time.time * _frequency) * _amplitude;

            float y = _frequency + Mathf.Sin(Time.time * _frequency) * _amplitude;
            float z = transform.localPosition.z;
            transform.localPosition = new Vector3(x, y, z);
        }
    }

}