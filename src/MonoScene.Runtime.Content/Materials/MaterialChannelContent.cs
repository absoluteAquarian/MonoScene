﻿using System;
using System.Collections.Generic;
using System.Text;

using XNAV3 = Microsoft.Xna.Framework.Vector3;

namespace MonoScene.Graphics.Content
{
    [System.Diagnostics.DebuggerDisplay("{Target}")]
    public class MaterialChannelContent
    {
        #region lifecycle
        internal MaterialChannelContent(string name)
        {
            _Target = name;
            _Value = _GetDefaultValue(name);
        }

        private static float[] _GetDefaultValue(string key)
        {
            switch (key)
            {
                case "Normal":                
                case "Occlusion":
                case "ClearCoat":
                case "ClearCoatNormal":
                case "ClearCoatRoughness":
                    return new float[] { 1 }; // Amount/Scale/Factor

                case "Emissive": return new float[] { 0, 0, 0 }; // RGB

                case "BaseColor":
                case "Diffuse":
                    return new float[] { 1, 1, 1, 1 }; // RGBA

                case "MetallicRoughness":
                    return new float[] { 1, 1 }; // R:Metallic G:Roughness

                case "SpecularGlossiness":
                    return new float[] { 1, 1, 1, 1 }; // RGB:Specular, A:Glossiness                
            }

            throw new NotImplementedException("Material channel is not supported: " + key);
        }

        #endregion

        #region data

        private readonly string _Target;

        private int _VertexIndexSet;

        private XNAV3 _TransformU = XNAV3.UnitX;
        private XNAV3 _TransformV = XNAV3.UnitY;

        private float[] _Value;

        private SamplerStateContent _Sampler;

        private int _TextureIndex = -1;

        #endregion

        #region properties

        public string Target => _Target;

        public int VertexIndexSet
        {
            get => _VertexIndexSet;
            set => _VertexIndexSet = value;
        }

        public (XNAV3 U, XNAV3 V) Transform
        {
            get => (_TransformU, _TransformV);
            set { _TransformU = value.U; _TransformV = value.V; }
        }

        public SamplerStateContent Sampler
        {
            get => _Sampler;
            set => _Sampler = value;
        }

        /// <summary>
        /// An array with a minimum of 1 element and a maximum of 4 elements.
        /// </summary>
        /// <remarks>
        /// If this channel does not have a texture, this represents the
        /// overall value of this channel. Else, the texture samples must
        /// be scaled by this value
        /// </remarks>
        public float[] Value
        {
            get => _Value;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (value.Length < 1 || value.Length > 4) throw new ArgumentOutOfRangeException(nameof(value));
                _Value = value;
            }
        }

        /// <summary>
        /// Index to a texture at <see cref="MaterialCollectionContent.Textures"/> or -1 if unused.
        /// </summary>
        public int TextureIndex
        {
            get => _TextureIndex;
            set => _TextureIndex = value;
        }

        #endregion
    }
}
