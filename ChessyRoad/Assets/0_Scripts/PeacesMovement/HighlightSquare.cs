using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSquare : MonoBehaviour
{
    public Material m_HighlightMat, m_SquareMat;
    private MeshRenderer m_Renderer;
    void Awake()
    {
        m_Renderer = GetComponentInChildren<MeshRenderer>();
    }
    public void SquareHighlighted()
    {
        if (m_Renderer.material != m_HighlightMat) m_Renderer.material = m_HighlightMat;
    }
    public void SquareNotSelected()
    {
        if (m_Renderer.material != m_SquareMat) m_Renderer.material = m_SquareMat;
    }
}
