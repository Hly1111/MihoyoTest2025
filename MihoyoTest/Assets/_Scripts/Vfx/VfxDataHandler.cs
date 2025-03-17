using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VfxDataHandler
{
    [field: SerializeField] public SerializableDictionary<EVfxType, VfxData> VfxDataDictionary { get; private set; }

    public void PlayVfx(EVfxType eVfxType)
    {
        if (VfxDataDictionary.Dictionary.TryGetValue(eVfxType, out VfxData vfxData))
        {
            vfxData.SpawnVfx();
        }
    }
}