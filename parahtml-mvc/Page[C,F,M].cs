using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using com.parahtml.Core;
using com.parahtml;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using com.paralib.Gen;
using System.Web.Mvc;

namespace com.parahtml.Mvc
{
    /*

        Base class that makes a Fragment-derived class an IPage, but with a 
        Model.

    */
    public abstract class Page<C, F, M> : Fragment<C, F>, IPage<C>, IHasModel<M> where C : MvcContext where F : Page<C, F, M>
    {
        protected M Model { private set; get; }
        protected Helpers<M> Helpers { private set; get; }

        public Page() : base(null)
        {
        }

        public abstract C CreateContext(ViewContext viewContext, TextWriter textWriter);

        void IHasModel<M>.SetModel(M model)
        {
            Model = model;
        }

        void IPage<C>.Render(C context)
        {
            ((IHasContext<C>)this).SetContext(context);
            Helpers = new Helpers<M>(Context.ViewContext, Model);
            OnRender();
        }

        protected abstract void OnRender();

        void IPage<C>.End()
        {
            Dispose();
        }

        public F Write(HtmlString text)
        {
            return (F)Write(text?.ToHtmlString());
        }

        public F WriteLine(HtmlString text)
        {
            return (F)WriteLine(text?.ToHtmlString());
        }

        public F HiddenFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            return Write(Helpers.Html.HiddenFor(expression, htmlAttributes));
        }

        public F LabelFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            return Write(Helpers.Html.LabelFor(expression, htmlAttributes));
        }

        public F TextBoxFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            return Write(Helpers.Html.TextBoxFor(expression, htmlAttributes));
        }

        public F PasswordFor<TProperty>(Expression<Func<M, TProperty>> expression, object htmlAttributes = null)
        {
            return Write(Helpers.Html.PasswordFor(expression, htmlAttributes));
        }

        public F CheckBoxFor(Expression<Func<M, bool>> expression, object htmlAttributes = null)
        {
            return Write(Helpers.Html.CheckBoxFor(expression, htmlAttributes));
        }

        public F ValidationMessageFor<TProperty>(Expression<Func<M, TProperty>> expression)
        {
            return Write(Helpers.Html.ValidationMessageFor(expression));
        }

        public F ValidationSummary(bool excludePropertyErrors, string message)
        {
            return Write(Helpers.Html.ValidationSummary(excludePropertyErrors, message));
        }

        public F ValidationSummary(string message, object htmlAttributes)
        {
            return Write(Helpers.Html.ValidationSummary(message, htmlAttributes));
        }

    }
}