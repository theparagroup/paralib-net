using System;
using System.Text.RegularExpressions;

namespace com.paralib.Data
{
    public class BlobType : ParaType
    {
        public int MaximumLength { get; internal set; }

        public BlobType(string name) : base(name, typeof(byte[])) { }

    }



}
