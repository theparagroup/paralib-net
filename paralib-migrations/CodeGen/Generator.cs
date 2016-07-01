﻿using System;
using System.Linq;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;

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

        protected virtual string GetClassName(string tableName)
        {
            return Convention.GetClassName(tableName, true);
        }

        protected bool IsNullable(Column column)
        {
            if (column.IsNullable && CSharpTypes.HasNullable(column.ClrType)) return true;
            return false;
        }

        protected void Start(string className)
        {
            _writer.Start(className);
        }

        protected void Write(string text)
        {
            _writer.Write(text);
        }

        protected void WriteLine(string text=null)
        {
            _writer.WriteLine(text);
        }

        protected void End()
        {
            _writer.End();
        }

    }
}