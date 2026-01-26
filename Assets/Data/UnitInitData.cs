using UnityEngine;

[CreateAssetMenu]
public class UnitInitData : ScriptableObject
{
    [field: SerializeField] public UnitActor UnitPrefab {  get; private set; }
    [field: SerializeField] public float DefaultSpeed { get; private set; } = 1.5f;
}
