using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.ShaderGlobals.Components
{
    [AddComponentMenu("Shaders/Transform Vector")]
    [ExecuteAlways, ExecuteInEditMode]
    public class TransformVector : MonoBehaviour
    {
        public enum Source { Position, Direction, Scale }
        public enum Mode { Global, ByMaterial, ByPropertyBlock }

        [SerializeField] Source source;
        [SerializeField] string vectorReferenceName = "_CustomVector";
        [SerializeField] Mode mode = Mode.Global;
        [SerializeField] Renderer[] targets;

        MaterialPropertyBlock _materialPropertyBlock;
        MaterialPropertyBlock materialPropertyBlock
        {
            get
            {
                if (_materialPropertyBlock is null)
                {
                    _materialPropertyBlock = new MaterialPropertyBlock();
                }
                return _materialPropertyBlock;
            }
        }

        private void Awake()
        {
            if (mode == Mode.ByPropertyBlock && targets.Length > 0)
            {
                materialPropertyBlock.SetVector(vectorReferenceName, vector);
                foreach (var renderer in targets)
                {
                    renderer.SetPropertyBlock(materialPropertyBlock);
                }
            }
        }

        Vector4 vector
        {
            get => source switch
            {
                Source.Position => transform.position,
                Source.Direction => transform.forward,
                Source.Scale => transform.localScale,
            };
        }

        private void Update()
        {
            switch (mode)
            {
                case Mode.Global:
                    Shader.SetGlobalVector(vectorReferenceName, vector);
                    break;
                case Mode.ByMaterial:
                    if (targets.Length == 0)
                        break;
                    foreach (var renderer in targets)
                    {
                        renderer.material.SetVector(vectorReferenceName, vector);
                    }
                    break;
                case Mode.ByPropertyBlock:
                    materialPropertyBlock.SetVector(vectorReferenceName, vector);
                    break;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.cyan;
            switch (source)
            {
                case Source.Position:
                    break;
                case Source.Direction:
                    Gizmos.DrawRay(Vector3.zero, Vector3.forward);
                    break;
                case Source.Scale:
                    Gizmos.DrawWireCube(Vector3.zero, transform.localScale);
                    break;
            }
        }

        [ContextMenu("Reset PropertyBlocks on Targets")]
        void ResetTargets()
        {
            foreach (var renderer in targets)
            {
                renderer.SetPropertyBlock(null);
            }
        }
    }
}