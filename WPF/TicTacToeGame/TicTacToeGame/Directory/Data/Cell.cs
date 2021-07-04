using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class Cell
    {
        #region property declarations

        public MarkType Type { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        #endregion
    }
}
