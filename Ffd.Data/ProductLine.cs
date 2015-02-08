using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ffd.Data
{
    public class ProductLine
    {
        virtual public string Description()
        {
            throw new ApplicationException("Invalid call to base class.");
        }

    }
}
