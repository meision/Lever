using Meision.VisualStudio.CustomTools;
using Microsoft.VisualStudio.Shell;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class GenerateLanguagesCommand : RunCustomToolCommand<ExcelLanguagesGenerator>
    {
        public GenerateLanguagesCommand()
        {
            this.CommandId = 0x0302;
        }
    }
}
