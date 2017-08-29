using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library
{
    public class KernelExtensions
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
                  [MarshalAs(UnmanagedType.LPTStr)]
                   string path,
                  [MarshalAs(UnmanagedType.LPTStr)]
                   StringBuilder shortPath,
                  int shortPathLength
                  );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetLongPathName(
               [MarshalAs(UnmanagedType.LPTStr)]
                   string path,
               [MarshalAs(UnmanagedType.LPTStr)]
                   StringBuilder longPath,
               int longPathLength            
            );

        /// <summary>
        /// Gets the long path.
        /// </summary>
        /// <param name="shortPath">The short path.</param>
        /// <returns>System.String.</returns>
        public string GetLongPath(string shortPath)
        {
            StringBuilder longPath = new StringBuilder(255);
            GetLongPathName(shortPath, longPath, longPath.Capacity);
            return longPath.ToString();
        }
        /// <summary>
        /// Gets the short path.
        /// </summary>
        /// <param name="longPath">The long path.</param>
        /// <returns>System.String.</returns>
        public string GetShortPath(string longPath)
        {

            StringBuilder shortPath = new StringBuilder(255);
            GetShortPathName(longPath, shortPath, shortPath.Capacity);
            return shortPath.Length == 0 ? longPath : shortPath.ToString();
        }
    }
}
