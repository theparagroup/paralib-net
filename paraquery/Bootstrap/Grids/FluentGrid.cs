using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Attributes;
using com.paraquery.Html.Fluent;
using com.paraquery.Html.Tags;
using com.paraquery.Rendering;
using com.paraquery.Html;

namespace com.paraquery.Bootstrap.Grids
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

    public partial class FluentGrid : FluentHtml, IGrid, IContainer, IRow, IColumn
    {
        protected IList<string> _classes;
        protected int _columnNumber;

        public FluentGrid(HtmlContext context) : base(context, new Marker(context))
        {
        }

        public new class Marker : FluentHtml.Marker
        {
            public Marker(HtmlContext context) : base(context)
            {
            }

            protected override void OnBegin()
            {
                if (Context.Options.CommentFluentGrid)
                {
                    Comment("fluent bootstrap grid start");
                }
            }

            protected override void OnEnd()
            {
                if (Context.Options.CommentFluentGrid)
                {
                    Comment("fluent bootstrap grid end");
                }
            }
        }

        public class ContainerTag : Tag
        {
            public ContainerTag(Context context, AttributeDictionary attributes) : base(context, TagTypes.Block, "div", attributes)
            {
            }
        }

        public class RowTag : Tag
        {
            public RowTag(Context context, AttributeDictionary attributes) : base(context, TagTypes.Block, "div", attributes)
            {
            }
        }

        public class ColumnTag : Tag
        {
            public ColumnTag(Context context, AttributeDictionary attributes) : base(context, TagTypes.Block, "div", attributes)
            {
            }
        }

        protected override void OnBegin()
        {
        }


        protected new FluentGrid Push(Renderer renderer)
        {
            //this method simplifies "calling to Push() and returning a FluentGrid", 
            //as we do for grids, containers, rows, columns...
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
            return Push(new ContainerTag(Context, AttributeDictionary.Attributes(init, new { @class = fluid ? "container-fluid" : "container" })));
        }


        protected void CloseRow()
        {
            //end all columns up to and including last row
            //if no row is found, end all columns up to but NOT including last grid/container
            while (Stack.Count > 0)
            {
                Renderer top = Stack.Peek();

                if (top is RowTag)
                {
                    Pop();
                    break;
                }
                else if (top is Marker || top is ContainerTag)
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
            return Push(new RowTag(Context, AttributeDictionary.Attributes(init, new { @class = "row"})));
        }

        protected void CloseColumn()
        {
            //end all elements up to last column
            //end last (current) column
            //don't end grids, row or containers
            while (Stack.Count > 0)
            {
                Renderer top = Stack.Peek();

                if (top is ColumnTag)
                {
                    Pop();
                    break;
                }
                else if (top is Marker || top is ContainerTag || top is RowTag)
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

            return Push(new ColumnTag(Context, AttributeDictionary.Attributes(init, new { @class = columnClasses })));
        }

        public new IColumn Open(Renderer renderer)
        {
            base.Open(renderer);
            return this;
        }

        public new IColumn Close()
        {
            base.Close();
            return this;
        }

        public IGrid Grid()
        {
            var grid = new FluentGrid(Context);
            return Push(grid);
        }


        public IGrid EndGrid()
        {
            //end all elements up to last grid, if any
            //end last (current) grid
            while (Stack.Count > 0)
            {
                Renderer top = Stack.Peek();

                if (top is Marker)
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
