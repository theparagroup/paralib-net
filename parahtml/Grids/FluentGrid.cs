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
using com.paralib.Gen;

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

    public partial class FluentGrid : FluentRendererStack<FluentGrid>, IGrid, IContainer, IRow, IColumn
    {
        protected GridOptions _gridOptions;

        protected string[] _containerColumnClasses;
        protected string[] _rowColumnClasses;

        protected int _columnNumber;


        public FluentGrid(HtmlContext context, HtmlRendererStack rendererStack, Action<GridOptions> gridOptions = null) : base(rendererStack)
        {
            ((IFluentWriter)this).SetContext(context);
            NewGrid(GetOptions(gridOptions));
        }

        protected new HtmlContext Context
        {
            get
            {
                return (HtmlContext)base.Context;
            }
        }

        protected GridOptions GetOptions(Action<GridOptions> gridOptions)
        {
            GridOptions options = null;

            if (gridOptions != null)
            {
                options = new GridOptions();

                gridOptions(options);
            }

            return options;
        }


        protected class GridState
        {
            public GridOptions GridOptions;
            public string[] ContainerColumnClasses;
            public string[] RowColumnClasses;
            public int ColumnNumber;
        }

        protected class GridBlock : CommentBlock
        {
            public GridBlock(GridState gridState) : base("fluent grid")
            {
                Data = gridState;
            }

            protected override bool IsVisible()
            {
                return Context.IsDebug(DebugFlags.Grids);
            }
        }

        protected class ContainerTag : Tag
        {
            public ContainerTag(AttributeDictionary attributes, string[] columnClassList) : base("div", attributes, TagTypes.Block, false)
            {
                Data = columnClassList;
            }
        }

        protected class RowTag : Tag
        {
            public RowTag(AttributeDictionary attributes, string[] columnClassList) : base("div", attributes, TagTypes.Block, false)
            {
                Data = columnClassList;
            }
        }

        protected class ColumnTag : Tag
        {
            public ColumnTag(AttributeDictionary attributes) : base("div", attributes, TagTypes.Block, false)
            {
            }
        }

        protected void NewGrid(GridOptions gridOptions)
        {
            var gridState = new GridState();
            gridState.GridOptions = _gridOptions;
            gridState.ContainerColumnClasses = _containerColumnClasses;
            gridState.RowColumnClasses = _rowColumnClasses;
            gridState.ColumnNumber = _columnNumber;

            _gridOptions = gridOptions;
            _containerColumnClasses = null;
            _rowColumnClasses = null;
            _columnNumber = 0;

            Open(new GridBlock(gridState));
        }

        protected void CloseGrid()
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

        public IContainer Container(Action<GlobalAttributes> attributes, string[] columnClasses = null)
        {
            CloseContainer();

            var containerTag = new ContainerTag(Context.AttributeBuilder.Attributes(attributes, new { Class = _gridOptions?.ContainerClass }), _containerColumnClasses);

            _containerColumnClasses = columnClasses;

            return Open(containerTag);
        }

        public IContainer Container(string @class, string[] columnClasses = null)
        {
            return Container(a => a.Class = @class, columnClasses);
        }

        public IContainer Container(string[] columnClasses = null)
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

        public IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null)
        {
            CloseRow();

            var rowTag = new RowTag(Context.AttributeBuilder.Attributes(attributes, new { Class = _gridOptions?.RowClass }), _rowColumnClasses);

            _rowColumnClasses = columnClasses;

            _columnNumber = 0;

            return Open(rowTag);
        }

        public IRow Row(string @class, string[] columnClasses = null)
        {
            return Row(a => a.Class = @class, columnClasses);
        }

        public IRow Row(string[] columnClasses = null)
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

        public IColumn Column(string @class)
        {
            CloseColumn();

            ++_columnNumber;

            return Open(new ColumnTag(Context.AttributeBuilder.Attributes(new { Class = @class })));
        }

        public IColumn Column(Action<GlobalAttributes> attributes = null)
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

            return Open(new ColumnTag(Context.AttributeBuilder.Attributes(attributes, new { Class = columnClasses, attributes = new { Class = _gridOptions?.ColumnClass } })));
        }


        protected IColumn _Grid(GridOptions gridOptions, Action<IGrid> grid)
        {
            if (grid!=null)
            {
                NewGrid(gridOptions);
                grid(this);
                CloseGrid();
            }

            return this;
        }

        public IColumn Grid(Action<GridOptions> gridOptions, Action<IGrid> grid)
        {
            return _Grid(GetOptions(gridOptions), grid);
        }

        public IColumn Grid(Action<IGrid> grid)
        {
            return _Grid(null, grid);
        }

        protected FluentGrid Here<T>(Action<T> action, T value)
        {
            if (action != null)
            {
                action(value);
            }

            return this;
        }

        public IGrid Here(Action<IGrid> grid)
        {
            return Here(grid, this);
        }

        public IContainer Here(Action<IContainer> grid)
        {
            return Here(grid, this);
        }

        public IRow Here(Action<IRow> grid)
        {
            return Here(grid, this);
        }

        public IColumn Here(Action<IColumn> grid)
        {
            return Here(grid, this);
        }

        protected FluentGrid _Html(Action<FluentHtml> action)
        {
            if (action != null)
            {
                var fh = new FluentHtml(Context, (HtmlRendererStack)RendererStack);
                action(fh);
            }

            return this;
        }

        IGrid IGrid.Html(Action<FluentHtml> html)
        {
            return _Html(html);
        }

        IContainer IContainer.Html(Action<FluentHtml> html)
        {
            return _Html(html);
        }

        IRow IRow.Html(Action<FluentHtml> html)
        {
            return _Html(html);
        }

        IColumn IColumn.Html(Action<FluentHtml> html)
        {
            return _Html(html);
        }

    }
}
