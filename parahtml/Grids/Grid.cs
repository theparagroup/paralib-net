using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Builders;
using com.parahtml.Html2;
using com.paralib.Gen.Rendering;
using com.parahtml.Attributes;
using com.parahtml.Html;
using com.parahtml.Core;

namespace com.parahtml.Grids
{
    public class Grid : IComponent2, IContainer, IRow, IColumn
    {
        protected HtmlBuilder2 _htmlBuilder;

        protected GridOptions _gridOptions;
        protected ICloseable _marker;

        protected string[] _containerColumnClasses;
        protected string[] _rowColumnClasses;

        protected ICloseable _container;
        protected ICloseable _row;
        protected ICloseable _column;

        protected int _columnNumber;

        public Grid(HtmlBuilder2 htmlBuilder, GridOptions gridOptions)
        {
            _htmlBuilder = htmlBuilder;
            _gridOptions = gridOptions;
        }

        public Grid(HtmlBuilder2 htmlBuilder, Action<GridOptions> gridOptions = null) : this(htmlBuilder, GetOptions(gridOptions))
        {
        }

        protected static GridOptions GetOptions(Action<GridOptions> gridOptions)
        {
            GridOptions options = null;

            if (gridOptions != null)
            {
                options = new GridOptions();

                gridOptions(options);
            }

            return options;
        }

        protected HtmlContext Context
        {
            get
            {
                return _htmlBuilder.Context;
            }
        }

        protected ICloseable Open(IContent content)
        {
            return _htmlBuilder.Open(content);
        }

        protected void Close(ICloseable closable)
        {
            _htmlBuilder.Close(closable);
        }

        void IComponent2.Open()
        {
            _marker = _htmlBuilder.Open(new Marker(LineModes.Multiple, ContainerModes.Block));
        }

        void IComponent2.Close()
        {
            Close(_marker);
        }

        protected void CloseColumn()
        {
            Close(_column);
            _column = null;
        }

        protected void CloseRow()
        {
            CloseColumn();
            Close(_row);
            _row = null;
        }

        protected void CloseContainer()
        {
            CloseColumn();
            CloseRow();
            Close(_container);
            _container = null;
        }

        public IContainer Container(Action<GlobalAttributes> attributes, string[] columnClasses = null)
        {
            CloseContainer();

            _container = Open(new Tag("div", Context.AttributeBuilder.Attributes(attributes, new { Class = _gridOptions?.ContainerClass }), TagTypes.Block, false));

            _containerColumnClasses = columnClasses;

            return this;
        }

        public IContainer Container(string @class, string[] columnClasses = null)
        {
            return Container(a => a.Class = @class, columnClasses);
        }

        public IContainer Container(string[] columnClasses = null)
        {
            return Container(attributes: null, columnClasses: columnClasses);
        }

        public IRow Row(Action<GlobalAttributes> attributes, string[] columnClasses = null)
        {
            CloseRow();

            _row = Open(new Tag("div", Context.AttributeBuilder.Attributes(attributes, new { Class = _gridOptions?.RowClass }), TagTypes.Block, false));

            _rowColumnClasses = columnClasses;

            _columnNumber = 0;

            return this;
        }

        public IRow Row(string @class, string[] columnClasses = null)
        {
            return Row(a => a.Class = @class, columnClasses);
        }

        public IRow Row(string[] columnClasses = null)
        {
            return Row(attributes: null, columnClasses: columnClasses);
        }

        public IColumn Column(Action<GlobalAttributes> attributes, Action action = null)
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

            _column = Open(new Tag("div", Context.AttributeBuilder.Attributes(attributes, new { Class = columnClasses, attributes = new { Class = _gridOptions?.ColumnClass } }), TagTypes.Block, false));

            if (action != null)
            {
                action();
            }

            return this;
        }

        //public IColumn Column(string @class, Action action = null)
        //{
        //    CloseColumn();

        //    ++_columnNumber;

        //    _column = Open(new Tag("div", Context.AttributeBuilder.Attributes(new { Class = @class }), TagTypes.Block, false));

        //    return this;
        //}

        public IColumn Column(string @class, Action action = null)
        {
            return Column(a=> { a.Class = @class; }, action: action);
        }

        public IColumn Column(Action action = null)
        {
            return Column(attributes: null, action: action);
        }

    }
}
