using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paraquery.Html.Tags;

namespace com.paraquery.Html
{
    public class Background: StyleBase, IValueContainer //, IDynamicValueContainer
    {
        public string backgroundColor { set; get; }
        public Color? BackgroundColor { set; get; }

        public string backgroundImage { set; get; }
        public List<BackgroundImage> Image = new List<BackgroundImage>();
        //protected BackgroundImage _backgroundImage;

        /*
            
            position
            size
            repeat
            origin
            clip
            attachment



        */


        //bool IDynamicValueContainer.HasValue(string propertyName)
        //{
        //    switch (propertyName)
        //    {
        //        case nameof(BackgroundImage):
        //            return _backgroundImage != null;

        //        default:
        //            throw new InvalidOperationException($"Property name {propertyName} not found");
        //    }

        //}

        //[DynamicValue]
        //public BackgroundImage BackgroundImage
        //{
        //    set
        //    {
        //        _backgroundImage = value;
        //    }
        //    get
        //    {
        //        if (_backgroundImage == null)
        //        {
        //            _backgroundImage = new BackgroundImage();
        //        }

        //        return _backgroundImage;
        //    }
        //}


    }


}
