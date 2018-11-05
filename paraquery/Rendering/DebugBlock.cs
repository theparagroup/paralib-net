using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        Containers are special renderers that are used to wrap


    */
    public abstract class DebugBlock : Renderer
    {
        public string Name { private set; get; }
        public bool IsDebug { private set; get; }

        public DebugBlock(Context context, string name, bool debug, bool indent) : base(context, debug ? FormatModes.Block : FormatModes.None, StructureModes.Block, false, indent)
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
