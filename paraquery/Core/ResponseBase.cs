using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Core
{
    public abstract class ResponseBase : IResponse
    {
        protected abstract void _write(string text);
        protected abstract string _tabs { get; }

        public virtual void Write(string text)
        {
            _write(_tabs);
            _write(text);
        }

        public virtual void WriteLine(string text)
        {
            Write(text);
            NewLine();
        }

        public abstract void NewLine();

        public abstract void Tab();

        public abstract void Indent();

        public abstract void Dedent();

        public virtual void Snippet(string name, string text)
        {
            text = text.Replace("\n", $"\n{_tabs}");

            _write(_tabs);
            _write(text);
            NewLine();
        }

        public virtual void Attribute(string name, string value)
        {
            if (name!=null && value!=null)
            {
                Write($" {name}=\"{value}\"");
            }
        }



        public virtual void Block(string text)
        {
            Write(text);
        }

        public virtual void StartBlock(string name)
        {
            Write($"<{name}");
        }

        public virtual void EndBlock()
        {
            Write(">");
            NewLine();
        }

        public virtual void CloseBlock(string name)
        {
            Write($"</{name}>");
            NewLine();
        }

        public virtual void Inline(string text)
        {
            Write(text);
        }

        public virtual void StartInline(string name)
        {
            Write($"<{name}");
        }

        public virtual void EndInline()
        {
            Write(">");
        }

        public virtual void CloseInline(string name)
        {
            Write($"</{name}>");
        }

        public virtual void Empty(string name)
        {
            Write($"<{name} />");
        }

    }
}
