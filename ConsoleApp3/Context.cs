using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Context
    {

        public Correlition Correlition { get; set; }

        public CvsReader CvsReader { get; set; }

        public Regression Regression { get; set; }

        public Dictionary<String, List<List<double>>> values { get; set; }

        public Context() { 
            Correlition = new Correlition();
            CvsReader = new CvsReader();
            Regression = new Regression(2, 4);
        }
    }
}
