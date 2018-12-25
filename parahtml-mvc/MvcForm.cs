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

namespace com.parahtml.Mvc
{
    public class MvcForm<M> : MvcComponent<M>
    {
        protected ICloseable _form;
        protected Action<FormAttributes> _attributes;

        public MvcForm(MvcPage<M> mvcPage, Action<FormAttributes> attributes) : base(mvcPage)
        {
            _attributes = attributes;
        }

        protected override void OnOpen()
        {
            _form = MvcPage.Form(_attributes);
        }

        protected override void OnClose()
        {
            MvcPage.Close(_form);
        }

        protected void Write(HtmlString text)
        {
            MvcPage.Write(text?.ToHtmlString());
        }

        protected void WriteLine(HtmlString text)
        {
            MvcPage.WriteLine(text?.ToHtmlString());
        }

        public void HiddenFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            Write(Helpers.Html.HiddenFor(expression, htmlAttributes));
        }

        public void LabelFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            Write(Helpers.Html.LabelFor(expression, htmlAttributes));
        }

        public void TextBoxFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            Write(Helpers.Html.TextBoxFor(expression, htmlAttributes));
        }

        public void PasswordFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            Write(Helpers.Html.PasswordFor(expression, htmlAttributes));
        }

        public void CheckBoxFor(Expression<Func<M, bool>> expression, object htmlAttributes = null)
        {
            Write(Helpers.Html.CheckBoxFor(expression, htmlAttributes));
        }

        public void ValidationMessageFor<TProperty>(Expression<Func<M, TProperty>> expression)
        {
            Write(Helpers.Html.ValidationMessageFor(expression));
        }

        public void ValidationSummary(bool excludePropertyErrors, string message)
        {
            Write(Helpers.Html.ValidationSummary(excludePropertyErrors, message));
        }

        public void ValidationSummary(string message, object htmlAttributes)
        {
            Write(Helpers.Html.ValidationSummary(message, htmlAttributes));
        }


    }
}
