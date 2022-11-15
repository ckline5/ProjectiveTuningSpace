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

    Transform collider;
    Transform textObj;
    BoxCollider col;
    TextMesh text;


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


        collider = transform.GetChild(0);
        col = collider.gameObject.GetComponent<BoxCollider>();
        textObj = transform.GetChild(1);
        text = textObj.gameObject.GetComponent<TextMesh>();
    }

    public void SetWidth(float width)
    {
        line.startWidth = width;
        line.endWidth = line.startWidth;
    }

    public void RemoveCollider()
    {
        Destroy(collider.gameObject);
        return;
    }

    public void AddCollider()
    {   
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

    public void SetText(string textString)
    {
        if (lineType == LineType.LINE)
            text.anchor = TextAnchor.MiddleLeft;
        else if (lineType == LineType.CIRCLE)
            text.anchor = TextAnchor.UpperLeft;

        text.text = textString;
    }

    public void SetTextPosition()
    {
        //for a circle, the position is at the first/last point on the line
        //for a line, the position is at the last point on the line
        //so, we can do last point for both
        //text should be rotated to be in line with line, for a line

        Vector3 m1 = line.GetPosition(0);
        Vector3 m2 = line.GetPosition(1);
        Vector3 v = m1 - m2;

        textObj.transform.localPosition = m2;

        textObj.transform.rotation = Quaternion.identity;
        if (lineType == LineType.LINE)
        {
            float atan = v.x == 0 ? (v.z < 0 ? (-Mathf.PI / 2) : (v.z > 0 ? Mathf.PI / 2 : 0)) : Mathf.Atan(v.z / v.x);
            //Debug.Log(v + " " + atan);
            textObj.transform.Rotate(new Vector3(0, 0, atan * Mathf.Rad2Deg));
        }
    }
}
