using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TstApp
{
    /// <summary>
    /// Description of ExampleClass
    /// </summary>
    /// <summary>
    /// Yet another summary
    /// </summary>
    /// <remarks>
    /// Remark 1
    /// </remarks>
    /// <remarks>
    /// Remark 2
    /// </remarks>
    public class ExampleClass
    {
        /// <summary type="e">
        /// Description of ExampleEnum
        /// </summary>
        public enum ExampleEnum
        {
            /// <summary>
            /// Comment 1
            /// </summary>
            F,

            /// <summary>
            /// Comment 2
            /// </summary>
            G
        }

        /// <summary>
        /// Some Prop2
        /// </summary>
        public ExampleEnum SomeProp2 { get; set; }
    }
}

namespace TstApp.ExampleNamespace
{
    /// <summary>
    /// Description of Example2
    /// </summary>
    public class Example2
    {
        /// <summary>
        /// SomeProp
        /// </summary>
        /// <value>someValue</value>
        public int SomeProp { get; set; }

        /// <summary>
        /// SomeMethod
        /// </summary>
        /// <param name="a">SomeParam</param>
        public void Run(int a)
        {
        }

        /// <summary>
        /// Run2
        /// </summary>
        /// <remarks>rem</remarks>
        /// <typeparam name="T">My very simple typeparam!!!</typeparam>
        /// <param name="a">SomeParam</param>
        /// <returns>Some return</returns>
        /// <para>Para</para>
        /// <example>This is my example</example>
        /// <exception cref="NotImplementedException">This is strong exception!!!!</exception>
        public int Run2<T>(int a)
        {
            return a;
        }

        /// <summary>
        /// Some Event
        /// </summary>
        event Action SomeEvent;
    }

    /// <summary type="i">
    /// IY
    /// </summary>
    public interface IY
    {
    }
}
