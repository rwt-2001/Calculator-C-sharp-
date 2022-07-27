using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_UI
{
    internal class MemoryData
    {
        public String data { get; private set; }
        public bool _binaryUsed { get; private set; }
        public bool _unaryUsed { get; private set; }
        public bool _dotUsed { get; private set; }
        public int _trackBrackets { get; private set; }

        public MemoryData()
        {
            this.data = String.Empty;
        }
        public void SetMemory(string data, bool binaryUsed, bool unaryUsed, bool dotUsed, int trackBrackets)
        {
            this.data = data;
            this._binaryUsed = binaryUsed;
            this._unaryUsed = unaryUsed;
            this._dotUsed = dotUsed;
            this._trackBrackets = trackBrackets;
        }
    }
}
