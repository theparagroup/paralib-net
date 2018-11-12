using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml
{
    public class LinearGradient:Gradient
    {
        protected Angle _angle;
        protected SideOrCorner? _sideOrCorner;

        public Angle Angle
        {
            set
            {
                _angle = value;
            }
            get
            {
                return _angle;
            }
        }

        public SideOrCorner? SideOrCorner
        {
            set
            {
                _sideOrCorner = value;
            }
            get
            {
                return _sideOrCorner;
            }
        }

    }
}
