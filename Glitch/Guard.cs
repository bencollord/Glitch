using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class Guard
    {
        public static void NotNull<T>(T obj, string name)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(name);
                }
            }
        public static void NotNull<T>(T obj, string name, string message)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name, message);
            }
        }
        public static void Require(bool condition, string message) => Require<ArgumentException>(condition, message);

        public static void Require<TException>(bool condition, string message)
            where TException : Exception
        {
            if (!condition)
            {
                //throw new ArgumentNullException(name, message);
                TException ex = (TException)Activator.CreateInstance(typeof(TException), message);
                throw ex;
            }
        }
    }
}
