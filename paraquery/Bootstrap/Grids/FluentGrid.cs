using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Html.Fluent;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;

namespace com.paraquery.Bootstrap.Grids
{
    public partial class FluentGrid:FluentHtml, IGrid, IContainer, IRow, IColumn
    {
        protected IList<string> _classes;
        protected int _columnNumber;

        public FluentGrid(TagBuilder tagBuilder) : base(tagBuilder)
        {
        }

        protected new FluentGrid Push(Renderer renderer)
        {
            base.Push(renderer);
            return this;
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
            return Push(new Container(_tagBuilder, new { @class = fluid ? "container-fluid" : "container", additional = additional }));
        }
        

        protected void CloseRow()
        {
            //end all elements up to last row or grid/container
            //end last (current) row
            //do not end container
            while (_stack.Count > 0)
            {
                Renderer top = _stack.Peek();

                if (top is Row)
                {
                    Pop();
                    break;
                }
                else if(top is Grid || top is Container)
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
            return Push(new Row(_tagBuilder, new { @class = "row", additional = additional }));
        }

        protected void CloseColumn()
        {
            //end all elements up to last column
            //end last (current) column
            //don't end grids, row or containers
            while (_stack.Count > 0)
            {
                Renderer top = _stack.Peek();

                if (top is Column)
                {
                    Pop();
                    break;
                }
                else if (top is Grid || top is Container || top is Row)
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

            ++_columnNumber;

            return Push(new Column(_tagBuilder, new { @class = columnClasses, additional = additional }));
        }

        public IGrid Grid()
        {
            var grid=new Grid(_context);
            //grid.Extra = "grid";
            Push(grid);
            return this;
        }


        public IGrid EndGrid()
        {
            //end all elements up to last grid, if any
            //end last (current) grid
            while (_stack.Count > 0)
            {
                Renderer top = _stack.Peek();

                if (top is Grid)
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
