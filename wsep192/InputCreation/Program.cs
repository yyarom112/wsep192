using System;
using src.ServiceLayer;

namespace InputCreation
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SystemState.fileCreation(StateCreator.createState());
        }

    }
}
