using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderVision : MonoBehaviour
{
    [SerializeField]
    protected LineRenderer m_LineRenderer;

    [SerializeField]
    protected Camera m_Camera;

    protected List<Vector2> m_Points;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if (m_LineRenderer == null)
        {
            Debug.LogWarning("DrawLine: Line Renderer not assigned, Adding and Using default Line Renderer.");
            CreateDefaultLineRenderer();
        }
    }
        // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if (hit.collider != null)
        {
            Debug.Log(hit.point);
            m_LineRenderer.SetPosition(0, this.transform.position);

            m_LineRenderer.SetPosition(1, hit.point);
        }

          
    }
    public static void DumpToConsole(object obj)
    {
        var output = JsonUtility.ToJson(obj, true);
        Debug.Log(output);
    }
    protected virtual void CreateDefaultLineRenderer()
    {
        m_LineRenderer = gameObject.AddComponent<LineRenderer>();
        m_LineRenderer.positionCount = 0;
        m_LineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        m_LineRenderer.startColor = Color.black;
        m_LineRenderer.endColor = Color.black;
        m_LineRenderer.startWidth = 0.2f;
        m_LineRenderer.endWidth = 0.2f;
        m_LineRenderer.useWorldSpace = true;
    }

}
