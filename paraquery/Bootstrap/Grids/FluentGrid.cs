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
    /*
        FluentGrid.

        We derive from FluentHtml, which is a RendererStack, so we can wrap standard HTML content inside our grid tags.

        We use the interfaces to control what kind of content can be pushed onto various other content.

        IGrid
            IContainer    
            IRow

        IContainer
            IRow

        IRow
            IRow
            IColumn

        IColumn
            IGrid
            IRow
            IColumn
            HTML

        When you add a Row to a Row, the last Row is closed. 
         
        When you add a Column to a Column, the last Column is closed.
        
        Grids don't render, but are used as "markers" in the stack for when we call EndGrid(), we know when to stop.
        
        You can define the container div outside of paraquery, such as in a Razor layout.   
            
        Using SetClasses(), when columns are generated under a row, the classes can be automatically populated. This
        is helpful when generating uniform grids, such as edit forms with labels and controls.


    */

    public partial class FluentGrid : FluentHtml, IGrid, IContainer, IRow, IColumn
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

        public IContainer Container(Action<GlobalAttributes> init = null, bool fluid = false)
        {
            return Push(new Container(_tagBuilder, _tagBuilder.Attributes(init, new { @class = fluid ? "container-fluid" : "container" })));
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
                else if (top is Grid || top is Container)
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

        public IRow Row(Action<GlobalAttributes> init)
        {
            CloseRow();
            return Push(new Row(_tagBuilder, _tagBuilder.Attributes(init, new { @class = "row"})));
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

        public IColumn Column(string classes)
        {
            return Column(a => a.Class = classes);
        }

        public IColumn Column(Action<GlobalAttributes> init = null)
        {
            CloseColumn();

            string columnClasses = null;

            if (_classes != null && _columnNumber < _classes.Count)
            {
                columnClasses = _classes[_columnNumber];
            }

            ++_columnNumber;

            return Push(new Column(_tagBuilder, _tagBuilder.Attributes(init, new { @class = columnClasses })));
        }

        public IGrid Grid()
        {
            var grid = new Grid(_context);
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
