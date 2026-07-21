using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopProduct", menuName = "Configs/Shop")]
public class DataProduct : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public uint Price { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

}
