using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;

namespace Ffd.Data
{
    public static class DataManagerOffline
    {
        /// <summary>
        /// Return a single template matching the supplied description
        /// </summary>
        /// <param name="description">String description of template to get</param>
        /// <returns>Template, or null if none found.  If more than one found, returns the first.</returns>
        public static Template GetTemplate(string description)
        {
            // List<Template> templates = GetTemplates(description);
            Template result = null;

            if (description == "Baseball")
            {
                result = new Template();

            }

            return result;
        }

        public static TemplateGraphicJersey GetTemplateGraphicJerseyAttributes(Template template)
        {
            TemplateGraphicJersey result = new TemplateGraphicJersey();

            TemplateAttrDataSet ds = new TemplateAttrDataSet(string.Format("select * from [dbo].[TEMPLATE_ATTRIBUTES] where template_id = {0}", template.TemplateId));

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

            return result;
        }

    }
}
