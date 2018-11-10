using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paraquery.Html
{
    public class LinearGradient:Gradient
    {
        protected float? _degrees;
        protected Directions? _direction;

        public float? Degrees
        {
            set
            {
                _direction = null;
                _degrees = value;
            }
            get
            {
                return _degrees;
            }
        }

        public Directions? Direction
        {
            set
            {
                _degrees = null;
                _direction = value;
            }
            get
            {
                return _direction;
            }
        }

    }
}
