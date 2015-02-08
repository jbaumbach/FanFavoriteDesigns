using Ffd.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;

namespace Ffd.Data
{
    public class DataSourceSql : DataSource
    {
        public TemplateGraphic GetTemplateGraphic(ProductLine line, Template template)
        {
            if (typeof(ProductLineJersey).IsInstanceOfType(line))
            {
                return GetTemplateGraphicJersey((ProductLineJersey)line, template);
            }
            else
            {
                throw new ApplicationException("Product line type not implemented.");
            }
        }

        private TemplateGraphicJersey GetTemplateGraphicJersey(ProductLineJersey line, Template template)
        {
            TemplateGraphicJersey result = new TemplateGraphicJersey();

            TemplateAttrDataSet ds = new TemplateAttrDataSet(string.Format("select * from [dbo].[TEMPLATE_ATTRIBUTES] where template_id = {0}", line.TemplateId));

            //
            // Actual pixels (easier to calculate)
            //
            result.NameBoundingBox = ds.GetGraphicRect(TemplateAttrDataSet.TemplateAttrTypeCode.tatcNameBoundingBoxRect);
            result.NumberBoundingBox = ds.GetGraphicRect(TemplateAttrDataSet.TemplateAttrTypeCode.tatcNumberBoundingBoxRect);

            result.NameManualPositionAdjustments = ds.GetManualPositionAdjustments(TemplateAttrDataSet.TemplateAttrTypeCode.tatcManualPosAdjsNameFontHeight, 
                TemplateAttrDataSet.TemplateAttrTypeCode.tatcManualPosAdjsNameFontVerticalPosition);

            result.NumberManualPositionAdjustments = ds.GetManualPositionAdjustments(TemplateAttrDataSet.TemplateAttrTypeCode.tatcManualPosAdjsNumberFontHeight, 
                TemplateAttrDataSet.TemplateAttrTypeCode.tatcManualPosAdjsNumberFontVertPosition);

            result.NameFont = ds.GetValueFromRowSet((int)TemplateAttrDataSet.TemplateAttrTypeCode.tatcNameFontName).ToString();// "Comic Sans MS";
            
            string templateFileName = string.Format("{0}\\Source Files\\{1}", Config.GraphicsRootDirectory(), 
                        string.Format("{0}_{1:000}.bmp", template.TemplateDescShort, template.TemplateId));

            result.TemplateImage = Image.FromFile(templateFileName);

            return result;
        }


    }
}
