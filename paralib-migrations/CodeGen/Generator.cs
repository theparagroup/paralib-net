using System;
using System.Linq;
using com.paralib.Dal.Metadata;

namespace com.paralib.Migrations.CodeGen
{


    public abstract class Generator
    {
        protected string[] _skip;
        protected IClassWriter _writer;
        protected IConvention Convention { get; private set; }
        protected ClassOptions ClassOptions { get; private set; }

        public Generator(IClassWriter writer, IConvention convention, string[] skip, ClassOptions classOptions)
        {
            _skip = skip;
            _writer = writer;
            Convention = convention;
            ClassOptions = classOptions;
        }

        public void Generate(Table[] tables)
        {
            foreach (Table table in tables)
            {
                Generate(table);
            }
        }

        public void Generate(Table table)
        {
            if (_skip != null && (from s in _skip where s == table.Name select s).Count() > 0) return;

            string className = Convention.GetClassName(table.Name);

            _writer.Start(className);

            OnGenerate(table, className);

            _writer.End();

        }
        

        protected void Write(string text)
        {
            _writer.Write(text);
        }

        protected void WriteLine(string text=null)
        {
            _writer.WriteLine(text);
        }

        protected abstract void OnGenerate(Table table, string className);


    }
}
