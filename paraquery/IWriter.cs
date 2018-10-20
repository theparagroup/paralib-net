﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    public interface IWriter
    {
        void Write(string text, bool indent = true);
        void WriteLine(string text, bool indent = true);
        void Snippet(string name, string text, bool indent = true, string newline="\n");

        void NewLine();
        bool IsNewLine { get; }
        bool IsSpaced { get; }

        void Indent();
        void Dedent();
        int TabLevel { get; }

        void Tab();
        void Tabs(int? level = null);

    }
}