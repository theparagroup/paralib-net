﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Blocks;

namespace com.paraquery.jQuery.Blocks
{
    public abstract class jQueryBlock : Block
    {
        public jQueryBlock(IContext context) : base(context)
        {
        }

        protected override void Comment(string text)
        {
            _writer.Write($" // {text}", false);
        }

        protected override void OnPreBegin()
        {
            base.OnPreBegin();

            if (!_writer.IsSpaced)
            {
                _writer.NewLine();
            }

        }

        protected override void OnPostEnd()
        {
            base.OnPostEnd();

            if (!_writer.IsSpaced)
            {
                _writer.NewLine();
            }
        }

    }
}
