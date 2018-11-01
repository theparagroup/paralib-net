using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery
{
    public class Context
    {
        public IWriter Writer { private set; get; }

        public Options Options { get; set; }=new Options();

        //namespace stack
        //namespace vars

        public Context(IWriter writer, Action<Options> init=null)
        {
            if (init!=null)
            {
                init(Options);                
            }

            Writer = writer;

            //derived classes create the other components

            //add initial namespace

        }


    }
}
