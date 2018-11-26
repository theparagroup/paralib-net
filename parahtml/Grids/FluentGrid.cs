using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Html;
using com.parahtml.Attributes;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Grids
{
    /*
        FluentGrid. A bootstrap-style grid with a fluent interface.

        Example output:

            <!-- grid start -->
                <div class="container">
                        <div class="row">
                            <div class="col-xs-2">
                                content
                            </div>
                            <div class="col-xs-10">
                                content
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <!-- grid -->
                                    <div class="container">
                                        etc...
                                    </div>
                                <!-- end grid -->
                            </div>
                        </div>
                </div>
            <!-- grid end -->

        We derive from FluentHtml, which is a RendererStack, and re-implement the HTML methods so we can wrap 
        standard HTML content inside our grid tags fluently.

        Several custom Renderers have been created and are used as "markers" in the stack so that various
        methods that walk the stack know when to stop:

            Grid
            Container
            Row
            Column

        We use the interfaces to control what kind of content can be pushed onto various other content in
        the fluent interface. Note: these are implemented by FluentGrid and not the custom Renderers described
        above.

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
        
        You can define the "container div" outside of paraquery, such as in a Razor layout. It is not required to 
        create a container in order to create a row.  
            
        Using SetClasses(), when columns are generated under a row, the classes can be automatically populated. This
        is helpful when generating uniform grids, such as edit forms with labels and controls.


    */

    public partial class FluentGrid<C> : FluentRendererStack<C, FluentGrid<C>>, IGrid<C>, IContainer<C>, IRow<C>, IColumn<C> where C:HtmlContext
    {
        protected GridOptions _gridOptions;

        protected string[] _containerColumnClassList;
        protected string[] _rowColumnClassList;

        protected int _columnNumber;


        public FluentGrid(C context, RendererStack rendererStack, Action<GridOptions> gridOptions = null, GridOptions current=null) : base(context, rendererStack)
        {
            _gridOptions = new GridOptions();

            if (gridOptions != null)
            {
                gridOptions(_gridOptions);
            }

            Open(new GridBlock(Context, current));
        }

        public class GridBlock : DebugBlock
        {
            public GridBlock(C context, GridOptions gridOptions) : base(context, "fluent grid", context.IsDebug(DebugFlags.Grids))
            {
                Data = gridOptions;
            }
        }

        public class ContainerTag : Tag
        {
            public ContainerTag(C context, AttributeDictionary attributes, string[] columnClassList) : base(context, TagTypes.Block, "div", attributes)
            {
                Data = columnClassList;
            }
        }

        public class RowTag : Tag
        {
            public RowTag(C context, AttributeDictionary attributes, string[] columnClassList) : base(context, TagTypes.Block, "div", attributes)
            {
                Data = columnClassList;
            }
        }

        public class ColumnTag : Tag
        {
            public ColumnTag(HtmlContext context, AttributeDictionary attributes) : base(context, TagTypes.Block, "div", attributes)
            {
            }
        }

        protected void CloseContainer()
        {
            //end all columns and rows up to and including last container
            //if no row is found, end all columns up to but NOT including last grid
            while (RendererStack.Count > 0)
            {
                IRenderer top = RendererStack.Top;

                if (top is ContainerTag)
                {
                    _containerColumnClassList = top.Data as string[];
                    RendererStack.Close();
                    break;
                }
                else if (top is RowTag)
                {
                    _rowColumnClassList = top.Data as string[];
                    RendererStack.Close();
                }
                else if (top is ColumnTag)
                {
                    RendererStack.Close();
                }
                else
                {
                    break;
                }
            }

            _columnNumber = 0;

        }

        public IContainer<C> Container(Action<GlobalAttributes> attributes, string[] columnClasses = null)
        {
            CloseContainer();

            var containerTag = new ContainerTag(Context, Context.AttributeBuilder.Attributes(attributes, new { Class = _gridOptions?.ContainerClass }), _containerColumnClassList);

            _containerColumnClassList = columnClasses;

            return Open(containerTag);
        }

        public IContainer<C> Container(string @class, string[] columnClasses = null)
        {
            return Container(a=>a.Class=@class, columnClasses);
        }

        public IContainer<C> Container(string[] columnClasses = null)
        {
            return Container(attributes: null, columnClasses:columnClasses);
        }

        protected void CloseRow()
        {
            //end all columns up to and including last row
            //if no row is found, end all columns up to but NOT including last grid/container
            while (RendererStack.Count > 0)
            {
                IRenderer top = RendererStack.Top;

                if (top is RowTag)
                {
                    _rowColumnClassList = top.Data as string[];
                    RendererStack.Close();
                    break;
                }
                else if (top is ColumnTag)
                {
                    RendererStack.Close();
                }
                else
                {
                    break;
                }
            }

            _columnNumber = 0;

        }

        public IRow<C> Row(Action<GlobalAttributes> attributes, string[] columnClasses = null)
        {
            CloseRow();

            var rowTag = new RowTag(Context, Context.AttributeBuilder.Attributes(attributes, new { Class = _gridOptions?.RowClass }), _rowColumnClassList);

            _rowColumnClassList = columnClasses;

            return Open(rowTag);
        }

        public IRow<C> Row(string @class, string[] columnClasses = null)
        {
            return Row(a => a.Class = @class, columnClasses);
        }

        public IRow<C> Row(string[] columnClasses = null)
        {
            return Row(attributes: null, columnClasses: columnClasses);
        }

        protected void CloseColumn()
        {
            //end all elements up to last column
            //end last (current) column
            //don't end grids, row or containers
            while (RendererStack.Count > 0)
            {
                IRenderer top = RendererStack.Top;

                if (top is ColumnTag)
                {
                    RendererStack.Close();
                    break;
                }
                else if (top is GridBlock || top is ContainerTag || top is RowTag)
                {
                    break;
                }
                else
                {
                    RendererStack.Close();
                }
            }

        }

        public IColumn<C> Column(string @class)
        {
            CloseColumn();

            ++_columnNumber;

            return Open(new ColumnTag(Context, Context.AttributeBuilder.Attributes(new { Class = @class })));
        }

        public IColumn<C> Column(Action<GlobalAttributes> attributes = null)
        {
            CloseColumn();

            string columnClasses = null;

            if (_rowColumnClassList != null && _columnNumber < _rowColumnClassList.Length)
            {
                columnClasses = _rowColumnClassList[_columnNumber];
            }
            else if (_containerColumnClassList != null && _columnNumber < _containerColumnClassList.Length)
            {
                columnClasses = _containerColumnClassList[_columnNumber];
            }
            else if (_gridOptions?.ColumnClassList != null && _columnNumber< _gridOptions.ColumnClassList.Length)
            {
	            columnClasses = _gridOptions?.ColumnClassList[_columnNumber];
            }

            ++_columnNumber;

            return Open(new ColumnTag(Context, Context.AttributeBuilder.Attributes(attributes, new { Class = columnClasses, attributes=new { Class=_gridOptions?.ColumnClass} })));
        }

        public IGrid<C> Grid(Action<GridOptions> gridOptions = null)
        {
            var grid = new FluentGrid<C>(Context, RendererStack, gridOptions, _gridOptions);
            return grid;
        }

        public IGrid<C> CloseGrid()
        {
            //end all elements up to last grid, if any
            //end last (current) grid
            while (RendererStack.Count > 0)
            {
                IRenderer top = RendererStack.Top;

                if (top is GridBlock)
                {
                    _gridOptions = top.Data as GridOptions;
                    RendererStack.Close();
                    break;
                }
                else
                {
                    RendererStack.Close();
                }
            }

            return this;
        }

        protected FluentGrid<C> Here<T>(Action<T> action, T value)
        {
            if (action != null)
            {
                action(value);
            }

            return this;
        }

        public IGrid<C> Here(Action<IGrid<C>> grid)
        {
            return Here(grid, this);
        }

        public IContainer<C> Here(Action<IContainer<C>> grid)
        {
            return Here(grid, this);
        }

        public IRow<C> Here(Action<IRow<C>> grid)
        {
            return Here(grid, this);
        }

        public IColumn<C> Here(Action<IColumn<C>> grid)
        {
            return Here(grid, this);
        }

        protected FluentGrid<C> _Html(Action<FluentHtml<C>> action)
        {
            if (action != null)
            {
                var fh = new FluentHtml<C>(Context, RendererStack);
                action(fh);
            }

            return this;
        }

        IGrid<C> IGrid<C>.Html(Action<FluentHtml<C>> html)
        {
            return _Html(html);
        }

        IContainer<C> IContainer<C>.Html(Action<FluentHtml<C>> html)
        {
            return _Html(html);
        }

        IRow<C> IRow<C>.Html(Action<FluentHtml<C>> html)
        {
            return _Html(html);
        }

        IColumn<C> IColumn<C>.Html(Action<FluentHtml<C>> html)
        {
            return _Html(html);
        }

    }
}
