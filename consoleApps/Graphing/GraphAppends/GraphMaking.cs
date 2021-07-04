using Graphing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAppends
{
    public static class GraphMaking
    {
        public static void DefaultSchedule()
        {
            var graph = GraphExpressionProcesses.ExpressionProcessing();
            graph.RootProcessing();
            graph.Printing();
        }
    }
}
