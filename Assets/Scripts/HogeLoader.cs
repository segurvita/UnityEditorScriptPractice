using UnityEngine;

public class HogeLoader : MonoBehaviour
{
    private Hoge m_hoge;

    private void Start()
    {
        m_hoge = ScriptableObject.Instantiate(Resources.Load<Hoge>("Hoge"));

        Debug.Log(m_hoge.IntValue);
    }
}
