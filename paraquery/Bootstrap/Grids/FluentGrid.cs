using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Core;
using com.paraquery.Html.Blocks;

namespace com.paraquery.Bootstrap.Grids
{ 
    /*

        This fluent interface allows you build a bootstrap grid structure like this


        grid (virtual block)
            container (optional)
                row
                    column
                        nested grid
                            container (optional)


        You can use "using" syntax or call Begin/End methods.

        You can't nest <div> or other elements using the fluent interface (yet - we would need generic nesting of blocks)
        but you can do that with razor html or just by calling Response.Write.

        You *can* have nested containers (for that rare occasion) by nesting "using" statements and using Begin/End container calls,
        or by calling Container() in a nested grid in a column.

    */


    public class FluentGrid: Block, IGrid, IContainer, IRow, IColumn
    {
        private FluentGrid _parent;
        private IContext _context;
        private Div _container;
        private Div _row;
        private Div _column;
        private IList<string> _classes;
        private int _columnNumber;

        internal FluentGrid(IContext context) : base(context, false)
        {
            _context = context;
            Begin();
        }

        internal FluentGrid(IContext context, FluentGrid parent = null) : base(context, false)
        {
            _context = context;
            _parent = parent;
            Begin();
        }

        protected override void OnBegin()
        {
            //nothing to do here
            //WriteLine($"<!--gridstart  {_context.Response.TabLevel}-->");
        }

        protected override void OnPreEnd()
        {
            //end anything we have open
            _EndContainer();
        }

        protected override void OnEnd()
        {
            //nothing to do here
            //WriteLine($"<!--gridend {_context.Response.TabLevel}-->");
        }

        public IContainer Container(object attributes = null, bool fluid = false)
        {
            BeginContainer(attributes, fluid);
            return this;
        }

        public void BeginContainer(object attributes = null, bool fluid = false)//IList<string> classes = null)
        {
            if (_container != null)
            {
                throw new InvalidOperationException("Container already open");
            }

            _container = new Div(_context, new { @class = fluid ? "container-fluid" : "container", attributes = attributes });
        }

        private void _EndContainer()
        {
            _EndRow();

            _container?.End();
            _container = null;
        }

        public void EndContainer()
        {
            if (_container == null)
            {
                throw new InvalidOperationException("No container is open");
            }

            _EndContainer();
        }

        public IRow Row(object attributes = null)
        {
            BeginRow(attributes);
            return this;
        }

        public void BeginRow(object attributes = null)
        {
            if (_row != null)
            {
                _EndRow();
            }

            _row = new Div(_context, new { @class = "row", attributes = attributes });
        }

        private void _EndRow()
        {
            _EndColumn();

            _row?.End();
            _row = null;

            _columnNumber = 0;
        }

        public void EndRow()
        {
            if (_row == null)
            {
                throw new InvalidOperationException("No row is open");
            }

            _EndRow();
        }

        public IColumn Column(object attributes = null)
        {
            BeginColumn(attributes);
            return this;
        }

        public void BeginColumn(object attributes = null)
        {
            if (_column != null)
            {
                _EndColumn();
            }

            object columnClasses = new { };

            if (_classes!=null && _columnNumber<_classes.Count)
            {
                columnClasses = _classes[_columnNumber];
            }

            _column = new Div(_context, new { @class = columnClasses, attributes = attributes });

            //this points to the next column number
            ++_columnNumber;

        }

        private void _EndColumn()
        {
            _column?.End();
            _column = null;
        }

        public void EndColumn()
        {
            if (_column == null)
            {
                throw new InvalidOperationException("No column is open");
            }

            _EndColumn();
        }

        public IColumn Write(string content)
        {
            Context.Response.Write(content);
            return this;
        }

        public IColumn WriteLine(string content)
        {
            Context.Response.WriteLine(content);
            return this;
        }

        IGrid IColumn.Grid()
        {
            if (_column == null)
            {
                throw new InvalidOperationException("No Column open");
            }

            return new FluentGrid(Context, this);
        }

        IGrid IColumn.End()
        {

            if (_parent==null)
            {
                throw new InvalidOperationException("Not a nested grid");
            }

            End();

            return _parent;
        }

        public void SetClasses(IList<string> classes = null)
        {
            _classes = classes;
        }

        IGrid IGrid.SetClasses(IList<string> classes)
        {
            SetClasses(classes);
            return this;
        }

        IContainer IContainer.SetClasses(IList<string> classes)
        {
            SetClasses(classes);
            return this;
        }

        IRow IRow.SetClasses(IList<string> classes)
        {
            SetClasses(classes);
            return this;
        }

    }
}
