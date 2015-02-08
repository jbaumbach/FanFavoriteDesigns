using Ffd.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Text;

namespace Ffd.Data
{
    public class DataSourceFileSystem : DataSource
    {
        public override TemplateGraphic GetTemplateGraphic(ProductLine line)
        {
            if (typeof(ProductLineJersey).IsInstanceOfType(line))
            {
                return GetTemplateGraphicJersey((ProductLineJersey)line);
            }
            else
            {
                throw new ApplicationException("Product line type not implemented.");
            }
        }

        private TemplateGraphicJersey GetTemplateGraphicJersey(ProductLineJersey line)
        {
            TemplateGraphicJersey result = new TemplateGraphicJersey();

            //
            // Actual pixels (easier to calculate)
            //
            result.NameBoundingBox = new Rectangle(400, 300, 700, 150);
            result.NumberBoundingBox = new Rectangle(450, 550, 600, 500);

            result.NameManualPositionAdjustments = new ManualPositionAdjustments(1.15f, -20);
            result.NumberManualPositionAdjustments = new ManualPositionAdjustments(1.20f, -105);

            //InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            //PrivateFontCollection pf = new PrivateFontCollection();
            //pf.AddFontFile("C:\\ffdgraphics\\United Sans Cond Black\\UnitedSansCdBk.ttf");
            //FontFamily[] ff = pf.Families;

            result.NameFont = "United Sans Cd Bk";

            // result.NameFont = "Comic Sans MS";

            string templateFileName = string.Format("{0}\\{1}", Config.GraphicsRootDirectory(), "jersey-template-05.bmp");

            result.TemplateImage = Image.FromFile(templateFileName);

            return result;
        }
    }
}
