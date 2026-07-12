using UnityEngine;

[CreateAssetMenu(fileName ="Skill")]
public class SkillsData : ItemData
{
    public float skillCooldown;
    public enum SkillType {Mobility, Offence, Burst}
}
