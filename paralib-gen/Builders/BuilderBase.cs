using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.paralib.Gen.Rendering;

namespace com.paralib.Gen.Builders
{
    /*

        Builder Mode
        --------------------------------------------------------

        implicit (stack) rules apply
            anything under a block nests
            inlines under an inline nest
            blocks under an inline close
            anything under a none close

        renderers and components? cannot be instatiated by developers

        all methods avaliable

        block methods return "blocks", all else void
        
        implementation of builder should call CloseAll()

        example
            Div();
            Span();
            Div();

        explicit closing of Blocks only
            var div1=Div();
            Span();
            Close(div1);
            Div();            

        "With" (sets context, closes renderer)
            With(renderer, renderer => 
            {
                Div();
                renderer.Method();
            }); 


            With(Div(), ()=>
            {
                Div();
            }); //closes div

            With(Span(), ()=>
            {
                Div(); //implicitly closes span
            }); //does not close to span


        Components
            var component=new Component(options=>
            {
            });

            With(component, component=>
            {
                Div()
                Span();
                component.Method();

            }); //closes to component





        Fluent Mode
        --------------------------------------------------------
        Html(fluent => 
        {

        });

    */
    public abstract class BuilderBase<C>:IBuilderBase<C> where C:Context 
    {
        private C _context;
        private RendererStack _rendererStack;

        public BuilderBase(RendererStack rendererStack)
        {
            _rendererStack = rendererStack;
        }

        void IBuilderBase<C>.SetContext(C context)
        {
            _context = context;
        }

        protected C Context
        {
            get
            {
                return _context;
            }
        }

        private void OnBeforeNewLine()
        {
            if (_rendererStack.Top?.LineMode != LineModes.Multiple)
            {
                _rendererStack.CloseUp();
            }
        }

        public void Write(string content)
        {
            Context.Writer.Write(content);
        }

        public void WriteLine(string content)
        {
            OnBeforeNewLine();
            Context.Writer.WriteLine(content);
        }

        public void NewLine()
        {
            OnBeforeNewLine();
            Context.Writer.NewLine();
        }

        public void Space()
        {
            OnBeforeNewLine();
            Context.Writer.Space();
        }

        void IBuilderBase<C>.Open(IRenderer renderer)
        {
            _rendererStack.Open(renderer);
        }

        public virtual void Close(IClosable closable)
        {
            _rendererStack.Close(closable.Renderer);
        }

        void IBuilderBase<C>.CloseAll()
        {
            _rendererStack.CloseAll();
        }

        void With(IClosable closable, Action<BuilderBase<C>> action)
        {
            if (action!=null)
            {

            }

        }

    }
}
