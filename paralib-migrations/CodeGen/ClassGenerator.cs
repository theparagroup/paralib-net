using System;
using System.Linq;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;

namespace com.paralib.Migrations.CodeGen
{


    public abstract class ClassGenerator:Generator
    {

        public ClassGenerator(IClassWriter writer, IConvention convention, string[] skip, ClassOptions classOptions):base(writer,convention,skip, classOptions)
        {
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

            string className = GetClassName(table.Name);

            _writer.Start(className);

            OnGenerate(table, className);

            _writer.End();

        }

        protected abstract void OnGenerate(Table table, string className);


    }
}
