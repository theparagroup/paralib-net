﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Bootstrap.Grids;
using com.parahtml.Core;

namespace com.paraquery.Bootstrap
{
    public class Bs
    {
        protected HtmlContext _context;

        public Bs(HtmlContext context)
        {
            _context = context;
        }

        public IGrid Grid(bool fluid=false)
        {
            return new FluentGrid(_context,o=> { o.ContainerClass = fluid ? "container-fluid" : "container"; o.RowClass = "row"; });
        }
    }
}
