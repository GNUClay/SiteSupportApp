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
        public int SomeProp { get; set; }

        /// <summary>
        /// SomeMethod
        /// </summary>
        /// <param name="a">SomeParam</param>
        public void Run(int a)
        {
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
