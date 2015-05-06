using System;

namespace Z.Data.Tools
{
    public static class Tools
    {
        public static void Require(object dependency, string name)
        {
            if (dependency == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}