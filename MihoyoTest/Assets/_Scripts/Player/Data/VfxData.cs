using System;
using Core;
using UnityEngine;

[Serializable]
public enum EVfxType
{
    LighteningImpact,
    AttackOne,
    AttackTwo,
    AttackThree,
}

[Serializable]
public struct VfxData
{
    [field: SerializeField] public string VfxName { get; private set; }
    [field: SerializeField] public string SfxName { get; private set; }
    [field: SerializeField] public Transform VfxSpawnPoint { get; private set; }

    public void SpawnVfx()
    {
        Vector3 position = VfxSpawnPoint.position;
        Quaternion rotation = VfxSpawnPoint.rotation;
        ObjectPool.Instance.GetObject(VfxName, (vfx) =>
        {
            vfx.transform.position = position;
            vfx.transform.rotation = rotation;
        });
        AudioManager.Instance.PlaySfx(SfxName);
    }
}