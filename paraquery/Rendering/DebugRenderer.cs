using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        Containers are special renderers that are only visible when the "debug" parameter is set.

        We use them for things that are usually invisible, such as Pages and FluentHtml.
        
        Containers are Multiline/Nested, but indentation is an option.

        We want to make sure derived classes override OnDebug(), but there is no mechanism for that in C#,
        so we add an abstract "nuisance property" called "CanDebug" to force implementors to think about it.

    */
    public abstract class DebugRenderer : Renderer
    {
        public string Name { private set; get; }
        public bool IsDebug { private set; get; }

        public DebugRenderer(Context context, string name, bool debug, bool indent) : base(context, LineModes.Multiple, StackModes.Nested, false, debug, indent)
        {
            Name = name;
            IsDebug = debug;

            if (IsDebug && !CanDebug)
            {
                throw new InvalidOperationException("Containers must be able to write debug information when in debug mode");
            }

        }

        protected abstract bool CanDebug { get; }

        protected override void OnBegin()
        {
            if (IsDebug)
            {
                Debug($"{Name} start");
            }
        }

        protected override void OnEnd()
        {
            if (IsDebug)
            {
                Debug($"{Name} end");
            }
        }
    }
}
