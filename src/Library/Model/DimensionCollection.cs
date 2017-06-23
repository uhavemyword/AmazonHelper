// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/16/2016 4:23:27 PM
// ------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Library.Model
{
    public class DimensionCollection : List<Dimension>
    {
        public override string ToString()
        {
            var str = string.Join(",", this.Select(x => x.Value).ToArray());
            return str;
        }
    }
}