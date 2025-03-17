using UnityEngine;
public class SkinMeshHandler : MonoBehaviour
{
        private SkinnedMeshRenderer[] _skinMeshes;
        
        private void Awake()
        {
                _skinMeshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        }
        
        public void ShowSkin(bool show)
        {
                foreach (SkinnedMeshRenderer skinMesh in _skinMeshes)
                {
                        skinMesh.enabled = show;
                }
        }
}