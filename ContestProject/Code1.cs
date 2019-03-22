using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

// -------------------------------------------------
// Wer hots vabrochn? Da Stoascheißer Koarl!
// -------------------------------------------------
namespace ContestProject
{
#if Level1
    /// <summary>
    /// Generic Type Works for 
    ///     - string
    ///     - IEnumerable^T
    ///     - InputRowElement
    /// </summary>
    public class Code : CodeBase<IList<double?>>
    {
        public override string Execute(IList<double?> row)
        {
            return row.First().ToString();
        }
    }
#endif
}
