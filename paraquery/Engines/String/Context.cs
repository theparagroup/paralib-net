﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.StringContext
{
    public class Context: Engines.Base.Context
    {
        public Context(string urlPrefix, string @namespace, Dictionary<string, string> namespaceVars) : base(@namespace, namespaceVars)
        {
            Server = new Server(this, urlPrefix);
            Request= new Request(this);
            Response= new Response(this);
        }

    }
}