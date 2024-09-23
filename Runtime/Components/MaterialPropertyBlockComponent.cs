using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.ShaderGlobals
{
    [RequireComponent(typeof(Renderer))]
    public class MaterialPropertyBlockComponent : MonoBehaviour
    {
        [System.Serializable]
        public class PropertyBlockElement<T>
        {
            public string name;
            public string reference;
            public T value;
        }

        [SerializeField] List<PropertyBlockElement<float>> floats;
        [SerializeField] List<PropertyBlockElement<Color>> colors;

        Renderer _renderer;
        new Renderer renderer
        {
            get
            {
                if (_renderer == null)
                    _renderer = GetComponent<Renderer>();
                return _renderer;
            }
        }

        public void ApplyPropertyBlock()
        {
            var propertyBlock = new MaterialPropertyBlock();
            foreach (var f in floats)
                propertyBlock.SetFloat(f.reference, f.value);
            foreach (var c in colors)
                propertyBlock.SetColor(c.reference, c.value);

            renderer.SetPropertyBlock(propertyBlock);
        }

        public void ResetPropertyBlock()
        {
            renderer.SetPropertyBlock(null);
        }
    }
}