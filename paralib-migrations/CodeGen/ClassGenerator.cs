﻿using System;
using System.Linq;
using System.Collections.Generic;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;

namespace com.paralib.Migrations.CodeGen
{


    public abstract class ClassGenerator:Generator
    {

        public ClassGenerator(IClassWriter writer, IConvention convention, Dictionary<string, Table> tables, ClassOptions classOptions):base(writer,convention,tables, classOptions)
        {
        }

        public void Generate()
        {
            foreach (Table table in _tables.Values)
            {
                Generate(table);
            }
        }

        protected void Generate(Table table)
        {
            string className = GetClassName(table.Name);

            _writer.Start(className);

            OnGenerate(table, className);

            _writer.End();

        }

        protected abstract void OnGenerate(Table table, string className);


    }
}
