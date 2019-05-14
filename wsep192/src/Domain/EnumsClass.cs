using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    public enum DuplicatePolicy { WithMultiplication, WithoutMultiplication };
    public enum LogicalConnections { or, and, xor };

    public static class EnumActivaties
    {
        //log=0=>and otherwise=>or
        public static LogicalConnections ConvertIntToLogicalConnections(object log)
        {
            if ((int)log == 0)
                return LogicalConnections.and;
            else
                return LogicalConnections.or;
        }


    }

   
}
