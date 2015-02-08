using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ffd.Data
{
    public class ProductItemJersey
    {
        public enum PreviewImageBackgroundColorType
        {
            Auto = 0,
            Light = 1,
            Dark = 2
        }

        private Image _itemImage;
        // private Template _template;
        private PlayerSeason _playerSeason;
        private string _color = string.Empty;
        private PreviewImageBackgroundColorType _previewImageBackgroundColor = PreviewImageBackgroundColorType.Auto;
        private Material _material;

        public Image ItemImage
        {
            get { return _itemImage; }
            set { _itemImage = value; }
        }

        //public Template Template
        //{
        //    get { return _template; }
        //    set { _template = value; }
        //}

        public PlayerSeason PlayerSeason
        {
            get { return _playerSeason; }
            set { _playerSeason = value; }
        }

        /// <summary>
        /// The color to use in hex RRGGBB format.
        /// </summary>
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// The material of this item.
        /// </summary>
        public Material Material
        {
            get { return _material; }

            set 
            { 
                _material = value;

                if (_material != null)
                {
                    //
                    // Add a little bit of convenience - automatically set the color property from the material's color.
                    //
                    _color = _material.RGBColorHex;
                }
            }
        }

        /// <summary>
        /// The type of background color processing to use.
        /// </summary>
        public PreviewImageBackgroundColorType PreviewImageBackgroundColor
        {
            get { return _previewImageBackgroundColor; }
            set { _previewImageBackgroundColor = value; }
        }


        #region Constructors
        public ProductItemJersey() : base()
        {
        }

        public ProductItemJersey(PlayerSeason playerSeason, Template template)
            : this()
        {
            _playerSeason = playerSeason;
            _playerSeason.TemplateCurrent = template;
        }
        #endregion




    }
}
