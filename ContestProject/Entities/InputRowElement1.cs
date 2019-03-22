using LINQtoCSV;
using System;

namespace ContestProject
{
// Because the fields in this type are used only indirectly, the compiler
// will warn they are unused or unassigned. Disable those warnings.
#pragma warning disable 0169, 0414, 0649

#if Level1
    public class InputRowElement
    {
        [CsvColumn(Name = "KlientNr", FieldIndex = 1)]
        public int Klient { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public string MessageType { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public string Message { get; set; }

        [CsvColumn(FieldIndex = 4)]
        public string Scope { get; set; }

        [CsvColumn(FieldIndex = 5)]
        public string Type { get; set; }

        [CsvColumn(FieldIndex = 6)]
        public string Value { get; set; }
    }
#endif

#pragma warning restore 0169, 0414, 0649
}
