using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using com.paralib.Gen.Rendering;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using com.parahtml.Attributes;
using System.Web.Mvc;

namespace com.parahtml.Mvc
{
    public class MvcForm<M> : MvcComponent<M>
    {
        protected ICloseable _form;
        protected Action<FormAttributes> _attributes;

        public MvcForm(MvcFragment<M> mvcFragment, Action<FormAttributes> attributes=null) : base(mvcFragment)
        {
            _attributes = attributes;
        }

        protected override void OnOpen()
        {
            _form = Fragment.Form(_attributes);
        }

        protected override void OnClose()
        {
            Fragment.Close(_form);
        }

        protected void Write(HtmlString text)
        {
            Fragment.Write(text?.ToHtmlString());
        }

        protected void WriteLine(HtmlString text)
        {
            Fragment.WriteLine(text?.ToHtmlString());
        }

        public void HiddenFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            Write(Fragment.Helpers.Html.HiddenFor(expression, htmlAttributes));
        }

        public void LabelFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            Write(Fragment.Helpers.Html.LabelFor(expression, htmlAttributes));
        }

        public void TextBoxFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            Write(Fragment.Helpers.Html.TextBoxFor(expression, htmlAttributes));
        }

        public void TextBoxFor<TProperty>(Expression<Func<M, TProperty>> expression, string format, object htmlAttributes = null)
        {
            Write(Fragment.Helpers.Html.TextBoxFor(expression, format, htmlAttributes));
        }

        public void PasswordFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            Write(Fragment.Helpers.Html.PasswordFor(expression, htmlAttributes));
        }

        public void CheckBoxFor(Expression<Func<M, bool>> expression, object htmlAttributes = null)
        {
            Write(Fragment.Helpers.Html.CheckBoxFor(expression, htmlAttributes));
        }

        public void DropDownListFor<TProperty>(Expression<Func<M, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes=null)
        {
            Write(Fragment.Helpers.Html.DropDownListFor(expression, selectList, optionLabel, htmlAttributes));
        }

        public void ValidationMessageFor<TProperty>(Expression<Func<M, TProperty>> expression)
        {
            Write(Fragment.Helpers.Html.ValidationMessageFor(expression));
        }

        public void ValidationSummary(bool excludePropertyErrors, string message)
        {
            Write(Fragment.Helpers.Html.ValidationSummary(excludePropertyErrors, message));
        }

        public void ValidationSummary(string message, object htmlAttributes)
        {
            Write(Fragment.Helpers.Html.ValidationSummary(message, htmlAttributes));
        }


    }
}
