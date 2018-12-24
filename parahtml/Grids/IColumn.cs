using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Attributes;
using com.parahtml.Html;
using com.parahtml.Core;

namespace com.parahtml.Grids
{
    public interface IColumn
    {
        IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow Row(string @class, string[] columnClasses = null);
        IRow Row(string[] columnClasses = null);

        IColumn Column(Action<GlobalAttributes> attributes, Action action = null);
        IColumn Column(string @class, Action action = null);
        IColumn Column(Action action = null);
    }
}
