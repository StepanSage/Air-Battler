using UnityEngine;

[CreateAssetMenu(fileName = "Ability Config", menuName = "Configs/Ability")]
public class AbilityConfig : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float ShootSpeed { get; private set; }
    [field: SerializeField] public int ExperienceMultiplier { get; private set; }
    [field: SerializeField] public uint CountAddGold { get; private set; }

}
