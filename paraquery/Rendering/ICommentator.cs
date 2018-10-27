using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Rendering
{
    /*

        If a Renderer implements this, then other classes (such as BlockRenderer) can use
        this method to output debug info and other comments into the rendered source.

        We do it this way because HTML comments are different from, say, JavaScript comments.

    */
    public interface ICommentator
    {
        void Comment(string text);
    }
}
