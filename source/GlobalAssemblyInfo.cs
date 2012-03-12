// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Reflection;
using System.Runtime.InteropServices;

#if DEBUG

[assembly: AssemblyConfiguration("DEBUG")]
#elif RELEASE
[assembly: AssemblyConfiguration("RELEASE")]
#endif

[assembly: AssemblyCopyright("Copyright © Arnold Zokas 2012")]
[assembly: AssemblyProduct("MvcNavigation")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: AssemblyVersion("0.0.0.0")]
[assembly: AssemblyFileVersion("0.0.0.0")]