using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintController : MonoBehaviour
{
    public static PaintController currentPaint = null;

    public Color paintColor = Color.clear;
    public Vector2Int lastPos;
    public Vector2 lastWorldPos;
    public List<Vector2> drawPoints;
    public bool isDelete = false;
    public bool isPaint = false;

    public int erSize = 10;

    protected Texture2D m_Texture;
    protected Collider2D drawBoundCollider;
    protected Color[] originalColors;
    protected Color[] m_Colors;
    [SerializeField]
    float perCent = 0.6f;

    bool canDraw = false;
    bool selected;


    GameObject erase;
    SpriteRenderer renderer;
    public virtual Texture2D GetSourceTexture()
    {
      /*  var*/ renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            return renderer.sprite.texture;
        }
        return null;
    }

    public virtual void ApplyTexture(Texture2D texture2D)
    {
       // var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = Sprite.Create(m_Texture, renderer.sprite.rect, new Vector2(0.5f, 0.5f));
        }
    }
    Vector2 posOriginal;

    public virtual void Start()
    {
        erase = GameController.instance.eraser;
        posOriginal = erase.transform.position;
        Init();
    }

    public void Init()
    {
        var tex = GetSourceTexture();
        m_Texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        m_Texture.filterMode = FilterMode.Bilinear;
        m_Texture.wrapMode = TextureWrapMode.Clamp;
        m_Colors = tex.GetPixels();
        originalColors = tex.GetPixels();
        m_Texture.SetPixels(m_Colors);
        m_Texture.Apply();
        drawPoints = new List<Vector2>();
        canDraw = false;

        drawBoundCollider = GetComponent<Collider2D>();

        ApplyTexture(m_Texture);


        Debug.LogError("====== step active");
    }
    Vector2 pos;
    void Update()
    {
        if (!DataParam.canDelete)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            canDraw = true;
            erase.transform.position = pos;
        }

        if (!canDraw)
        {
            isDelete = false;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool inside = drawBoundCollider.OverlapPoint(pos);
            erase.transform.position = pos;
            if (inside && currentPaint == null)
            {
                currentPaint = this;
            }

            if (inside && currentPaint == this)
            {
                
                UpdateTexture(pos);
                isDelete = true;
                return;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentPaint == this)
            {
                currentPaint = null;
            }
            erase.transform.position = posOriginal;
        }

        isDelete = false;
    }

    public void UpdateTexture(Vector2 pos)
    {
        int w = m_Texture.width;
        int h = m_Texture.height;
        lastWorldPos = pos;
        drawPoints.Add(pos);

        var mousePos = pos - (Vector2)drawBoundCollider.bounds.min;
        mousePos.x *= w / drawBoundCollider.bounds.size.x;
        mousePos.y *= h / drawBoundCollider.bounds.size.y;
        Vector2Int p = new Vector2Int((int)mousePos.x, (int)mousePos.y);
        Vector2Int start = new Vector2Int();
        Vector2Int end = new Vector2Int();
        if (!isDelete)
            lastPos = p;
        start.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) - erSize, 0, w);
        start.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) - erSize, 0, h);
        end.x = Mathf.Clamp(Mathf.Max(p.x, lastPos.x) + erSize, 0, w);
        end.y = Mathf.Clamp(Mathf.Max(p.y, lastPos.y) + erSize, 0, h);
        Vector2 dir = p - lastPos;
        for (int x = start.x; x < end.x; x++)
        {
            for (int y = start.y; y < end.y; y++)
            {
                Vector2 pixel = new Vector2(x, y);
                Vector2 linePos = p;
                if (isDelete)
                {
                    float d = Vector2.Dot(pixel - lastPos, dir) / dir.sqrMagnitude;
                    d = Mathf.Clamp01(d);
                    linePos = Vector2.Lerp(lastPos, p, d);
                }

                if ((pixel - linePos).sqrMagnitude <= erSize * erSize)
                {
                    m_Colors[x + y * w] = paintColor;
                }
            }
        }
        lastPos = p;
        m_Texture.SetPixels(m_Colors);
        m_Texture.Apply();

        ApplyTexture(m_Texture);
    }

    /// <summary>
    /// Check if paint is enough base on PERCENT
    /// </summary>
    /// <returns></returns>
    public bool IsDeleteFinish()
    {
        if (m_Colors == null) return false;

        float count = 0;

        for (int i = 0; i < m_Colors.Length; i++)
        {
            if (m_Colors[i] == paintColor)
            {
                count++;
            }
        }

        float percent = count / m_Colors.Length;

        return percent > perCent;
    }

    public void ClearDelete()
    {
        m_Texture.SetPixels(originalColors);
        m_Texture.Apply();
        ApplyTexture(m_Texture);

        Init();

        if (currentPaint == this)
        {
            currentPaint = null;
        }
    }
}
