﻿using System;
using System.Linq;
using com.paralib.Dal.Metadata;
using com.paralib.Dal.Utils;
using System.Text.RegularExpressions;

namespace com.paralib.Migrations.CodeGen
{

    public class EfGenerator:ClassGenerator
    {
        public EfGenerator(IClassWriter writer, IConvention convention, Table[] tables, ClassOptions classOptions) : base(writer, convention, tables, classOptions)
        {
        }

        protected override string GetClassName(string tableName)
        {
            return Convention.EfPrefix + base.GetClassName(tableName);
        }

        protected string GetDependentEntityNavProperty(string tableName)
        {
            //user_types -> IList<EfUserType> UserTypes;
            return Convention.GetClassName(tableName, Pluralities.Plural);
        }

        protected string GetFKNavProperty(string columnName)
        {
            //created_by_user_id => CreatedByUser
            //user_type_id => UserType

            columnName = Convention.GetEntityName(columnName);
            return columnName;
        }

        protected override void OnGenerate(Table table, string className)
        {
            WriteLine("using System;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.ComponentModel.DataAnnotations.Schema;");

            //for debugging
            //if (className=="EfJob")
            //{
            //    int x = 1;
            //}

            //bad hack
            if (ClassOptions.Namespace != null) WriteLine($"using {ClassOptions.Namespace};");

            WriteLine();

            if (ClassOptions.Namespace!=null) WriteLine($"namespace {ClassOptions.Namespace}{(ClassOptions.SubNamespace!=null?"."+ ClassOptions.SubNamespace:"")}\n{{");

            WriteLine($"\tpublic partial class {className}:{Convention.GetClassName(table.Name, Pluralities.Singular)}");
            WriteLine("\t{");

            //fkey navigation properties (non-compound keys)
            foreach (Relationship r in table.ForeignKeys)
            {
                //is other table 'skipped'?
                //if (!_skip.Contains(r.OtherTable))
                //is other table included in our table list?
                if ((from t in _tables where t.Name==r.OtherTable select t).Count()>0)
                {
                    //created_by_user_id => [ForeignKey("CreatedByUserId")]
                    WriteLine($"\t\t[ForeignKey(\"{Convention.GetPropertyName(r.OnColumn)}\")]");

                    //created_by_user_id => public virtual EfUser CreatedByUser { get; set; }
                    //user_type_id => public virtual EfUserType UserType { get; set; }
                    string fkNav = GetFKNavProperty(r.OnColumn);
                    WriteLine($"\t\tpublic virtual {GetClassName(r.OtherTable)} {fkNav} {{ get; set;}}");
                }
            }

            /*
                Example of compound self-referential relationship:

                    [Key, Column(Order = 0)]
                    public int Id1 { get; set; }

                    [Key, Column(Order = 1)]
                    public int Id2 { get; set; }

                    public int? ParentId1 { get; set; }

                    public int? ParentId2 { get; set; }

                    [InverseProperty("Parent")]
                    public virtual List<EfAccount> Accounts { get; set; }

                    [ForeignKey("ParentId1,ParentId2")]
                    public virtual EfAccount Parent { get; set; }


                Alternate way: 
                (note column order is relative and the ordinals do not need to match):

                    [Key, Column(Order = 0)]
                    public int Id1 { get; set;}

                    [Key, Column(Order = 1)]
                    public int Id2 { get; set;}

                    [ForeignKey("EfAccount")]
                    [Column(Order = 2)]
                    public int Fk1 { get; set;}

                    [ForeignKey("EfAccount")]
                    [Column(Order = 3)]
                    public int Fk2{ get; set;}

                    [InverseProperty("Parent")]
                    public virtual List<EfAccount> Accounts { get; set; }

                    [ForeignKey("EfAccount")]
                    public virtual AccountType Parent { get; set; }

            */


            //dependent entity navigation properties (non-compound keys)
            foreach (Relationship r in table.References)
            {
                //is other table 'skipped'?
                //if (!_skip.Contains(r.OtherTable))
                //is other table included in our table list?
                if ((from t in _tables where t.Name == r.OtherTable select t).Count() > 0)
                {

                    //created_by_user_id => [InverseProperty("CreatedByUser")]
                    WriteLine($"\t\t[InverseProperty(\"{GetFKNavProperty(r.OtherColumn)}\")]");

                    //public virtual List<EfUser> CreatedByUser_Users { get; set; }
                    string entityNavPrefix = "";
                    if (table.References.Where(tr => tr.OtherTable == r.OtherTable).Count() > 1)
                    {
                        entityNavPrefix = GetFKNavProperty(r.OtherColumn) + "_";
                    }

                    //public virtual List<EfUser> Users { get; set; }
                    WriteLine($"\t\tpublic virtual List<{GetClassName(r.OtherTable)}> {entityNavPrefix + GetDependentEntityNavProperty(r.OtherTable)} {{ get; set;}}");
                }
            }


            WriteLine("\t}");
            if (ClassOptions.Namespace != null) WriteLine("}");



        }
    }
}
