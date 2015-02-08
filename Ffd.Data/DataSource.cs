using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    public class DataSource
    {
        /// <summary>
        /// Enumerate all product lines available with this data source
        /// </summary>
        /// <returns></returns>
        virtual public List<ProductLine> GetProductLines()
        {
            throw new ApplicationException("Invalid call to base class method.");
        }

        virtual public TemplateGraphic GetTemplateGraphic(ProductLine line)
        {
            throw new ApplicationException("Invalid call to base class method.");
        }
    }
}
