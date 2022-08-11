using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHexagon : MonoBehaviour
{
    TuningSpace ts;
    LineRenderer line;

    private const float DAMAGE_TO_LENGTH_RATIO = 0.0808536957313274717100240214488f;

    public float damage;

    public void Initialize(float damage)
    {
        Vector3 center = ProjectionTools.Project(Tuple.Create(1f, 1f, 1f));
        float dmg = damage * DAMAGE_TO_LENGTH_RATIO;
        transform.position += center;
        line.SetPositions(new Vector3[]
        {
            new Vector3(center.x - dmg, center.y, center.z),
            new Vector3(center.x - dmg / 2, center.y + dmg * Mathf.Sqrt(3) / 2, center.z),
            new Vector3(center.x + dmg / 2, center.y + dmg * Mathf.Sqrt(3) / 2, center.z),
            new Vector3(center.x + dmg, center.y, center.z),
            new Vector3(center.x + dmg / 2, center.y - dmg * Mathf.Sqrt(3) / 2, center.z),
            new Vector3(center.x - dmg / 2, center.y - dmg * Mathf.Sqrt(3) / 2, center.z)
        });
        line.loop = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ts.OnZoomChange += ZoomChangeHandler;
    }

    void Awake()
    {
        ts = GameObject.Find("TuningSpace").GetComponent<TuningSpace>();
        line = GetComponent<LineRenderer>();
        line.startWidth = .003f * ts.Zoom/1.5f;
        line.endWidth = line.startWidth;
        line.startColor = Color.black;
        line.endColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ZoomChangeHandler(float newVal)
    {
        //resize stuff
        line.startWidth = .003f * (newVal/1.5f);
        line.endWidth = line.startWidth;
    }

    private void OnDestroy()
    {
        // Unsubscribe from event(s)
        ts.OnZoomChange -= ZoomChangeHandler;

        // And stop all coroutines
        StopAllCoroutines();

        ts.Remove(this);
    }
}
