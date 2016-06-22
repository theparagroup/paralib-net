using System;
using com.paralib.Data;

namespace com.paralib.Migrations.ParaTypes
{
    public static class ParaTypeFactory
    {

        public static TNext AsParaType<TNext>(FluentMigrator.Builders.IColumnTypeSyntax<TNext> fluent, string name) where TNext : FluentMigrator.Infrastructure.IFluentSyntax
        {
            ParaType paraType = Paralib.ParaTypes[name];

            if (paraType.Type == typeof(string))
            {
                return fluent.AsString(((StringType)paraType).MaximumLength);
            }
            else if (paraType.Type == typeof(long))
            {
                return fluent.AsInt32();
            }
            else if (paraType.Type == typeof(byte[]))
            {
                return fluent.AsBinary();
            }
            else
            {
                throw new ParalibException($"ParaType \"{paraType.Type.Name}\" is not supported in migrations.");
            }
        }
    }
}
