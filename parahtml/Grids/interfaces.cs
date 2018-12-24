using com.parahtml.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml.Grids
{
    public interface IContainer
    {
        IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow Row(string @class, string[] columnClasses = null);
        IRow Row(string[] columnClasses = null);
    }

    public interface IRow 
    {
        IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow Row(string @class, string[] columnClasses = null);
        IRow Row(string[] columnClasses = null);

        IColumn Column(Action<GlobalAttributes> attributes, Action action = null);
        IColumn Column(string @class, Action action = null);
        IColumn Column(Action action = null);
    }

    public interface IColumn
    {
        IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null);
        IRow Row(string @class, string[] columnClasses = null);
        IRow Row(string[] columnClasses = null);

        IColumn Column(Action<GlobalAttributes> attributes, Action action=null);
        IColumn Column(string @class, Action action = null);
        IColumn Column(Action action = null);
    }
}
