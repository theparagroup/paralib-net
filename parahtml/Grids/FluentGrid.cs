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

       
        When you add a Row to a Row, the last Row is closed. 
         
        When you add a Column to a Column, the last Column is closed.
        


    */

    public partial class FluentGrid<C> : FluentRendererStack<C, FluentGrid<C>>, IGrid<C>, IContainer<C>, IRow<C>, IColumn<C> where C : HtmlContext
    {
        protected GridOptions _gridOptions;

        protected string[] _containerColumnClasses;
        protected string[] _rowColumnClasses;

        protected int _columnNumber;


        public FluentGrid(C context, RendererStack rendererStack, Action<GridOptions> gridOptions = null) : base(context, rendererStack)
        {
            Grid(gridOptions);
        }

        protected class GridState
        {
            public GridOptions GridOptions;
            public string[] ContainerColumnClasses;
            public string[] RowColumnClasses;
            public int ColumnNumber;
        }

        protected class GridBlock : DebugBlock
        {
            public GridBlock(C context, GridState gridState) : base(context, "fluent grid", context.IsDebug(DebugFlags.Grids))
            {
                Data = gridState;
            }
        }

        protected class ContainerTag : Tag
        {
            public ContainerTag(C context, AttributeDictionary attributes, string[] columnClassList) : base(context, "div", attributes, TagTypes.Block, false)
            {
                Data = columnClassList;
            }
        }

        protected class RowTag : Tag
        {
            public RowTag(C context, AttributeDictionary attributes, string[] columnClassList) : base(context, "div", attributes, TagTypes.Block, false)
            {
                Data = columnClassList;
            }
        }

        protected class ColumnTag : Tag
        {
            public ColumnTag(HtmlContext context, AttributeDictionary attributes) : base(context, "div", attributes, TagTypes.Block, false)
            {
            }
        }

        public IGrid<C> Grid(Action<GridOptions> gridOptions = null)
        {
            var gridState = new GridState();
            gridState.GridOptions = _gridOptions;
            gridState.ContainerColumnClasses = _containerColumnClasses;
            gridState.RowColumnClasses = _rowColumnClasses;
            gridState.ColumnNumber = _columnNumber;

            _gridOptions = new GridOptions();
            _containerColumnClasses = null;
            _rowColumnClasses = null;
            _columnNumber = 0;

            if (gridOptions != null)
            {
                gridOptions(_gridOptions);
            }

            return Open(new GridBlock(Context, gridState));
        }

        public IGrid<C> CloseGrid()
        {
            //end all elements up to last grid
            //end last (current) grid
            //throw if no grid found

            bool gridFound = false;

            while (RendererStack.Count > 0)
            {
                IRenderer top = RendererStack.Top;

                if (top is GridBlock)
                {
                    var gridState = top.Data as GridState;
                    _gridOptions = gridState.GridOptions;
                    _containerColumnClasses = gridState.ContainerColumnClasses;
                    _rowColumnClasses = gridState.RowColumnClasses;
                    _columnNumber = gridState.ColumnNumber;

                    RendererStack.Close();

                    gridFound = true;

                    break;
                }
                else
                {
                    //everything not a gridblock
                    RendererStack.Close();
                }
            }

            if (!gridFound)
            {
                throw new Exception("Can't close grid, no grid found");
            }
            else
            {
                return this;
            }

        }

        protected void CloseContainer()
        {
            //end all columns and rows up to and including last container
            //if no row is found, end all columns up to but NOT including last grid
            while (RendererStack.Count > 0)
            {
                IRenderer top = RendererStack.Top;

                if (top is GridBlock)
                {
                    break;
                }
                else if (top is ContainerTag)
                {
                    _containerColumnClasses = top.Data as string[];
                    RendererStack.Close();
                    break;
                }
                else if (top is RowTag)
                {
                    _rowColumnClasses = top.Data as string[];
                    RendererStack.Close();
                }
                else if (top is ColumnTag)
                {
                    RendererStack.Close();
                }
                else
                {
                    //content
                    RendererStack.Close();
                }
            }

            _columnNumber = 0;

        }

        public IContainer<C> Container(Action<GlobalAttributes> attributes, string[] columnClasses = null)
        {
            CloseContainer();

            var containerTag = new ContainerTag(Context, Context.AttributeBuilder.Attributes(attributes, new { Class = _gridOptions?.ContainerClass }), _containerColumnClasses);

            _containerColumnClasses = columnClasses;

            return Open(containerTag);
        }

        public IContainer<C> Container(string @class, string[] columnClasses = null)
        {
            return Container(a => a.Class = @class, columnClasses);
        }

        public IContainer<C> Container(string[] columnClasses = null)
        {
            return Container(attributes: null, columnClasses: columnClasses);
        }

        protected void CloseRow()
        {
            //end all columns up to and including last row
            //if no row is found, end all columns up to but NOT including last grid/container
            while (RendererStack.Count > 0)
            {
                IRenderer top = RendererStack.Top;

                if (top is GridBlock || top is ContainerTag)
                {
                    break;
                }
                else if (top is RowTag)
                {
                    _rowColumnClasses = top.Data as string[];
                    RendererStack.Close();
                    break;
                }
                else if (top is ColumnTag)
                {
                    RendererStack.Close();
                }
                else
                {
                    //content
                    RendererStack.Close();
                }
            }

        }

        public IRow<C> Row(Action<GlobalAttributes> attributes, string[] columnClasses = null)
        {
            CloseRow();

            var rowTag = new RowTag(Context, Context.AttributeBuilder.Attributes(attributes, new { Class = _gridOptions?.RowClass }), _rowColumnClasses);

            _rowColumnClasses = columnClasses;

            _columnNumber = 0;

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

                if (top is GridBlock || top is ContainerTag || top is RowTag)
                {
                    break;
                }
                else if (top is ColumnTag)
                {
                    RendererStack.Close();
                    break;
                }
                else
                {
                    //content
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

            if (_rowColumnClasses != null && _columnNumber < _rowColumnClasses.Length)
            {
                columnClasses = _rowColumnClasses[_columnNumber];
            }
            else if (_containerColumnClasses != null && _columnNumber < _containerColumnClasses.Length)
            {
                columnClasses = _containerColumnClasses[_columnNumber];
            }
            else if (_gridOptions?.ColumnClasses != null && _columnNumber < _gridOptions.ColumnClasses.Length)
            {
                columnClasses = _gridOptions?.ColumnClasses[_columnNumber];
            }

            ++_columnNumber;

            return Open(new ColumnTag(Context, Context.AttributeBuilder.Attributes(attributes, new { Class = columnClasses, attributes = new { Class = _gridOptions?.ColumnClass } })));
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
