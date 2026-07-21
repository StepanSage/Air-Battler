using TMPro;
using UnityEngine;

public class VisualGold : MonoBehaviour, IGoldView
{
    [SerializeField] private TMP_Text _gold;

    public void RendererGold(uint count)
    {
        _gold.text = count.ToString();
    }
}
