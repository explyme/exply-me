using System;
using System.Reflection;

namespace ExplyMe.Infrastructure.Modules
{
    public class ModuleInfo
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public Assembly Assembly { get; set; }
    }
}
