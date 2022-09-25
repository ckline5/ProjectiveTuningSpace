using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MOSLine : MonoBehaviour
{
    public LineRenderer line;

    public LineType lineType;
    public enum LineType
    {
        CIRCLE,
        LINE
    }

    public float cents;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
    }

    public void SetWidth(float width)
    {
        line.startWidth = width;
        line.endWidth = line.startWidth;
    }

    public void AddCollider()
    {
        Transform collider = transform.GetChild(0);

        if (lineType == LineType.CIRCLE)
        {
            Destroy(collider.gameObject);
            return;
        }

        BoxCollider col = collider.gameObject.GetComponent<BoxCollider>();

        Vector3 m1 = line.GetPosition(0);
        Vector3 m2 = line.GetPosition(1);

        float lineLength = Vector3.Distance(m1, m2);

        col.size = new Vector3(lineLength, line.startWidth * 2, line.startWidth * 2); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement

        Vector3 v = m1 - m2;
        collider.transform.localPosition = Vector3.Lerp(m1, m2, .5f);
        collider.transform.rotation = Quaternion.identity;
        float atan = v.x == 0 ? (v.z < 0 ? (-Mathf.PI / 2) : (v.z > 0 ? Mathf.PI / 2 : 0)) : Mathf.Atan(v.z / v.x);
        //Debug.Log(v + " " + atan);
        collider.transform.Rotate(new Vector3(0, 0, atan * Mathf.Rad2Deg));
    }
}
