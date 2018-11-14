﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.parahtml.Core;
using com.paralib.Gen.Rendering;
using com.parahtml.Tags;
using com.parahtml.Attributes;
using com.parahtml.Tags.Fluent;
using com.parahtml;
using com.paralib.Gen.Fluent;

namespace com.parahtml.Tags.Fluent.Grids
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

    public partial class FluentGrid : FluentRendererStack<HtmlContext, FluentGrid>, IGrid, IContainer, IRow, IColumn
    {
        protected string _containerClass;
        protected string _rowClass;
        protected string _columnClass;

        protected IList<string> _gridColulmnClasses;
        protected IList<string> _containerColulmnClasses;
        protected IList<string> _rowColulmnClasses;
        protected int _columnNumber;

        public FluentGrid(HtmlContext context, RendererStack rendererStack, Action<GridOptions> options = null) : base(context, rendererStack)
        {
            var gridOptions = new GridOptions();

            if (options != null)
            {
                options(gridOptions);
            }

            _gridColulmnClasses = gridOptions.GridColumnClasses;

            _containerClass = gridOptions.ContainerClass;
            _rowClass = gridOptions.RowClass;
            _columnClass = gridOptions.ColumnClass;

            Open(new GridBlock(Context));
        }

        public class GridBlock : HtmlRenderer
        {
            public GridBlock(HtmlContext context) : base(context, LineModes.Multiple, ContainerModes.Block, false)
            {
            }

            protected override void OnBegin()
            {
                if (Context.IsDebug(DebugFlags.Grids))
                {
                    Context.Comment("fluent grid begin");
                }
            }

            protected override void OnEnd()
            {
                if (Context.IsDebug(DebugFlags.Grids))
                {
                    Context.Comment("fluent grid end");
                }
            }
        }

        public class ContainerTag : Tag
        {
            public ContainerTag(HtmlContext context, AttributeDictionary attributes) : base(context, TagTypes.Block, "div", attributes)
            {
            }
        }

        public class RowTag : Tag
        {
            public RowTag(HtmlContext context, AttributeDictionary attributes) : base(context, TagTypes.Block, "div", attributes)
            {
            }
        }

        public class ColumnTag : Tag
        {
            public ColumnTag(HtmlContext context, AttributeDictionary attributes) : base(context, TagTypes.Block, "div", attributes)
            {
            }
        }

        public IContainer Container(Action<GlobalAttributes> attributes, IList<string> columnClasses = null)
        {
            // fluid ? "container-fluid" : "container"
            _containerColulmnClasses = columnClasses;
            return Open(new ContainerTag(Context, Context.AttributeBuilder.Attributes(attributes, new { @class = _containerClass })));
        }

        public IContainer Container(IList<string> columnClasses = null)
        {
            return Container(null, columnClasses);
        }

        protected void CloseRow()
        {
            //end all columns up to and including last row
            //if no row is found, end all columns up to but NOT including last grid/container
            while (_rendererStack.Count > 0)
            {
                IRenderer top = _rendererStack.Top;

                if (top is RowTag)
                {
                    _rowColulmnClasses = null;
                    _rendererStack.Close();
                    break;
                }
                else if (top is ColumnTag)
                {
                    _rendererStack.Close();
                }
                else
                {
                    break;
                }
            }

            _columnNumber = 0;

        }

        public IRow Row(Action<GlobalAttributes> attributes, IList<string> columnClasses = null)
        {
            CloseRow();
            _rowColulmnClasses = columnClasses;
            return Open(new RowTag(Context, Context.AttributeBuilder.Attributes(attributes, new { @class = _rowClass})));
        }

        public IRow Row(IList<string> columnClasses = null)
        {
            return Row(null, columnClasses);
        }

        protected void CloseColumn()
        {
            //end all elements up to last column
            //end last (current) column
            //don't end grids, row or containers
            while (_rendererStack.Count > 0)
            {
                IRenderer top = _rendererStack.Top;

                if (top is ColumnTag)
                {
                    _rendererStack.Close();
                    break;
                }
                else if (top is GridBlock || top is ContainerTag || top is RowTag)
                {
                    break;
                }
                else
                {
                    _rendererStack.Close();
                }
            }

        }

        public IColumn Column(string @class)
        {
            CloseColumn();

            ++_columnNumber;

            return Open(new ColumnTag(Context, Context.AttributeBuilder.Attributes(new { @class = @class })));
        }

        public IColumn Column(Action<GlobalAttributes> attributes = null)
        {
            CloseColumn();

            string columnClasses = null;

            if (_rowColulmnClasses != null && _columnNumber < _rowColulmnClasses.Count)
            {
                columnClasses = _rowColulmnClasses[_columnNumber];
            }
            else if (_containerColulmnClasses != null && _columnNumber < _containerColulmnClasses.Count)
            {
                columnClasses = _containerColulmnClasses[_columnNumber];
            }
            else if (_gridColulmnClasses != null && _columnNumber < _gridColulmnClasses.Count)
            {
                columnClasses = _gridColulmnClasses[_columnNumber];
            }

            ++_columnNumber;

            return Open(new ColumnTag(Context, Context.AttributeBuilder.Attributes(attributes, new { @class = columnClasses, attributes=new { @class=_columnClass} })));
        }

        public IGrid Grid(Action<GridOptions> options = null)
        {
            return new FluentGrid(Context, _rendererStack, options);
        }


        public IGrid EndGrid()
        {
            //end all elements up to last grid, if any
            //end last (current) grid
            while (_rendererStack.Count > 0)
            {
                IRenderer top = _rendererStack.Top;

                if (top is GridBlock)
                {
                    _rendererStack.Close();
                    break;
                }
                else
                {
                    _rendererStack.Close();
                }
            }

            return this;
        }

        public IGrid Here(Action<IGrid> grid)
        {
            if (grid != null)
            {
                grid(this);
            }

            return this;
        }

        public IRow Here(Action<IContainer> container)
        {
            if (container != null)
            {
                container(this);
            }

            return this;
        }

        public IRow Here(Action<IColumn> row)
        {
            if (row!=null)
            {
                row(this);
            }

            return this;
        }

        public IColumn Html(Action<Html> html)
        {
            var fh = new Html(Context, _rendererStack);

            if (html != null)
            {
                html(fh);
            }

            return this;
        }

    }
}
