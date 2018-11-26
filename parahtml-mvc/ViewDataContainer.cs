using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.parahtml.Mvc
{
    public class ViewDataContainer : IViewDataContainer
    {
        protected ViewDataDictionary _viewData;

        public ViewDataContainer(ViewDataDictionary viewData)
        {
            _viewData = viewData;
        }

        public ViewDataDictionary ViewData
        {
            get
            {
                return _viewData;
            }

            set
            {
                _viewData = value;
            }
        }
    }
}