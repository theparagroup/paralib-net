using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Gen.Fluent
{
    internal class FluentConcepts
    {
        /*
            We start with an Interface<F> that returns a type, F, which will
            be our final object that has the fluent interface.

            We return the fluent object so we can chain method calls.

            Using an interface isn't strictly necessary, but it is an option.

        */
        public interface IFluent<F> where F : class
        {
            F DoIt1();
        }

        /*
            Next we implement the IFluent<F> interface in an abstract generic class.

            The key idea here is that the parameter F must be a type that *derives*
            from this very class, which allows us to cast this to an F.
        */
        public abstract class Fluent<F> : IFluent<F> where F : Fluent<F>
        {
            public F DoIt1()
            {
                Console.WriteLine("do it 1");
                return (F)this;
            }
        }

        /*
            If we want to use this fluent class as is, we create a concrete version
            of it.
        */
        public class Fluent : Fluent<Fluent>
        {
            //this is just the concrete (usable) version of Foo<F>
        }

        /*
            Or we can extend the fluent interface by continuing the pattern.

            IFoo will contain all of IFluent's methods.
        */
        public interface IFoo<F> : IFluent<F> where F : class
        {
            F DoIt2();
        }

        /*
            Implement another abstract class...
        */
        public abstract class Foo<F> : Fluent<F>, IFoo<F> where F : Foo<F>
        {
            public F DoIt2()
            {
                Console.WriteLine("do it 2");
                return (F)this;
            }

        }

        /*
            Now, Foo will inherit both implementations.
        */
        public class Foo : Foo<Foo>
        {
        }

        /*
            We can keep going...
        */
        public interface IBar<F> : IFoo<F> where F : class
        {
            F DoIt3();
        }

        public abstract class Bar<F> : Foo<F>, IBar<F> where F : Bar<F>
        {
            public F DoIt3()
            {
                Console.WriteLine("do it 3");
                return (F)this;
            }
        }

        public class Bar : Bar<Bar>
        {
        }

        /*
            And finally, this is how we can use it.
        */
        public class Test
        {
            public static void Go()
            {
                var fluent = new Fluent();
                fluent
                    .DoIt1();

                var foo = new Foo();
                foo
                    .DoIt1()
                    .DoIt2();

                var bar = new Bar();
                bar
                    .DoIt1()
                    .DoIt2()
                    .DoIt3();
            }
        }

    }

}
