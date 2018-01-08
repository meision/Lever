using Meision.VisualStudio.CustomTools;
using Microsoft.VisualStudio.Shell;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class GenerateTestDataCommand : RunCustomToolCommand<ExcelTestDataAttributeGenerator>
    {
        public GenerateTestDataCommand()
        {
            this.CommandId = 0x0201;
        }
    }
}
