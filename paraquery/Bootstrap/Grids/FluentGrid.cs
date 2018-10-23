using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html;
using com.paraquery.Html.Attributes;
using com.paraquery.Html.Fluent;

namespace com.paraquery.Bootstrap.Grids
{
    public partial class FluentGrid:FluentHtml, IGrid, IContainer, IRow, IColumn
    {
        protected IList<string> _classes;
        protected int _columnNumber;

        public FluentGrid(IContext context, TagBuilder tagBuilder) : base(context, tagBuilder)
        {
        }

        IGrid IGrid.SetClasses(IList<string> classes)
        {
            _classes = classes;
            return this;
        }

        IContainer IContainer.SetClasses(IList<string> classes)
        {
            _classes = classes;
            return this;
        }

        IRow IRow.SetClasses(IList<string> classes)
        {
            _classes = classes;
            return this;
        }

        IColumn IColumn.SetClasses(IList<string> classes)
        {
            _classes = classes;
            return this;
        }


        public IContainer Container(bool fluid = false)
        {
            return Container(null, null, fluid);
        }

        public IContainer Container(object attributes, bool fluid = false)
        {
            return Container(null, attributes, fluid);
        }

        public IContainer Container(Action<GlobalAttributes> attributes, object additional = null, bool fluid = false)
        {
            Div(attributes, new { @class = fluid ? "container-fluid" : "container", additional = additional });
            _stack.Peek().Extra = "container";
            return this;
        }
        

        protected void CloseRow()
        {
            //end all elements up to last row or grid/container
            //end last (current) row
            //do not end container
            while (_stack.Count > 0)
            {
                Element top = _stack.Peek();

                if (top.Extra=="row")
                {
                    Pop();
                    break;
                }
                else if(top.Extra == "grid" || top.Extra=="container")
                {
                    break;
                }
                else
                {
                    Pop();
                }
            }

            _columnNumber = 0;

        }

        public IRow Row(object attributes = null)
        {
            return Row(null, attributes);
        }

        public IRow Row(Action<GlobalAttributes> attributes, object additional = null)
        {
            CloseRow();
            Div(attributes, new { @class =  "row", additional = additional });
            _stack.Peek().Extra = "row";
            return this;
        }

        protected void CloseColumn()
        {
            //end all elements up to last column
            //end last (current) column
            //don't end grids, row or containers
            while (_stack.Count > 0)
            {
                Element top = _stack.Peek();

                if (top.Extra == "column")
                {
                    Pop();
                    break;
                }
                else if (top.Extra == "grid" || top.Extra == "container" || top.Extra == "row")
                {
                    break;
                }
                else
                {
                    Pop();
                }
            }

        }


        public IColumn Column(object attributes = null)
        {
            return Column(null, attributes);
        }

        public IColumn Column(Action<GlobalAttributes> attributes, object additional = null)
        {
            CloseColumn();

            string columnClasses = null;

            if (_classes != null && _columnNumber < _classes.Count)
            {
                columnClasses = _classes[_columnNumber];
            }

            Div(attributes, new { @class = columnClasses, additional = additional });
            _stack.Peek().Extra = "column";

            ++_columnNumber;

            return this;
        }

        public IGrid Grid()
        {
            var grid=new BlockElement(_context, _tagBuilder, "div", render:false );
            grid.Extra = "grid";
            Push(grid);
            return this;
        }


        public IGrid EndGrid()
        {
            //end all elements up to last grid, if any
            //end last (current) grid
            while (_stack.Count > 0)
            {
                Element top = _stack.Peek();

                if (top.Extra == "grid")
                {
                    Pop();
                    break;
                }
                else
                {
                    Pop();
                }
            }


            return this;
        }

    }
}
